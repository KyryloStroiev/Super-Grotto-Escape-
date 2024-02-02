using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Enemy
{
    public class EnemyPlayerChecking : MonoBehaviour
    {
        
        public float DistanceForward { get; set; }
        public float DistanceBack { get; set; }
        
        public BoxCollider2D BoxCollider;
        
        public bool IsPlayerFound { get; private set; }
        public RaycastHit2D Hit { get; private set; }

        private int _layerMask;
        private const string LayerName = "Player";

        private void Awake() => 
            _layerMask = 1 << LayerMask.NameToLayer(LayerName);

        private void Update()
        {
           IsPlayerFound = CheckCollision(Vector2.left, DistanceBack) ||
            CheckCollision(Vector2.right, DistanceForward);
        }

        private bool CheckCollision(Vector2 raycastDirection, float raycastDistance)
        {
            Vector2 rotatedDirection = RotateDirection(raycastDirection);

            Vector2 raycastOrigin = PlayerCenter() + rotatedDirection * RendererExtents();
            
            Hit = Physics2D.Raycast(raycastOrigin, rotatedDirection, raycastDistance, _layerMask);
            
            Debug.DrawRay(raycastOrigin, rotatedDirection * raycastDistance, Color.red);
            
            return Hit.collider;
        }

        private Vector2 PlayerCenter() => 
            (Vector2)transform.position + BoxCollider.offset;

        private Vector3 RotateDirection(Vector2 raycastDirection) => 
            Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0 ) * raycastDirection;

        private float RendererExtents() => 
            BoxCollider.bounds.size.x * 0.5f + 0.01f;
    }
}