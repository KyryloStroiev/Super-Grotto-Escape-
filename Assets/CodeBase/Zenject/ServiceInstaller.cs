using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.SaveLoad;
using CodeBase.Infrastructure.Service.StaticDataService;
using Zenject;

namespace CodeBase.Zenject
{
    public class ServiceInstaller: MonoInstaller
    {
        public override void InstallBindings()
        { 
            
            AssetProviderBind();
            PersistentProgressBind();
            GameFactoryBind();
            InputSystemBind();
            SaveLoadServiceBind();
            StaticDataServiceBind();
        }

        /*
        private void EnemyFactoryBind()
        {
            Container
                .Bind<IEnemyFactory>()
                .To<EnemyFactory>()
                .AsSingle();
        }
        */

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

        private void GameFactoryBind() =>
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();




    }
}