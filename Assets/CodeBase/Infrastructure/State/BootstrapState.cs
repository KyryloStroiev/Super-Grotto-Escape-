using CodeBase.Infrastructure.Service.StaticDataService;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class BootstrapState: IState
    {
        private const string Initial = "Initial";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            _staticDataService.Load();
            
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() =>
            _gameStateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }
    }
}