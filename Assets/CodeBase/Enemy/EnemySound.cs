using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemySound : MonoBehaviour
    {
        public EnemySoundType _patrolSound;

        public EnemySoundType _aggroSound;
        
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
        
        public void PlayPatrolSound()
        {
            if (_patrolSound != null)
            {
                PlayLoopSound(_patrolSound);
            }
        }

        public void PlayAggroSound()
        {
            if (_aggroSound != null)
            {
                PlayLoopSound(_aggroSound);
            }
        }
           

        private void PlayLoopSound(EnemySoundType soundType)
        {
            AudioClip audioClip = EnemySoundData.GetSound(soundType);

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
            AudioSources?.Stop();
        }
    }
}