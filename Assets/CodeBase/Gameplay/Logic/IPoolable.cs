using CodeBase.Infrastructure.Service.ObjectPool;

namespace CodeBase.Logic
{
    public interface IPoolable
    {
        void Construct(IObjectPool objectPool, string assetsAddress);
    }
}