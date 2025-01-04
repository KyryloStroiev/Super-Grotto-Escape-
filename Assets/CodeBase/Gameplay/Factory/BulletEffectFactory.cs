using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class BulletEffectFactory: IBulletEffectFactory
    {
        private readonly IAssetProvider _assets;
        
        public BulletEffectFactory(IAssetProvider assets)
        {
            _assets = assets;
        }
        
        public async Task<GameObject> CreateBullet(string addressPrefab)
        { 
            GameObject prefab =  await _assets.Load<GameObject>(addressPrefab);
            return Object.Instantiate(prefab);
        }
    }
}