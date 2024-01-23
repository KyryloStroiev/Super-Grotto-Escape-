using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Player
{
    public class Bullet: MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        private float _damage;
        
        public Rigidbody2D Rigidbody;
        
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

            if (healthComponent != null)
            {
                healthComponent.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}