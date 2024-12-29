using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class GameLoopState: IState, IUpdateable
    {
        private readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
        }
    }
}