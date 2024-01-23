using System;
using CodeBase.Infrastructure.Service.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class PlayerAnimator : MonoBehaviour
    {

        private static readonly int MoveHash = Animator.StringToHash("Speed");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int DamageHash = Animator.StringToHash("Damage");
        private static readonly int DieHash = Animator.StringToHash("Die");
        
        private Animator _animator;
        private PlayerMovement _playerMovement;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            _animator.SetFloat(MoveHash, Velocity(), 0.1f, Time.deltaTime);
            _animator.SetBool(JumpHash, _playerMovement.IsJumping);
        }

        public void PlayJump(bool isJump) => 
            _animator.SetBool(JumpHash, isJump);

        public void PlayAttack() =>
            _animator.SetTrigger(AttackHash);

        public void PlayHit() =>
            _animator.SetTrigger(DamageHash);

        public void PlayDie()
        {
            _animator.SetTrigger(DieHash);
        }
        private float Velocity() => 
            Mathf.Abs(_playerMovement.HorizontalVelocity);

        
        
      
    }
}