using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyDeath: MonoBehaviour
    {
        
        public GameObject ExplossionSmall;
        
        public event Action Died;

        private EnemyHealth _health;

        private void Start()
        {
            _health = GetComponent<EnemyHealth>();
            _health.HealthChanged += HealtChanged;
        }

        private void HealtChanged()
        {
            Debug.Log(_health.CurrentHP);
            if(_health.CurrentHP <= 0)
                Die();
        }

        private void Die()
        {
            Instantiate(ExplossionSmall, transform.position, Quaternion.identity);
            Died?.Invoke();
            Destroy(gameObject);
        }

        private void OnDestroy() => 
            _health.HealthChanged -= HealtChanged;
    }
}