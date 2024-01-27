using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        Task<GameObject> CreateHero(Vector3 at);
        Task<GameObject> CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
        
        Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent, Transform startPointPosition,
            Transform endPointPosition);

        Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId MonsterTypeId);
        Task WarmUp();
    }
}