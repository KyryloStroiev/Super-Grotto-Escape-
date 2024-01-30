using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public MonsterTypeId MonsterTypeId;
        public Vector3 Position;

        public Vector3 StartPoint;
        public Vector3 EndPoint;

        public EnemySpawnerData(string id, MonsterTypeId monsterTypeId, Vector3 position, Vector3 startPoint, Vector3 endPoint)
        {
            Id = id;
            MonsterTypeId = monsterTypeId;
            Position = position;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}