using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Enemy.EnemyState
{
    public class EnemyStateMachine: MonoBehaviour
    {
        public bool IsIdlieState { get; set; }
        
        private List<IEnemyState> _states;
        private EnemyPlayerChecking _checking;
        private IEnemyState _activeState;
        
        private void Awake()
        {
            _checking = GetComponent<EnemyPlayerChecking>();
            
            _states = new List<IEnemyState>();
            AddState<EnemyPatrol>();
            AddState<EnemyIdlie>();
            
            AddStateIfAvailable<EnemyAggro>();
            AddStateIfAvailable<EnemyAttackRange>();
            
            DisableAllState();
        }

        private void Update() => 
            ChangeState();

        private void ChangeState()
        {
            IEnemyState newState = FindMatchingState();

            if (_activeState == newState)
            { 
                return;
            }
            else if (newState != null)
            {
                EnableState(newState);
            }
            
        }

        private IEnemyState FindMatchingState()
        {
            foreach (IEnemyState state in _states)
            {
                if (state is EnemyIdlie && IsIdlieState && PlayerNotFound())
                {
                    return state;
                }
                else if (state is EnemyPatrol && PlayerNotFound() && !IsIdlieState)
                {
                    return state;
                }
                else if (state is EnemyAggro && PlayerFound())
                {
                    return state;
                }
                else if(state is EnemyAttackRange && PlayerFound())
                {
                    return state;
                }
            }
            return null;
        }
        
        private void EnableState(IEnemyState state)
        {
            DisableAllState();
            _activeState = state;
            state?.Enable();
        }

        private void DisableAllState()
        {
            foreach (IEnemyState state in _states)
            {
                state.Disable();
            }
        }

        private void AddState<TState>() where TState : IEnemyState => 
            _states.Add(GetComponent<TState>());

        private void AddStateIfAvailable<TState>() where TState : IEnemyState
        {
            TState component = GetComponent<TState>();
            if (component != null)
            {
                _states.Add(component);
            }
        }
        
        private bool PlayerFound() => 
            _checking.IsPlayerFound;

        private bool PlayerNotFound() => 
            !_checking.IsPlayerFound;
    }
}