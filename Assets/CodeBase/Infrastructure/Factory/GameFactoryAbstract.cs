﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.Input;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.Infrastructure.Service.StaticDataService;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public abstract class GameFactoryAbstract
    {
        
        private IAssetProvider _assets;
        private IStaticDataService _staticData;
        private IInputService _inputService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressWriters { get; } = new();
        
        [Inject]
        public GameFactoryAbstract(IAssetProvider assets, IStaticDataService staticData, IInputService inputService)
        {
            _assets = assets;
            _staticData = staticData;
            _inputService = inputService;
        }
        
        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            _assets.CleanUp();
        }
        
        protected void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }

        protected void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
        
        protected GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }
        
        protected async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
        {
            GameObject gameObject = await _assets.Instantiate(prefabPath, at);
            
            RegisterProgressWatchers(gameObject);
            
            return gameObject;
        }

        protected async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            GameObject gameObject = await _assets.Instantiate(prefabPath);
            
            RegisterProgressWatchers(gameObject);
            
            return gameObject;
        }
    }
}