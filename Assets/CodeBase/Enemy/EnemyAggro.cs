using System;
using CodeBase.Enemy.EnemyState;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Enemy
{
    public class EnemyAggro : MonoBehaviour, IEnemyState
    {
        public float MinDistanceToAttack { get; set; }
        
        private bool _isEnable;

        private PlayerChecking _checking;
        private EnemyMove _move;
        private EnemyAnimator _animator;
        private EnemyAttackMelee _attack;
        private void Start()
        {
            _move = GetComponent<EnemyMove>();
            _checking = GetComponent<PlayerChecking>();
            _animator = GetComponent<EnemyAnimator>();
            _attack = GetComponent<EnemyAttackMelee>();
        }
        
        private void Update()
        {
            if (_isEnable && IsCloseToPlayer() ) 
                MoveToPlayer();

            Attack();
            
            _animator.PlayAggroMove(_isEnable && IsCloseToPlayer());
        }

        private void MoveToPlayer() => 
            _move.Move(PlayerPosition());

        private void Attack()
        {
            if (_isEnable && !IsCloseToPlayer())
            {
                _attack.StartAttack();
            }
        }

        private bool IsCloseToPlayer()
        { 
            float distanceToTarget = Mathf.Abs(PlayerPosition().x - transform.position.x);
            return (distanceToTarget >= MinDistanceToAttack);
        }

        public void Enable() => _isEnable = true;

        public void Disable() => _isEnable = false;

        private Vector3 PlayerPosition() => 
            _checking.Hit.collider.transform.position;
    }
}