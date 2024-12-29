using System;
using CodeBase.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace CodeBase.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        public float Speed { get; set; }
        
        public bool IsFlying { get; set; }
        
        public Rigidbody2D Rigidbody;
 
        private Vector2 _direction;
        public float HorizontalVelocity => _direction.x;
        
        public void Move(Vector3 moveTo)
        {
            _direction = (moveTo - gameObject.transform.position).normalized;

            if (!IsFlying)
            {
                _direction.y = -2f;
            }
            
            Rigidbody.MovePosition(Rigidbody.position + (_direction * Speed * Time.fixedDeltaTime));

        }
        
    }
}