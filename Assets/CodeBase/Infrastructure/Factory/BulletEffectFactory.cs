using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.StaticDataService;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class BulletEffectFactory: GameFactoryAbstract
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IInputService _inputService;

        public BulletEffectFactory(IAssetProvider assets, IStaticDataService staticData, IInputService inputService) : base(assets, staticData, inputService)
        {
            _assets = assets;
            _staticData = staticData;
            _inputService = inputService;
        }

        public void WarmUp()
        {
            _assets.Load<GameObject>(AssetsAdress.Shot);
        }

        public async Task<GameObject> CreateBullet()
        {
            GameObject prefab =  await _assets.Load<GameObject>(AssetsAdress.Shot);
            return Object.Instantiate(prefab);
        }
    }
}