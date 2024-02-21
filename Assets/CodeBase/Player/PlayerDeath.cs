
 using System;
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
        private bool _isDead;

        private void Awake()
        {
            _health = GetComponent<PlayerHealth>();
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponent<PlayerAnimator>();
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
            _playerMovement.enabled = false;
            
            var position = new Vector3(transform.position.x, transform.position.y +0.8f, transform.position.z);
            GameObject explossion = Instantiate(DeathExplossion, position, Quaternion.identity);
            Destroy(explossion, 1f);
        }
    }
}