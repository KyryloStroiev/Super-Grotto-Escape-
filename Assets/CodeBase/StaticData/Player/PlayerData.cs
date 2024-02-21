using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/Player")]
    public class PlayerData: ScriptableObject
    {
        public string Player;
        
        [Header("Health")]
        [Range(1,100)]
        public float MaxHP = 8;

        [Header("Attack")]
        [Range(1, 50)] 
        public float Damage;

        [Header("Movement")]
        [Range(1, 10)] 
        public float Speed = 5;
        [Range(1,10)]
        public float JumpHeight = 4;
        [Range(1, 10)]
        public float MaxGravityMultiplier = 5;
    }
}