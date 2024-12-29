using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimator : MonoBehaviour
    {

        private static readonly int MoveHash = Animator.StringToHash("Speed");
        private static readonly int SlideHash = Animator.StringToHash("IsSlide");
        private static readonly int ClimpUpHash = Animator.StringToHash("UpSpeed");
        private static readonly int LookUpHash = Animator.StringToHash("IsLookingUp");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int AttackInCrouchHash = Animator.StringToHash("ShootInCrouch");    
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int DamageHash = Animator.StringToHash("Damage");
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int OnLadderHash = Animator.StringToHash("OnLadder");
        private static readonly int OnCrouchHash = Animator.StringToHash("Crouch");
        
        private Animator _animator;
        private PlayerMovement _playerMovement;
        private PlayerLookUpDown _playerLookUpDown;
        private PlayerCrouch _crouch;
        private ColliderChecking _colliderChecking;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
            _colliderChecking = GetComponent<ColliderChecking>();
            _playerLookUpDown = GetComponent<PlayerLookUpDown>();
            _crouch = GetComponent<PlayerCrouch>();
        }

        private void Update()
        {
            _animator.SetFloat(MoveHash, VelocityX(), 0.1f, Time.deltaTime);
            _animator.SetBool(JumpHash, _playerMovement.IsJumping);
            _animator.SetBool(SlideHash, _playerMovement.IsSliding);
            _animator.SetBool(LookUpHash, _playerLookUpDown.IsLookingUp);
            _animator.SetBool(OnCrouchHash, _crouch.IsCrouch);
                
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

        public void PlayAttack()
        {
            if (!_crouch.IsCrouch)
                _animator.SetTrigger(AttackHash);
            else
                _animator.SetTrigger(AttackInCrouchHash);
        }

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