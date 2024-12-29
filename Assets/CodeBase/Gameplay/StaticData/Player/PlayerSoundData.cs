using System;
using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [CreateAssetMenu(fileName = "PlayerSound", menuName = "StaticData/Sound/Player Sound")]
    public class PlayerSoundData : ScriptableObject
    {
        public AudioClip ShotSound;
        public AudioClip JumpSound;
        public AudioClip ExplosionSound;

        public AudioClip GetSound(PlayerSoundType soundType)
        {
            switch (soundType)
            {
                case PlayerSoundType.Jump:
                    return JumpSound;
                case PlayerSoundType.Shot:
                    return ShotSound;
                case PlayerSoundType.Explosion:
                    return ExplosionSound;
                default:
                    throw new ArgumentException("Unknown sound type", nameof(soundType));
            }
        }
        
    }
}