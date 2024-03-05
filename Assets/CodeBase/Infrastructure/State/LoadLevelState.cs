using System.Threading.Tasks;
using Cinemachine;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Factory.EnemyFactory;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.Logic;
using CodeBase.Player;
using CodeBase.StaticData;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Level;
using CodeBase.UI;
using CodeBase.UI.Elements;
using CodeBase.UI.Service.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.State
{
    public class LoadLevelState: IPayloadedState<string>
    {
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private IPlayerFactory _playerFactory;
        private IEnemyFactory _enemyFactory;
        private IObjectPool _objectPool;
        private readonly IAssetProvider _assetProvider;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private IUIFactory _uiFactrory;


        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IPlayerFactory playerFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService, IEnemyFactory enemyFactory, IObjectPool objectPool,
            IAssetProvider assetProvider, IUIFactory uiFactrory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _playerFactory = playerFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _enemyFactory = enemyFactory;
            _objectPool = objectPool;
            _assetProvider = assetProvider;
            _uiFactrory = uiFactrory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            
            _playerFactory.Cleanup();
            _enemyFactory.Cleanup();
            
            _enemyFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => 
            _curtain.Hide();

        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitGameWorld();
            InformProgressReaders();
            
            _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitUIRoot() => 
            await _uiFactrory.CreateUIRoot();

        private async Task InitGameWorld()
        {
            LevelStaticData levelData = LevelStaticData();

            GameObject hero = await InitHero(levelData);
            await InitSpawners(levelData);
            await InitHud(hero);
            await InitObjectPool();
        }

        private async Task<GameObject> InitHero(LevelStaticData levelData)
        {
            GameObject player =  await _playerFactory.CreateHero(levelData.InitialHeroPosition);
            CameraFollow(player);
            return player;
        }

        private async Task InitSpawners(LevelStaticData levelData)
        {
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
               await _enemyFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, 
                   spawnerData.MonsterTypeId, spawnerData.IsLookLeft, spawnerData.StartPoint, spawnerData.EndPoint);
        }

        private async Task InitHud(GameObject hero)
        {
            GameObject hud = await _playerFactory.CreateHud();
            
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
            hud.GetComponentInChildren<PlayerUI>().Construct(hero.GetComponent<PlayerTakeBattery>());
           
        }

        private async Task InitObjectPool() => 
            await _objectPool.Instantiate(_assetProvider);

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _playerFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
            
            foreach (ISavedProgressReader progressReader in _enemyFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private static void CameraFollow(GameObject hero)
        {
            CameraFollow cameraFollow = Camera.main.GetComponentInChildren<CameraFollow>();
            cameraFollow.Construct(hero);
            cameraFollow.Follow();
        }

        private LevelStaticData LevelStaticData() => 
            _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
    }
}