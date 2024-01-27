using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.SaveLoad;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.State
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type,IExitableState> _states;
        private IExitableState _activeState;
        private IGameFactory _gameFactory;
        private IPersistentProgressService _persistentProgressService;
        private ISaveLoadService _saveLoadService;
        private IStaticDataService _staticDataService;
        private IAssetProvider _assetProvider;

        [Inject]
        public GameStateMachine(IGameFactory gameFactory, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService, IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
        }

        public void CreateAllState(SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, _staticDataService, _assetProvider),
                [typeof(LoadProgressState)] = new LoadProgressState(this, _persistentProgressService, _saveLoadService),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, _gameFactory, _persistentProgressService, _staticDataService),
                [typeof(GameLoopState)] = new GameLoopState(this),
                
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}