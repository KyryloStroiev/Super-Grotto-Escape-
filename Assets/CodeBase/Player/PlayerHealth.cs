using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public event Action HealthChanged;
        
        private PlayerState _state;
        private PlayerAnimator _animator;
        public float CurrentHP
        {
            get => _state.CurrentHP;
            set
            {
                if (value != _state.CurrentHP)
                {
                    _state.CurrentHP = value;
          
                    HealthChanged?.Invoke();
                }
            }
        }

        public float MaxHP
        {
            get => _state.MaxHP;
            set => _state.MaxHP = value;
        }

        private void Awake() => 
            _animator = GetComponent<PlayerAnimator>();
        

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.PlayerState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (progress.PlayerState.CurrentHP != null)
            {
                progress.PlayerState.CurrentHP = CurrentHP;
                progress.PlayerState.MaxHP = MaxHP;
            }
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHP <= 0)
                return;
            
            CurrentHP -= damage;
            _animator.PlayHit();
        }

        public void Healing(float health)
        {
            CurrentHP += health;
            if (CurrentHP >= MaxHP)
            {
                CurrentHP = MaxHP;
            }
        }
        
    }
}