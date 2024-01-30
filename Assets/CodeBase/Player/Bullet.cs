using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service.ObjectPool;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Player
{
    public class Bullet: MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        private float _damage;
        
        public Rigidbody2D Rigidbody;
        private IObjectPool _objectPool;

        public void Construct(IObjectPool objectPool) => 
            _objectPool = objectPool;

        public void StartShoot(Vector2 direction, float damage)
        {
            _damage = damage;
            Rigidbody.velocity = _speed * direction;

            if (Mathf.Sign(direction.x) < 0) 
                transform.Rotate(0, 180, 0);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            IHealth healthComponent = collider.gameObject.GetComponent<IHealth>();

            Debug.Log(_damage);
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(_damage);
                Debug.Log($"Hit {collider.name}");
            }
            
            _objectPool.ReturnToPool(gameObject, AssetsAdress.PlayerBullet);
            
        }
    }
}