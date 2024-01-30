using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.Player;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class PlayerFactory : GameFactory, IPlayerFactory
    {
        private IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private IInputService _inputService;
        private readonly IObjectPool _objectPool;

        public PlayerFactory(IAssetProvider assets, IStaticDataService staticData, IInputService inputService, IObjectPool objectPool) : base(assets, staticData, inputService, objectPool)
        {
            _assets = assets;
            _staticData = staticData;
            _inputService = inputService;
            _objectPool = objectPool;
        }
        
        public async Task<GameObject> CreateHero(Vector3 at)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetsAdress.Player);
            GameObject playerGameObject = InstantiateRegistered(prefab, at);

            PlayerData playerData = _staticData.ForPlayer(AssetsAdress.Player);
            
            PlayerAttack playerAttack = playerGameObject.GetComponent<PlayerAttack>();
            playerAttack.Construct(_inputService, _objectPool);
            playerAttack.Damage = playerData.Damage;
            
            PlayerMovement playerMovement = playerGameObject.GetComponent<PlayerMovement>();
            playerMovement.Construct(_inputService);
            playerMovement.Speed = playerData.Speed;
            playerMovement.JumpHeight = playerData.JumpHeight;
            playerMovement.MaxGravityMultiplier = playerData.MaxGravityMultiplier;

            PlayerHealth playerHealth = playerGameObject.GetComponent<PlayerHealth>();
                playerHealth.MaxHP = playerData.MaxHP;
            
            return playerGameObject;
        }

        
        public async Task<GameObject> CreateHud() =>
           await InstantiateRegisteredAsync(AssetsAdress.Hud);

       
    }
}