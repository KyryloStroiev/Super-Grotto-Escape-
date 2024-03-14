
 using System;
 using CodeBase.Infrastructure.State;
 using CodeBase.StaticData.Player;
 using UnityEngine;

namespace CodeBase.Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerDeath : MonoBehaviour
    {
        public GameObject DeathExplossion;
        
        private PlayerHealth _health;
        private PlayerMovement _playerMovement;
        private PlayerAnimator _animator;
        private IGameStateMachine _gameStateMachine;
        private PlayerSounds _sounds;

        private bool _isDead;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _health = GetComponent<PlayerHealth>();
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponent<PlayerAnimator>();
            _sounds = GetComponent<PlayerSounds>();
        }

        private void Start() => 
            _health.HealthChanged += HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && _health.CurrentHP <= 0) 
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _animator.PlayDie();
            _sounds.PlayOneSound(PlayerSoundType.Explosion);
            _playerMovement.enabled = false;
            var position = new Vector3(transform.position.x, transform.position.y +0.8f, transform.position.z);
            GameObject explossion = Instantiate(DeathExplossion, position, Quaternion.identity);
            Destroy(explossion, 1f);
            
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}