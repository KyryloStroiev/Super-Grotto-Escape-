using CodeBase.Logic;

namespace CodeBase.Infrastructure.State
{
    public interface IGameStateMachine
    {
        void CreateAllState(SceneLoader sceneLoader, LoadingCurtain curtain);
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}