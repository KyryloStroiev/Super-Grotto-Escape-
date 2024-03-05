using System;
using UnityEngine;

namespace CodeBase.StaticData.Enemy
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public MonsterTypeId MonsterTypeId;
        public Vector3 Position;
        public bool IsLookLeft;

        public Vector3 StartPoint;
        public Vector3 EndPoint;
        

        public EnemySpawnerData(string id, MonsterTypeId monsterTypeId, bool isLookLeft, Vector3 position, Vector3 startPoint, Vector3 endPoint)
        {
            Id = id;
            MonsterTypeId = monsterTypeId;
            Position = position;
            IsLookLeft = isLookLeft;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}