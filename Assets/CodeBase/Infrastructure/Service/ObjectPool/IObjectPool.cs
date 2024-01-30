using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Service.ObjectPool
{
    public interface IObjectPool
    {
        Task Instantiate();
        GameObject GetPooledObject(string objectAddress, Vector3 position);
        void ReturnToPool(GameObject bullet, string objectAddress);

    }
}