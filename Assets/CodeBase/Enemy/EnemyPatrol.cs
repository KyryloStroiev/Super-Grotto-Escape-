using System;
using CodeBase.Enemy.EnemyState;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyPatrol : MonoBehaviour, IEnemyState
    {
        public Transform StartPoint { get; set; }
        public Transform EndPoint { get; set; }
        
        private Vector3 _direction;
        private Vector3 _currentTarget;

        private bool _isEnable = false;
        
        private EnemyMove _move;
        private EnemyAnimator _animator;

        private void Start()
        {
            _move = GetComponent<EnemyMove>();
            _animator = GetComponent<EnemyAnimator>();
            _currentTarget = StartPoint.position;
        }

        private void Update() => 
            PatrolRange();
        
        private void FixedUpdate()
        {
            if (_isEnable)
            {
                _move.Move(_currentTarget);
            }
            _animator.PlayMove(_isEnable);
        }

        private void PatrolRange()
        {
            if(IsCloseToTarget())
                SwitchTarget();
        }

        private bool IsCloseToTarget()
        { 
            float distanceToTarget = Mathf.Abs(_currentTarget.x - transform.position.x);
           return (distanceToTarget <= 1f);
        }

        private void SwitchTarget() => 
           _currentTarget = (_currentTarget == StartPoint.position) ? EndPoint.position : StartPoint.position;

        public void Enable() => _isEnable = true;

        public void Disable() => _isEnable = false;
    }
}
