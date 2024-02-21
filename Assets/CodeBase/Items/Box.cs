using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Items
{
    public class Box : MonoBehaviour, IHealth
    {
        public float MaxHP { get; set; } = 2f;
        public float CurrentHP { get; set; }
        
        public GameObject ExplossionSmall;
        
        public event Action HealthChanged;
        public List<Sprite> _boxSprite = new();
        private SpriteRenderer _spriteRenderere;
        private IObjectPool _objectPool;

        [Inject]
        public void Construct(IObjectPool objectPool)
        {
            _objectPool = objectPool;
        }
        
        private void Awake()
        {
            CurrentHP = MaxHP;
            _spriteRenderere = GetComponent<SpriteRenderer>();
        }
        public void TakeDamage(float damage)
        {
            --CurrentHP;
            _spriteRenderere.sprite = _boxSprite[(int)CurrentHP];
            if (CurrentHP <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
            _objectPool.GetPooledObject(AssetsAdress.ExplosionSmall, transform.position);
        }
    }
}