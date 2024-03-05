using System;
using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.Logic;
using TMPro;
using UnityEngine;

namespace CodeBase.Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public event Action HealthChanged;

        private float _invincibilityDuration = 2f;
        private float _blinkInterval = 0.2f;
        private bool _isInsensitive;

        private SpriteRenderer _spriteRenderer;
        private Color _originalColor;
        
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

        private void Awake()
        {
            _animator = GetComponent<PlayerAnimator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.color;
        }


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
            
            Debug.Log(_isInsensitive);
            if (!_isInsensitive)
            {
                CurrentHP -= damage;
                _animator.PlayHit();
                StartCoroutine(InvincibilityCoroutine());
            }
            
            
        }

        public void Healing(float health)
        {
            CurrentHP += health;
            if (CurrentHP >= MaxHP)
            {
                CurrentHP = MaxHP;
            }
        }

        private IEnumerator InvincibilityCoroutine()
        {
            _isInsensitive = true;
            
            float timer = 0f;
            while (timer < _invincibilityDuration)
            {
                _spriteRenderer.color = new Color(_originalColor.r, _originalColor.g, _originalColor.b, 0);
                yield return new WaitForSeconds(_blinkInterval);

                _spriteRenderer.color = _originalColor;
                yield return new WaitForSeconds(_blinkInterval);

                timer += _blinkInterval * 2f;
                Debug.Log(timer);
            }
            
            _isInsensitive = false;
        }
    }
}