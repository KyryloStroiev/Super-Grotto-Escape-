using CodeBase.UI.Service.Factory;
using Zenject;

namespace CodeBase.UI.Service.Menu
{
    public class MenuService : IMenuService
    {
        private IUIFactory _uiFactory;

        [Inject]
        public MenuService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(MenuId menuId)
        {
            switch (menuId)
            {
                case MenuId.Unknown:
                    break;
                case MenuId.MainMenu:
                    _uiFactory.CreateMenu();
                    break;
            }
        }
    }
}