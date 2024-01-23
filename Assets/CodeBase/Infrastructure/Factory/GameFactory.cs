using System;
using System.Collections.Generic;
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

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assets;
        private IStaticDataService _staticData;
        private IInputService _inputService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressWriters { get; } = new();
        
        public event Action PlayerCreated;
        
        [Inject]
        public GameFactory(IAssetProvider assets, IStaticDataService staticData, IInputService inputService)
        {
            _assets = assets;
            _staticData = staticData;
            _inputService = inputService;
        }

        public GameObject CreateHero(Vector3 at)
        { 
            GameObject playerGameObject =  InstantiateRegistered(AssetPath.Player, at);
            playerGameObject.GetComponent<PlayerAttack>().Construct(_inputService);
            playerGameObject.GetComponent<PlayerMovement>().Construct(_inputService);
            PlayerCreated?.Invoke();
            return playerGameObject;
        }
        

        public GameObject CreateMonster(MonsterTypeId typeId, Transform parent, Transform startPointPosition,
            Transform endPointPosition)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeId);
            GameObject monster = _assets.Instantiate(monsterData.EnemyPrefab, parent.position);
            
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

            PlayerChecking _playerChecking = monster.GetComponent<PlayerChecking>();
            _playerChecking.DistanceForward = monsterData.DistanceForward;
            _playerChecking.DistanceBack = monsterData.DistanceBack;
            
            CreatePatrolPoint(startPointPosition, endPointPosition, monster);
            
            return monster;
        }

        public  GameObject CreateHud() =>
            InstantiateRegistered(AssetPath.Hud);

        public void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
        {
            var spawner = InstantiateRegistered(AssetPath.Spawner, at).GetComponent<SpawnPoint>();
            spawner.Construct(this);
            
            spawner.Id = spawnerId;
            spawner.MonsterTypeId = monsterTypeId;
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

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            
            RegisterProgressWatchers(gameObject);
            
            return gameObject;
        }


        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            
            RegisterProgressWatchers(gameObject);
            
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
        
    }
}