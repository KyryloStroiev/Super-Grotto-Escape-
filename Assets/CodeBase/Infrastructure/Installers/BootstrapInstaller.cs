using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Factory.EnemyFactory;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Infrastructure.Service.SaveLoad;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.Infrastructure.State;
using CodeBase.Infrastructure.States.Factory;
using CodeBase.UI.Service.Factory;
using CodeBase.UI.Service.Menu;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller, IInitializable, ICoroutineRunner
{
    public override void InstallBindings()
    {
        BindInfrastructureServices();
        BindGameStateMachine();
        BindGameState();
        BindStateFactory();
        BindInputService();
        BindCommonService();
        BindGameFactory();
        BindUIFactory();
        BindUIService();
    }
    

    private void BindGameStateMachine() => 
        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();


    private void BindStateFactory() =>
    Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
    
    private void BindInputService() => 
        Container.Bind<IInputService>().To<InputService>().AsSingle();

    private void BindGameFactory()
    {
        Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
        Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
    }

    private void BindUIFactory() => 
        Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();

    private void BindUIService()
    {
        Container.Bind<IMenuService>().To<MenuService>().AsSingle();
    }

    private void BindGameState()
    {
        Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameLoopState>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoadLevelState>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
    }
    private void BindCommonService()
    {
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        Container.Bind<IObjectPool>().To<ObjectPool>().AsSingle();
        Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
        Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
        Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
        Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        
    }


    private void BindInfrastructureServices()
    {
        Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
    }
    public void Initialize()
    {
        Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
    }
}