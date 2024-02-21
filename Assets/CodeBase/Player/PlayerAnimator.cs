using System;
using CodeBase.Infrastructure.Service.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class PlayerAnimator : MonoBehaviour
    {

        private static readonly int MoveHash = Animator.StringToHash("Speed");
        private static readonly int ClimpUpHash = Animator.StringToHash("UpSpeed");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int DamageHash = Animator.StringToHash("Damage");
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int OnLadderHash = Animator.StringToHash("OnLadder");
        
        private Animator _animator;
        private PlayerMovement _playerMovement;
        private ColliderChecking _colliderChecking;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
            _colliderChecking = GetComponent<ColliderChecking>();
        }

        private void Update()
        {
            _animator.SetFloat(MoveHash, VelocityX(), 0.1f, Time.deltaTime);
            
            _animator.SetBool(JumpHash, _playerMovement.IsJumping);
            
            PlayClimpUp(VelocityY());
        }

        public void PlayJump(bool isJump) => 
            _animator.SetBool(JumpHash, isJump);
        public void PlayOnLadder(bool isOnLadder) => 
            _animator.SetBool(OnLadderHash, isOnLadder);

        public void PlayClimpUp(float direction)
        {
            if (_colliderChecking.IsLadder)
            {
                _animator.SetFloat(ClimpUpHash, direction, 0.1f, Time.deltaTime);
            }
           
        }

        public void PlayAttack() =>
            _animator.SetTrigger(AttackHash);

        public void PlayHit() =>
            _animator.SetTrigger(DamageHash);

        public void PlayDie()
        {
            _animator.SetTrigger(DieHash);
        }
        private float VelocityX() => 
            Mathf.Abs(_playerMovement.HorizontalVelocity);
        private float VelocityY() => 
            Mathf.Abs(_playerMovement.VerticalVelocity);

        
        
      
    }
}