using CodeBase.Infrastructure.State;
using CodeBase.Logic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain CurtainPrefab;
        
        private Game _game;
        private IGameStateMachine _stateMachine;

        [Inject]
        public void Construct(IGameStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        private void Awake()
        {
            var curtain = Instantiate(CurtainPrefab);
            _stateMachine.CreateAllState(new SceneLoader(this), curtain);
            _stateMachine.Enter<BootstrapState>();
     
            DontDestroyOnLoad(this);
        }
    }
}