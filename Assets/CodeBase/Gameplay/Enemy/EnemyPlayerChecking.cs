using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Enemy
{
    public class EnemyPlayerChecking : MonoBehaviour
    {
        
        public float DistanceForward { get; set; }
        public float DistanceBack { get; set; }
        
        public CapsuleCollider2D CapsuleCollider;
        
        public bool IsPlayerFound { get; private set; }
        public bool IsPlayerBack { get; set; }
        public RaycastHit2D Hit { get; private set; }

        private int _layerMask;
        private const string LayerName = "Player";


        private void Awake() => 
            _layerMask = 1 << LayerMask.NameToLayer(LayerName);

        private void Update()
        {
           IsPlayerFound = PlayerBack() ||
            PlayerForward();
           
        }

        private bool PlayerForward() => 
            CheckCollision(Vector2.right, DistanceForward);

        public bool PlayerBack() => 
            CheckCollision(Vector2.left, DistanceBack);

        private bool CheckCollision(Vector2 raycastDirection, float raycastDistance)
        {
            Vector2 rotatedDirection = RotateDirection(raycastDirection);

            Vector2 raycastOrigin = PlayerCenter() + rotatedDirection * RendererExtents();
            
            Hit = Physics2D.Raycast(raycastOrigin, rotatedDirection, raycastDistance, _layerMask);
            
            Debug.DrawRay(raycastOrigin, rotatedDirection * raycastDistance, Color.red);
            
            return Hit.collider;
        }

        private Vector2 PlayerCenter() => 
            (Vector2)transform.position + CapsuleCollider.offset;

        private Vector3 RotateDirection(Vector2 raycastDirection) => 
            Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0 ) * raycastDirection;

        private float RendererExtents() => 
            CapsuleCollider.bounds.size.x * 0.5f + 0.01f;
    }
}