namespace CodeBase.Infrastructure.State
{
    public interface IState: IExitableState
    {
        void Enter();
      
    }
}