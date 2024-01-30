using CodeBase.Enemy.EnemyState;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyPatrol : MonoBehaviour, IEnemyState
    {
        public Vector3 StartPoint { get; set; }
        public Vector3 EndPoint { get; set; }
        
        private Vector3 _direction;
        private Vector3 _currentTarget;

        private bool _isEnable = false;
        
        private EnemyMove _move;
        private EnemyAnimator _animator;

        private void Start()
        {
            _move = GetComponent<EnemyMove>();
            _animator = GetComponent<EnemyAnimator>();
            _currentTarget = StartPoint;
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
           _currentTarget = (_currentTarget == StartPoint) ? EndPoint : StartPoint;

        public void Enable() => _isEnable = true;

        public void Disable() => _isEnable = false;
    }
}
