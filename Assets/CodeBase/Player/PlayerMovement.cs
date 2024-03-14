using System;
using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.StaticData.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, ISavedProgress
    {
        public float Speed { get; set; }
        public float JumpHeight { get; set; }
        public float SlideDuration { get; set; }
        public float SlideSpeedMultiplier { get; set; }
        public float MaxGravityMultiplier { get; set; } //Додати в код 
        
        private const float Gravity = -9.81f;

        private Vector2 _direction; 
        private bool _isJumping;
        public float HorizontalVelocity => _direction.x;
        public bool IsJumping => _isJumping;
        public float VerticalVelocity => _direction.y;

        private IInputService _input;
        private RunPlayer _runPlayer;
        private PlayerSlide _playerSlide;
        private ColliderChecking _colliderChecking;
        private GravityHandler _gravityHandler;
        private PlayerClimpUp _climpUp;
        private PlayerSounds _playerSounds;
        private Rigidbody2D _rigidbody;
        public bool IsSliding { get; set; }
        
        public void Construct(IInputService input)
        {
            _input = input;
            _colliderChecking = GetComponent<ColliderChecking>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _climpUp = GetComponent<PlayerClimpUp>();
            _playerSounds = GetComponent<PlayerSounds>();
            _gravityHandler = new GravityHandler(_colliderChecking);
            _runPlayer = new RunPlayer(_colliderChecking);
            _playerSlide = new PlayerSlide(this);
            _input.Jump += Jump;
            _input.Slide += Slide;
        }

        private void Update()
        {
            CheckDirection();
            ApplyGravity();
            if (_isJumping)
            {
                _direction.y += Gravity * Time.deltaTime;
            }

            if (!_colliderChecking.isGround && !_colliderChecking.IsLadder)
            {
                _isJumping = true;
            }
        }
        
        private void FixedUpdate() => 
            MovePlayer();

        private void MovePlayer() => 
            _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime);

        private void CheckDirection()
        {
            _direction.x = _runPlayer.Run(_direction.x, _input.Axis.x, Speed);
            _direction.y = _climpUp.MoveUp(_direction.y, _input.Axis.y, Speed);
        }

        private void Jump()
        {
            if (_colliderChecking.isGround)
            {
                _direction.y = (float)Math.Sqrt(JumpHeight * -1f * Gravity);
                _isJumping = true;
                _playerSounds.PlayOneSound(PlayerSoundType.Jump);
            }
        }

       

        private void Slide() => 
            _playerSlide.Slide(SlideSpeedMultiplier, SlideDuration);

        private void ApplyGravity() => 
            _gravityHandler.ApplyGravity(ref _direction, ref _isJumping);

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel =
                new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());

        public void LoadProgress(PlayerProgress progress)
        {
            if(CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savePosition = progress.WorldData.PositionOnLevel.Position;
                if (savePosition != null)
                {
                    Vector3Data savedPosition = savePosition;
                    transform.position = savedPosition.AsUnityVector().AddY(gameObject.transform.localScale.y);
                }
            }
        }

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;

        private void OnDisable()
        {
            _input.Slide -= Slide;
            _input.Jump -= Jump;
        }
    }
}
