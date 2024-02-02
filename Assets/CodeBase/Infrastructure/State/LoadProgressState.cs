using System;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.SaveLoad;
using CodeBase.Infrastructure.Service.StaticDataService;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.State
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticData;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService, IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticData = staticData;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
            
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress = 
                _saveLoadService.LoadProgress() 
                ?? NewProgress();

        private PlayerProgress NewProgress()
        {
          PlayerProgress progress = new PlayerProgress(initialLevel: LevelId.Level_1.ToString());
          PlayerData playerData = _staticData.ForPlayer(AssetsAdress.Player);
          progress.PlayerState.MaxHP = playerData.MaxHP;
          progress.PlayerState.CurrentHP = playerData.MaxHP;
          progress.PlayerState.ResetHP();

          return progress;
        }
    }
}