using System;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Enemy
{
    [CreateAssetMenu(fileName = "EnemySound", menuName = "StaticData/Sound/Enemy Sound")]
    public class EnemySoundData: ScriptableObject
    {
        public AudioClip BatFlySound;
        public AudioClip SkeletPatrolSound;
        public AudioClip SkeletAggroSound;
        public AudioClip SkeletAtaackSound;
        public AudioClip Fireball;
        public AudioClip Explosion;

        public AudioClip GetSound(EnemySoundType soundType)
        {
            switch (soundType)
            {
                case EnemySoundType.BatFly:
                    return BatFlySound;
                case EnemySoundType.SkeletPatrol:
                    return SkeletPatrolSound;
                case EnemySoundType.Fireball:
                    return Fireball;
                case EnemySoundType.Explosion:
                    return Explosion; 
                case EnemySoundType.SkeletAggro:
                    return SkeletAggroSound;
                case EnemySoundType.SkeletAttack:
                    return SkeletAtaackSound;
                default:
                    return null;
            }
        }
    }
}