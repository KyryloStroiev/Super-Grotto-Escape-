using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        GameObject CreateHero(Vector3 at);
        GameObject CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
        
        event Action PlayerCreated;
        GameObject CreateMonster(MonsterTypeId typeId, Transform parent, Transform startPointPosition,
           Transform endPointPosition);

        void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId MonsterTypeId);
    }
}