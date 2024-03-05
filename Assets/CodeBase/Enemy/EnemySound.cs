using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemySound : MonoBehaviour
    {
        public EnemySoundType _patrolSound;
        
        public AudioSource AudioSources;

        public EnemySoundData EnemySoundData;
        
        public void PlayOneShot(EnemySoundType soundType)
        {
            AudioClip audioClip = EnemySoundData.GetSound(soundType);

            if (soundType != null)
            {
                AudioSources.PlayOneShot(audioClip);
            }
        }

        public void PatrolSound()
        {
            AudioClip audioClip = EnemySoundData.GetSound(_patrolSound);

            if (audioClip != null)
            {
                AudioSources.clip = audioClip;
                AudioSources.Play();
            }
         
        }

        public void SoundStop() => 
            AudioSources.Stop();
    }
}