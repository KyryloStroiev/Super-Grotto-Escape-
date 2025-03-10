using CodeBase.Infrastructure.State;

namespace CodeBase.Infrastructure.States.Factory
{
    public interface IStateFactory
    {
        T GetState<T>() where T: class, IExitableState;
    }
}