using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.PersistentProgress;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class PlayerAttack : MonoBehaviour, ISavedProgress
    {

        public float Damage { get; set; }
        
        public Transform shootPoint;
        public GameObject bulletPrefab;
        
        private IInputService _inputService;
        private FlipDirectionPlayer _flip;
        private PlayerAnimator _animator;
        private Stats _stats;

     
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _inputService.Shoot += StartAttack;
            _flip = GetComponent<FlipDirectionPlayer>();
            _animator = GetComponent<PlayerAnimator>();
        }
        private void StartAttack() => 
            _animator.PlayAttack();

        private void OnAttack()
        {
            Bullet bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity)
                .GetComponent<Bullet>();
            Vector2 direction = _flip.IsFacingRight ? Vector2.right : Vector2.left;
            bullet.StartShoot(direction, _stats.Damage);
        }

        private void OnDisable() => 
            _inputService.Shoot -= StartAttack;

        public void LoadProgress(PlayerProgress progress) => 
            _stats = progress.PlayerStats;

        public void UpdateProgress(PlayerProgress progress)
        {
            
        }
    }
}