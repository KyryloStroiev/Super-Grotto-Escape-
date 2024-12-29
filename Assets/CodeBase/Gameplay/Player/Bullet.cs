using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.ObjectPool;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Bullet: MonoBehaviour, IPoolable
    {
        [SerializeField] private float _speed = 10f;
        private float _damage;
        
        public Rigidbody2D Rigidbody;
        private IObjectPool _objectPool;
        private string _assetsAddress;

        public void Construct(IObjectPool objectPool, string assetsAddress)
        {
            _assetsAddress = assetsAddress;
            _objectPool = objectPool;
        }

        public void StartShoot(Vector2 direction, float damage)
        {
            _damage = damage;
            Rigidbody.linearVelocity = _speed * direction;

            if (Mathf.Sign(direction.x) < 0) 
                transform.Rotate(0, 180, 0);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            IHealth healthComponent = collider.gameObject.GetComponent<IHealth>();
            
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(_damage);
            }

            _objectPool.GetPooledObject(AssetsAdress.BulletEffect, transform.position);
            _objectPool.ReturnToPool(gameObject, _assetsAddress);
            
        }
    }
}