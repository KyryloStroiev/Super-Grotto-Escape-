using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        Task<GameObject> Instantiate(string address);
        Task<GameObject> Instantiate(string prefabPath, Vector3 startPoint);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        Task<T> Load<T>(string address) where T : class;
        void CleanUp();
        void Initialize();
        Task<GameObject> Instantiate(GameObject prefab, Vector3 startPoint);
    }
}