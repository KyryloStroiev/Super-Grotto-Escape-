using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Factory.EnemyFactory;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Infrastructure.Service.SaveLoad;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.Logic;
using CodeBase.UI.Service.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.State
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type,IExitableState> _states;
        private IExitableState _activeState;
        private IPlayerFactory _playerFactory;
        private IEnemyFactory _enemyFactory;
        private readonly IObjectPool _objectPool;
        private readonly IUIFactory _uiFactory;
        private IPersistentProgressService _persistentProgressService;
        private ISaveLoadService _saveLoadService;
        private IStaticDataService _staticDataService;
        private IAssetProvider _assetProvider;

        [Inject]
        public GameStateMachine(IPlayerFactory playerFactory, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService,
            IStaticDataService staticDataService, IAssetProvider assetProvider, IEnemyFactory enemyFactory, IObjectPool objectPool, IUIFactory uiFactory)
        {
            _playerFactory = playerFactory;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
            _enemyFactory = enemyFactory;
            _objectPool = objectPool;
            _uiFactory = uiFactory;
            _playerFactory.Construct(this);
        }

        public void CreateAllState(SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, _staticDataService, _assetProvider),
                [typeof(LoadProgressState)] = new LoadProgressState(this, _persistentProgressService, _saveLoadService, _staticDataService),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, _playerFactory,
                    _persistentProgressService, _staticDataService, _enemyFactory, _objectPool, _assetProvider, _uiFactory),
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