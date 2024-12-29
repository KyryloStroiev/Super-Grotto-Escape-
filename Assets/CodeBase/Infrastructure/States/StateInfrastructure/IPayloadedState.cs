namespace CodeBase.Infrastructure.State
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}