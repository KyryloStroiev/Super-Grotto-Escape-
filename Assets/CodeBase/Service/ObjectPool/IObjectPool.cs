using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Service.ObjectPool
{
    public interface IObjectPool
    {
        Task Instantiate(IAssetProvider assetProvider);
        GameObject GetPooledObject(string objectAddress, Vector3 position);
        void ReturnToPool(GameObject bullet, string objectAddress);

    }
}