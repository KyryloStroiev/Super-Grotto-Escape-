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
        }
    }
}