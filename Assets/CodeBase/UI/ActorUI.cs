using System;
using CodeBase.Logic;
using CodeBase.Player;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar HpBar;

        private IHealth _health;
        
        public void Construct(IHealth health)
        {
            _health = health;

            _health.HealthChanged += UpdateHPBar;
            
        }
        
        private void UpdateHPBar()
        {
            HpBar.SetValue(_health.CurrentHP, _health.MaxHP);
        }

        private void OnDestroy() => 
            _health.HealthChanged -= UpdateHPBar;
    }
}