using System;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerClimpUp : MonoBehaviour
    {
        public bool IsOnLadder;

        private ColliderChecking _checking;
        private PlayerAnimator _animator;

        private void Start()
        {
            _checking = GetComponent<ColliderChecking>();
            _animator = GetComponent<PlayerAnimator>();
        }

        private void Update()
        {
            if (_checking.IsLadder && !_checking.isGround)
            {
                IsOnLadder = true;
            }
            else
            {
                IsOnLadder = false;
            }
            _animator.PlayOnLadder(IsOnLadder);
        }
        
        public float MoveUp(float direction, float input, float speed)
        {
            float speedClimp = speed / 3;
            if (_checking.IsLadder)
            {
                direction = input * speedClimp;
            }
               

            return direction;
        }
    }
}
    
    
    
    
    
    
    
    
    
    
    
    
    