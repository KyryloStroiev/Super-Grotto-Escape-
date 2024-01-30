using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Enemy.EnemyState;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class EnemyFactory : GameFactory, IEnemyFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IObjectPool _objectPool;

        public EnemyFactory(IAssetProvider assets, IStaticDataService staticData, IInputService inputService, IObjectPool objectPool) : base(assets, staticData, inputService, objectPool)
        {
            _assets = assets;
            _staticData = staticData;
            _objectPool = objectPool;
        }
        
        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetsAdress.Spawner);
        }

        public async Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent, Vector3 startPointPosition,
            Vector3 endPointPosition)
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
        
        private static void CreatePatrolPoint(Vector3 startPointPosition, Vector3 endPointPosition, GameObject monster)
        {
            if (startPointPosition != Vector3.zero && endPointPosition != Vector3.zero)
            {
                monster.GetComponent<EnemyPatrol>().StartPoint = startPointPosition;
                monster.GetComponent<EnemyPatrol>().EndPoint = endPointPosition;
            }
            else
            {
                monster.GetComponent<EnemyStateMachine>().IsIdlieState = true;
            }
        }
        

        public async Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId, Vector3 startPointPosition, Vector3 endPointPosition)
        {
            var prefab = await _assets.Load<GameObject>(AssetsAdress.Spawner);
            
            SpawnPoint spawner = InstantiateRegistered(prefab, at).GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.StartPoint = startPointPosition;
            spawner.EndPoint = endPointPosition;
            spawner.Id = spawnerId;
            spawner.MonsterTypeId = monsterTypeId;
        }

    }
}