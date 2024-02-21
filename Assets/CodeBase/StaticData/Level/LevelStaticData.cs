using System.Collections.Generic;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.StaticData.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData: ScriptableObject
    {
        public string LevelKey;

        public List<EnemySpawnerData> EnemySpawners;
        public Vector3 InitialHeroPosition;
    }
}