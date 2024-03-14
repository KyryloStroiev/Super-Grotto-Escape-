using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.Infrastructure.State;
using CodeBase.Player;
using CodeBase.StaticData;
using CodeBase.StaticData.Player;
using CodeBase.UI.Elements;
using CodeBase.UI.Service.Menu;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class PlayerFactory : GameFactory, IPlayerFactory
    {
        private IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private IInputService _inputService;
        private readonly IObjectPool _objectPool;
        private readonly IMenuService _menuService;
        private IGameStateMachine _gameStateMachine;

        public PlayerFactory(IAssetProvider assets, IStaticDataService staticData, IInputService inputService, IObjectPool objectPool, IMenuService menuService) :
            base(assets, staticData, inputService, objectPool, menuService)
        {
            _assets = assets;
            _staticData = staticData;
            _inputService = inputService;
            _objectPool = objectPool;
            _menuService = menuService;
        }

        public void Construct(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

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
            playerMovement.SlideDuration = playerData.SlideDuration;
            playerMovement.SlideSpeedMultiplier = playerData.SlideSpeedMultiplier;
            
            playerGameObject.GetComponent<PlayerLookUpDown>().Construct(_inputService);
            playerGameObject.GetComponent<PlayerCrouch>().Construct(_inputService);
            playerGameObject.GetComponent<PlayerDeath>().Construct(_gameStateMachine);
            
            return playerGameObject;
        }

        
        public async Task<GameObject> CreateHud()
        {
            GameObject hud = await InstantiateRegisteredAsync(AssetsAdress.Hud);

            foreach (OpenMenuButton openMenuButton in hud.GetComponentsInChildren<OpenMenuButton>())
            {
                openMenuButton.Construct(_menuService);
            }

            return hud;
        }
    }
}