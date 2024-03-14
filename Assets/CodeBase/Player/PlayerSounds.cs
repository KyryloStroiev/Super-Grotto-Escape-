using CodeBase.Logic;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerSounds: MonoBehaviour
    {
        
        public AudioSource AudioSources;

        public PlayerSoundData PlayerSoundData;
        
        public void PlayOneSound(PlayerSoundType soundType)
        {
            AudioClip audioClip = PlayerSoundData.GetSound(soundType);

            if (soundType != null)
            {
                AudioSources.PlayOneShot(audioClip);
            }
        }

        public void PlayLoopSound(PlayerSoundType soundType)
        {
            AudioClip audioClip = PlayerSoundData.GetSound(soundType);

            if (audioClip != null)
            {
                AudioSources.clip = audioClip;
                AudioSources.loop = true;
                AudioSources.Play();
            }
         
        }

        public void SoundStop()
        {
            AudioSources.loop = false;
            AudioSources.Stop();
        }
    }
}
