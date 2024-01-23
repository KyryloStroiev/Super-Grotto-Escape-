using CodeBase.Infrastructure;
using CodeBase.Infrastructure.State;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public GameObject gameBootstrapperPrefab; 
    public override void InstallBindings()
    {
        CreateGameBootstrapper();
        GameStateMachineBind();
    }

    private void CreateGameBootstrapper()
    {
        Container
            .Bind<GameBootstrapper>()
            .FromComponentsInNewPrefab(gameBootstrapperPrefab)
            .AsSingle()
            .NonLazy();
    }

    private void GameStateMachineBind() => 
        Container.Bind<IGameStateMachine>()
            .To<GameStateMachine>()
            .AsSingle();
    
 


}