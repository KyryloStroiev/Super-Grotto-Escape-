using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Enemy.EnemyState
{
    public class EnemyStateMachine: MonoBehaviour
    {
        public bool IsIdlieState { get; set; }
        
        private List<IEnemyState> _states;
        private PlayerChecking _checking;


        private void Awake()
        {
            _checking = GetComponent<PlayerChecking>();
            
            _states = new List<IEnemyState>
            {
                GetComponent<EnemyPatrol>(),
                GetComponent<EnemyAggro>(),
                GetComponent<EnemyIdlie>()
            };
            
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
                EnableState<EnemyAggro>();
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