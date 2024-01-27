using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.StaticDataService;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class BootstrapState: IState
    {
        private const string Initial = "Initial";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetProvider _assetProvider;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
        }

        public void Enter()
        {
            _staticDataService.Load();
            _assetProvider.Initialize();
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() =>
            _gameStateMachine.Enter<LoadProgressState>();
        
        public void Exit()
        {
        }
    }
}