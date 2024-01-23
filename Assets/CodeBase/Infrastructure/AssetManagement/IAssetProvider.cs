using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string prefabPath, Vector3 startPoint);

        GameObject Instantiate(GameObject prefab, Vector3 startPoint);

    }
}