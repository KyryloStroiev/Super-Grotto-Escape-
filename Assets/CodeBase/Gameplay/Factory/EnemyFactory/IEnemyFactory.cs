using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory.EnemyFactory
{
    public interface  IEnemyFactory
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();

        Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent, bool isLookLeft, Vector3 startPointPosition,
            Vector3 endPointPosition);

        Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId, bool isLookLeft,
            Vector3 startPointPosition, Vector3 endPointPosition);

        Task WarmUp();
    }
}