using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerSounds : MonoBehaviour
    {
        public AudioSource AudioSources;

        public PlayerSoundData PlayerSoundData;
        
        public void PlayOneShot(PlayerSoundType soundType)
        {
            AudioClip audioClip = PlayerSoundData.GetSound(soundType);

            if (soundType != null)
            {
                AudioSources.PlayOneShot(audioClip);
            }
        }
    }
}
