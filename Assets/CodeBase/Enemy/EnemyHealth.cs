using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public float CurrentHP { get; set; }
        public float MaxHP { get; set; }

        public event Action HealthChanged;

        private void Awake()
        {
            CurrentHP = MaxHP;
        }

        public void TakeDamage(float damage)
        {
            CurrentHP -= damage;
           
            HealthChanged?.Invoke();
        }
    }
}