using CodeBase.Logic;
using CodeBase.Player;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class PlayerUI : ActorUI
    {
        public HpBar BatteryBar;
        private PlayerTakeBattery _battery;

        public void Construct(PlayerTakeBattery battery)
        {
            _battery = battery;
            _battery.BatteryChanged += UpdateBatteryBar;
        }
        
        private void UpdateBatteryBar()
        {
            BatteryBar.SetValue(  (float)_battery.NumberOfBatteriesFound, (float)_battery.MaxBattery);
        }
    }
}   