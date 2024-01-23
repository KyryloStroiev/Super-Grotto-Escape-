using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        
        [FormerlySerializedAs("MaxHP")]
        [Header("Health")]
        [Range(1, 100)]
        public int HP;

        [Header("Move")] 
        [Range(1, 20)] 
        public float Speed;

        [Header("Attack Melee")] 
        [Range(0.5f, 5)] 
        public float Damage = 1f;
        [Range(0.5f, 5)]
        public float Cooldown;
        [Range(0.5f, 2f)] 
        public float MinDistanceToAttack = 1f;
        
        [Header("Check Player")]
        [Range(0.2f, 12)] 
        public float DistanceForward = 7f;
        [Range(0.1f, 5)]
        public float DistanceBack = 0.2f;
        
        public GameObject EnemyPrefab;
    }
}