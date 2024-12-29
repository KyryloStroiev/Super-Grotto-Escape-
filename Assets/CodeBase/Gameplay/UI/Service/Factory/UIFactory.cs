using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.StaticData.Menu;
using CodeBase.UI.Menu;
using CodeBase.UI.Service.Menu;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Service.Factory
{
    public class UIFactory :IUIFactory
    {
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticData;
        private GameObject _uiRoot;
        private DiContainer _container;
        
        [Inject]
        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData, DiContainer container)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _container = container;
        }


        public void CreateMenu()
        {
            MenuConfig config = _staticData.ForMenu(MenuId.MainMenu);
            GameObject menu = _container.InstantiatePrefab(config.Prefab, _uiRoot.transform);
            menu.GetComponent<MenuWindows>();
        }

        public async Task CreateUIRoot()
        {
            GameObject root = await _assetProvider.Instantiate(AssetsAdress.UIRoot);
            _uiRoot = root;
        }
    }
}