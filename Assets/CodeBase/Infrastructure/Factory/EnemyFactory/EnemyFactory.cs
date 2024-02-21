using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Enemy.EnemyState;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.Infrastructure.State;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.StaticData.Enemy;
using CodeBase.UI;
using CodeBase.UI.Service.Menu;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory.EnemyFactory
{
    public class EnemyFactory : GameFactory, IEnemyFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IObjectPool _objectPool;

        public EnemyFactory(IAssetProvider assets, IStaticDataService staticData, IInputService inputService, IObjectPool objectPool, IMenuService menuService) :
            base(assets, staticData, inputService, objectPool, menuService)
        {       
            _assets = assets;
            _staticData = staticData;
            _objectPool = objectPool;
        }
        
        public async Task WarmUp() => 
            await _assets.Load<GameObject>(AssetsAdress.Spawner);

        public async Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent, Vector3 startPointPosition,
            Vector3 endPointPosition)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeId);
            GameObject prefab = await _assets.Load<GameObject>(monsterData.PrefabReferenc);
            GameObject monster = InstantiateRegistered(prefab, parent.position);

            MonsterBuilder monsterBuilder = new MonsterBuilder(monster);
            monsterBuilder.SetHealth(monsterData.HP)
                          .SetMove(monsterData.Speed, monsterData.IsFlying)
                          .SetPlayerCheckingDistances(monsterData.DistanceForward, monsterData.DistanceBack)
                          .SetObjectPool(_objectPool);

            if (monster.GetComponent<EnemyAttackMelee>() != null)
            {
                monsterBuilder.SetMelleAttack(monsterData.Damage, monsterData.Cooldown,
                    monsterData.MinDistanceToAttack);
            }
            else if (monster.GetComponent<EnemyAttackRange>() != null)
            {
                monsterBuilder.SetRangeAttack(monsterData.Damage, monsterData.Cooldown, _objectPool);
            }
            
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