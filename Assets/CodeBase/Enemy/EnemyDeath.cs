using System;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyDeath: MonoBehaviour
    {
        public event Action Died;

        private EnemyHealth _health;
        private IObjectPool _objectPool;
        private EnemySound _sound;

        public void Construct(IObjectPool objectPool)
        {
            _objectPool = objectPool;
        }
        private void Start()
        {
            _health = GetComponent<EnemyHealth>();
            _sound = GetComponent<EnemySound>();
            _health.HealthChanged += HealtChanged;
        }

        private void HealtChanged()
        {
            if(_health.CurrentHP <= 0)
                Die();
        }

        private void Die()
        {
            _objectPool.GetPooledObject(AssetsAdress.ExplosionSmall, transform.position);
            _sound.PlayOneShot(EnemySoundType.Explosion);
            Died?.Invoke();
            Destroy(gameObject);
        }

        private void OnDestroy() => 
            _health.HealthChanged -= HealtChanged;
    }
}