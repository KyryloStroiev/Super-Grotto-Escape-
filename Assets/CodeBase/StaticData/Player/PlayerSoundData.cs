using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [CreateAssetMenu(fileName = "PlayerSound", menuName = "StaticData/Sound/Player Sound")]
    public class PlayerSoundData : ScriptableObject
    {
        public AudioClip ShotSound;
        public AudioClip JumpSound;

        public AudioClip GetSound(PlayerSoundType soundType)
        {
            switch (soundType)
            {
                case PlayerSoundType.Jump:
                    return JumpSound;
                case PlayerSoundType.Shot:
                    return ShotSound;
                default:
                    return null;
            }
        }
    }
}