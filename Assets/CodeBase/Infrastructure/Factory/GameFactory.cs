using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Enemy.EnemyState;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Player;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assets;
        private IStaticDataService _staticData;
        private IInputService _inputService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressWriters { get; } = new();
        
        
        [Inject]
        public GameFactory(IAssetProvider assets, IStaticDataService staticData, IInputService inputService)
        {
            _assets = assets;
            _staticData = staticData;
            _inputService = inputService;
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetsAdress.Spawner);
        }

        public async Task<GameObject> CreateHero(Vector3 at)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetsAdress.Player);
            GameObject playerGameObject = InstantiateRegistered(prefab, at);
            
            playerGameObject.GetComponent<PlayerAttack>().Construct(_inputService);
            playerGameObject.GetComponent<PlayerMovement>().Construct(_inputService);
            return playerGameObject;
        }
        

        public async Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent, Transform startPointPosition,
            Transform endPointPosition)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeId);

            GameObject prefab = await _assets.Load<GameObject>(monsterData.PrefabReferenc);
            GameObject monster = InstantiateRegistered(prefab, parent.position);
            
            IHealth health = monster.GetComponent<IHealth>();
            health.CurrentHP = monsterData.HP;
            health.MaxHP = monsterData.HP;
            monster.GetComponent<ActorUI>().Construct(health);

            monster.GetComponent<EnemyMove>().Speed = monsterData.Speed;

            EnemyAttackMelee attackMelee = monster.GetComponent<EnemyAttackMelee>();
            if (attackMelee != null)
            {
                attackMelee.Damage = monsterData.Damage;
                attackMelee.Cooldown = monsterData.Cooldown;
            }
            
            monster.GetComponent<EnemyAggro>().MinDistanceToAttack = monsterData.MinDistanceToAttack;

             PlayerChecking playerChecking = monster.GetComponent<PlayerChecking>();
            playerChecking.DistanceForward = monsterData.DistanceForward;
            playerChecking.DistanceBack = monsterData.DistanceBack;
            
            CreatePatrolPoint(startPointPosition, endPointPosition, monster);
            
            return monster;
        }

        public async Task<GameObject> CreateHud() =>
           await InstantiateRegisteredAsync(AssetsAdress.Hud);

        public async Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
        {
            var prefab = await _assets.Load<GameObject>(AssetsAdress.Spawner);
            
            SpawnPoint spawner = InstantiateRegistered(prefab, at).GetComponent<SpawnPoint>();
            spawner.Construct(this);
            
            spawner.Id = spawnerId;
            spawner.MonsterTypeId = monsterTypeId;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            _assets.CleanUp();
        }

        private static void CreatePatrolPoint(Transform startPointPosition, Transform endPointPosition, GameObject monster)
        {
            if (startPointPosition != null && endPointPosition != null)
            {
                monster.GetComponent<EnemyPatrol>().StartPoint = startPointPosition;
                monster.GetComponent<EnemyPatrol>().EndPoint = endPointPosition;
            }
            else
            {
                monster.GetComponent<EnemyStateMachine>().IsIdlieState = true;
            }
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }
        
        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            GameObject gameObject = await _assets.Instantiate(prefabPath);
            
            RegisterProgressWatchers(gameObject);
            
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }
    }
}