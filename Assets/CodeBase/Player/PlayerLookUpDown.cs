using System;
using CodeBase.Infrastructure.Service.Input;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerLookUpDown : MonoBehaviour
    {
        private IInputService _inputService; 
        private ColliderChecking _colliderChecking;
        public bool IsLookingUp;
        private PlayerMovement _movement;
        

        private void Awake()
        {
            _colliderChecking = GetComponent<ColliderChecking>();
            _movement = GetComponent<PlayerMovement>();
        }

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public float VerticalDirection()
        {
            if (!_colliderChecking.IsLadder)
            {
                if (_inputService.Axis.y > 0.5)
                {
                    IsLookingUp = true;
                }
                else
                {
                    IsLookingUp = false;
                }
                return _inputService.Axis.y;
            }
            return 0;
        }
    }
}