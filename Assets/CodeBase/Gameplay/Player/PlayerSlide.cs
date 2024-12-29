using System.Collections;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerSlide
    {
        
        private readonly PlayerMovement _playerMovement;

        public PlayerSlide(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
         
        }

        public void Slide(float slideSpeedMultiplier, float slideDuration)
        {
            if (!_playerMovement.IsSliding)
            {
                _playerMovement.IsSliding = true;
                _playerMovement.StartCoroutine(SlideCorountine(slideSpeedMultiplier, slideDuration));
            }
        }

        private IEnumerator SlideCorountine(float slideSpeedMultiplier, float slideDuration)
        {
            float originalSpeed = _playerMovement.Speed;
            _playerMovement.Speed *= slideSpeedMultiplier;
            yield return new WaitForSeconds(slideDuration);
            _playerMovement.Speed = originalSpeed;
            _playerMovement.IsSliding = false;
        }
    }
}