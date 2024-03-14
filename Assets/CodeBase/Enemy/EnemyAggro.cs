using System;
using CodeBase.Enemy.EnemyState;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Enemy
{
    public class EnemyAggro : MonoBehaviour, IEnemyState
    {
        public float MinDistanceToAttack { get; set; }
        
        public bool IsEnable { get; set; }

        private EnemyPlayerChecking _checking;
        private EnemyMove _move;
        private EnemyAnimator _animator;
        private EnemyAttackMelee _attack;
        private EnemySound _sound;
        private void Start()
        {
            _move = GetComponent<EnemyMove>();
            _checking = GetComponent<EnemyPlayerChecking>();
            _animator = GetComponent<EnemyAnimator>();
            _attack = GetComponent<EnemyAttackMelee>();
            _sound = GetComponent<EnemySound>();
        }
        
        private void Update()
        {
            if (IsEnable && IsCloseToPlayer() ) 
                MoveToPlayer();

            Attack();
            
            _animator.PlayAggroMove(IsEnable && IsCloseToPlayer());
        }

        private void MoveToPlayer()
        {
            _move.Move(PlayerPosition());
        }

        private void Attack()
        {
            if (IsEnable && !IsCloseToPlayer())
            {
                _attack.StartAttack();
            }
        }

        private bool IsCloseToPlayer()
        { 
            float distanceToTarget = Mathf.Abs(PlayerPosition().x - transform.position.x);
            return (distanceToTarget >= MinDistanceToAttack);
        }

        public void Enable()
        {
            IsEnable = true;
           _sound.PlayAggroSound();
        }

        public void Disable() => IsEnable = false;

        private Vector3 PlayerPosition() => 
            _checking.Hit.collider.transform.position;
    }
}