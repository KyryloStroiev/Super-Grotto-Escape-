using CodeBase.Infrastructure.Service.ObjectPool;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Explosion : MonoBehaviour, IPoolable
    {
        
        private IObjectPool _objectPool;
        private string _assetsAddress;

        public void Construct(IObjectPool objectPool, string assetsAddress)
        {
            _assetsAddress = assetsAddress;
            _objectPool = objectPool;
        }
        public void ReturnToPool()
        {
            _objectPool.ReturnToPool(gameObject, _assetsAddress);
        }

      
    }
}
