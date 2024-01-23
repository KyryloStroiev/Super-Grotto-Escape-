
using Unity.Mathematics;
using UnityEngine;
using Zenject;


namespace CodeBase.Infrastructure.AssetManagement
{
  

    public class AssetProvider : IAssetProvider
    {
        private IInstantiator _instantiator;
        
        public AssetProvider(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
       
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }


        public GameObject Instantiate(string path, Vector3 startPoint)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, startPoint, Quaternion.identity);
        }

        public GameObject Instantiate(GameObject prefab, Vector3 startPoint) => 
            Object.Instantiate(prefab, startPoint, Quaternion.identity);
    }
}