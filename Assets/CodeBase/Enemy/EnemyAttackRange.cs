using System;
using CodeBase.Enemy.EnemyState;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAttackRange : MonoBehaviour, IEnemyState
    {
        public Transform ShootPoint;
        public float Damage { get; set; }
        public float Cooldown { get; set; }
        
        private bool _isEnable;
        private float _attackCooldown;
        private bool _isAttacking;

        private IObjectPool _objectPool;
        private EnemyFlipDirection _flip;
        private EnemyPlayerChecking _playerChecking;
        private EnemyAnimator _animator;
        private Vector2 _direction;


        public void Construct(IObjectPool objectPool)
        {
            _objectPool = objectPool;
            _animator = GetComponent<EnemyAnimator>();
            _flip = GetComponent<EnemyFlipDirection>();
            _playerChecking = GetComponent<EnemyPlayerChecking>();
        }

        private void Update()
        {
            UpdateCooldown();
            
            if (_playerChecking.PlayerBack())
            {
                _flip.Flip();
            }
            
            if (CanAttack())
            {
                _animator.PlayAttack();
                _isAttacking = true;
            }
        }

        private void OnAttack()
        {
            _direction = _flip.IsFacingRight ? Vector2.right : Vector2.left;
            GameObject bullet = _objectPool.GetPooledObject(AssetsAdress.EnemyBullet, ShootPoint.position);
            bullet.GetComponent<Bullet>().StartShoot(_direction, Damage);   
        }
        
        private void OnAttackEnded()
        {
            _attackCooldown = Cooldown;
            _isAttacking = false;
        }
        
        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }
        
        private bool CooldownIsUp() =>
            _attackCooldown <= 0;
        
        private bool CanAttack() =>
            !_isAttacking && CooldownIsUp() && _isEnable;
        
        public void Enable() => _isEnable = true;

        public void Disable() => _isEnable = false;
    }
}