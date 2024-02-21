using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Service.PersistentProgress;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Player
{
    public class PlayerTakeBattery : MonoBehaviour, ISavedProgress
    {
        public bool IsAllBatteryFound { get; set; }
        private int _maxBattery = 4;
        private int _numberOfBatteriesFound = 0;

        public int NumberOfBatteriesFound
        {
            get => _numberOfBatteriesFound;
            set
            {
                if (value != _numberOfBatteriesFound)
                {
                    _numberOfBatteriesFound = value;
          
                    BatteryChanged?.Invoke();
                }
            }
        }


        public int MaxBattery => _maxBattery;

        public event Action BatteryChanged;

        public void Found()
        {
            
            ++NumberOfBatteriesFound;
            if (NumberOfBatteriesFound >= MaxBattery)
            {
                IsAllBatteryFound = true;
            }
            Debug.Log(IsAllBatteryFound);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            NumberOfBatteriesFound = progress.BatteryNumber.NumberOfBatteries;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.BatteryNumber.NumberOfBatteries = NumberOfBatteriesFound;
        }
    }
}