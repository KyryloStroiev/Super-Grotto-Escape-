using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service.PersistentProgress;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Service.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IPlayerFactory _playerFactory;
        private readonly IEnemyFactory _enemyFactory;
        private const string ProgressKey = "Progress";
        
        [Inject]
        public SaveLoadService(IPersistentProgressService progressService, IPlayerFactory playerFactory, IEnemyFactory enemyFactory)
        {
            _progressService = progressService;
            _playerFactory = playerFactory;
            _enemyFactory = enemyFactory;
   
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _playerFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);
            
            foreach (ISavedProgress progressWriter in _enemyFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);
            
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
    }
}