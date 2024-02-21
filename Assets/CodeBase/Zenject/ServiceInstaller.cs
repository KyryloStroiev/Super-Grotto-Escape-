using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Factory.EnemyFactory;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Infrastructure.Service.SaveLoad;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.UI.Service.Factory;
using CodeBase.UI.Service.Menu;
using Zenject;

namespace CodeBase.Zenject
{
    public class ServiceInstaller: MonoInstaller
    {
        public override void InstallBindings()
        { 
            
            AssetProviderBind();
            PersistentProgressBind();
            PlayerFactoryBind();
            InputSystemBind();
            SaveLoadServiceBind();
            StaticDataServiceBind();
            EnemyFactoryBind();
            MenuServiceBind();
            ObjectPoolBind();
            UIFactoryBind();
        }

        private void UIFactoryBind()
        {
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();
        }

        private void MenuServiceBind()
        {
            Container
                .Bind<IMenuService>()
                .To<MenuService>()
                .AsSingle();
        }

        private void ObjectPoolBind()
        {
            Container
                .Bind<IObjectPool>()
                .To<ObjectPool>()
                .AsSingle();
        }

    
        private void EnemyFactoryBind()
        {
            Container
                .Bind<IEnemyFactory>()
                .To<EnemyFactory>()
                .AsSingle();
        }

        private void StaticDataServiceBind()
        {
            Container
                .Bind<IStaticDataService>()
                .To<StaticDataService>()
                .AsSingle();
        }

        private void SaveLoadServiceBind()
        {
            Container
                .Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle();
        }

        private void InputSystemBind()
        {
            Container
                .Bind<IInputService>()
                .To<InputService>()
                .AsSingle();
        }

        private void PersistentProgressBind()
        {
            Container
                .Bind<IPersistentProgressService>()
                .To<PersistentProgressService>()
                .AsSingle();
        }
        
        private void AssetProviderBind() =>
            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();

        private void PlayerFactoryBind() =>
            Container
                .Bind<IPlayerFactory>()
                .To<PlayerFactory>()
                .AsSingle();




    }
}