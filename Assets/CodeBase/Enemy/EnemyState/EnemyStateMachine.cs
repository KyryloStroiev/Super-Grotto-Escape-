using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Enemy.EnemyState
{
    public class EnemyStateMachine: MonoBehaviour
    {
        public bool IsIdlieState { get; set; }
        
        private List<IEnemyState> _states;
        private EnemyPlayerChecking _checking;
        private EnemyAggro _enemyAggro;
        private EnemyAttackRange _enemyAttackRange;


        private void Awake()
        {
            _checking = GetComponent<EnemyPlayerChecking>();
            _enemyAggro = GetComponent<EnemyAggro>();
            _enemyAttackRange = GetComponent<EnemyAttackRange>();
            
            _states = new List<IEnemyState>
            {
                GetComponent<EnemyPatrol>(),
                GetComponent<EnemyIdlie>()
            };
            
            if (_enemyAggro != null)
                _states.Add(_enemyAggro);
            
            if(_enemyAttackRange !=null)
                _states.Add(_enemyAttackRange);
            
            DisableAllState();
        }

        private void Update() => 
            ChangeState();

        private void ChangeState()
        {
            
            if (IsIdlieState && PlayerNotFound())
            {
                EnableState<EnemyIdlie>();
            }
            else if (PlayerFound())
            {
                if (_enemyAggro != null)
                    EnableState<EnemyAggro>();
                else
                    EnableState<EnemyAttackRange>();
            }
            else
            {
                EnableState<EnemyPatrol>();
            }
        }

        private void EnableState<TState>() where TState : IEnemyState
        {
            DisableAllState();
            var state = _states.Find(x => x is TState);
            state?.Enable();
        }

        private void DisableAllState()
        {
            foreach (IEnemyState state in _states)
            {
                state.Disable();
            }
        }

        private bool PlayerFound() => 
            _checking.IsPlayerFound;

        private bool PlayerNotFound() => 
            !_checking.IsPlayerFound;
    }
}