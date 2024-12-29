using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace CodeBase.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        
        [FormerlySerializedAs("MaxHP")]
        [Header("Health")]
        [Range(1, 100)]
        public float HP;

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
        [Range(0.2f, 20)] 
        public float DistanceForward = 7f;
        [Range(0.1f, 5)]
        public float DistanceBack = 0.2f;

        public bool IsFlying;
        
        public AssetReferenceGameObject PrefabReferenc;
    }
}