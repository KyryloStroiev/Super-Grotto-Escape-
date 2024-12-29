using System;
using CodeBase.Enemy.EnemyState;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Enemy
{
    public class EnemyPatrol : MonoBehaviour, IEnemyState
    {
        public Vector3 StartPoint { get; set; }
        public Vector3 EndPoint { get; set; }
        
        private Vector3 _direction;
        private Vector3 _currentTarget;

        public bool IsEnable { get; set; }
        
        private EnemyMove _move;
        private EnemyAnimator _animator;
        private EnemySound _enemySound;

        private void Awake()
        {
            _move = GetComponent<EnemyMove>();
            _animator = GetComponent<EnemyAnimator>();
            _enemySound = GetComponent<EnemySound>();
        }

        private void Start()
        {
            _currentTarget = StartPoint;
         
        }

        private void Update() => 
            PatrolRange();
        
        private void FixedUpdate()
        {
            _move.Move(_currentTarget);
            _animator.PlayMove(IsEnable);
        }

        private void PatrolRange()
        {
            if(IsCloseToTarget())
                SwitchTarget();
        }

        private bool IsCloseToTarget()
        { 
            Vector3 distanceToTarget = _currentTarget - transform.position;
            return distanceToTarget.magnitude <= 1f;
        }

        private void SwitchTarget() => 
           _currentTarget = (_currentTarget == StartPoint) ? EndPoint : StartPoint;

        public void Enable()
        {
            IsEnable = true;
            enabled = true;
        }

        public void Disable()
        {
            IsEnable = false;
            enabled = false;
        }
    }
}
