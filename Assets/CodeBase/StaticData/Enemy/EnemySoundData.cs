using UnityEngine;

namespace CodeBase.Enemy
{
    [CreateAssetMenu(fileName = "EnemySound", menuName = "StaticData/Sound/Enemy Sound")]
    public class EnemySoundData: ScriptableObject
    {
        public AudioClip BatFlySound;
        public AudioClip SkeletPatrolSound;
        public AudioClip Fireball;

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
                default:
                    return null;
            }
        }
    }
}