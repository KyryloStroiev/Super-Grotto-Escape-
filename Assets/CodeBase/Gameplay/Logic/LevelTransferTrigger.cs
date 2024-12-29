using CodeBase.Infrastructure.State;
using CodeBase.StaticData.Level;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        public LevelId TransferTo;
        
        private bool _isTriggered;
        private const string Player = "Player";
        private IGameStateMachine _stateMachine;

        [Inject]
        public void Construct(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_isTriggered)
                return;
            if (other.CompareTag(Player))
            {
                _stateMachine.Enter<LoadLevelState, string>(TransferTo.ToString());
                _isTriggered = true;
            }
             
        }
    }
}