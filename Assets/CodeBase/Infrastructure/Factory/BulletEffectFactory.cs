using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Infrastructure.Service.StaticDataService;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class BulletEffectFactory: IBulletEffectFactory
    {
        private readonly IAssetProvider _assets;
       

        [Inject]
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