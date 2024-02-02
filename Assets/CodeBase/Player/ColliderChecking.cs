using UnityEngine;

namespace CodeBase.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ColliderChecking : MonoBehaviour
    {
        private const string LayerGround = "Ground";
        private const string LayerLadder = "Ladder";
        public BoxCollider2D BoxCollider;
        
        [SerializeField] private float circleRadius;
        [SerializeField] private float circleOffsetY;


        public bool isGround;
        public bool isObstacleUp { get; private set; }
        public bool isObstacleRight { get; private set; }
        public bool isObstacleLeft { get; private set; }

        public bool IsLadder { get; set; }
        private float raycastDistance = 0.2f;
        private int _layerMaskGround;
        private int _layerMaskLadder;


        private void Awake()
        {
            _layerMaskGround = 1 << LayerMask.NameToLayer(LayerGround);
            _layerMaskLadder = 1 << LayerMask.NameToLayer(LayerLadder);
        }

       

        private void Update()
        {
            isGround = CheckGround(_layerMaskGround);
            isObstacleUp = CheckCollision(Vector2.up, BoxCollider.bounds.size.y * 0.5f);
            isObstacleLeft = CheckCollision(Vector2.left, BoxCollider.bounds.size.x * 0.5f);
            isObstacleRight = CheckCollision(Vector2.right, BoxCollider.bounds.size.x * 0.5f);

            IsLadder = CheckGround(_layerMaskLadder);
            
            Debug.Log(IsLadder);
        }

        private bool CheckCollision(Vector2 raycastDirection, float rendererExtents)
        {
            Vector2 playerCenter = (Vector2)transform.position + BoxCollider.offset;
            Vector2 raycastOrigin = playerCenter + raycastDirection * (rendererExtents + 0.01f);
            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance, _layerMaskGround);
            Debug.DrawRay(raycastOrigin, raycastDirection * raycastDistance, Color.red);
            return hit;
        }
 

        private bool CheckGround(int layerMask)
        {
            Vector2 playerCenter = (Vector2)transform.position + new Vector2(0.0f, circleOffsetY);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(playerCenter, circleRadius, layerMask);
            foreach (Collider2D collider in colliders)
            {
                if (collider != null && collider.gameObject != gameObject)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Vector3 playerCenter = transform.position + new Vector3(0.0f, circleOffsetY, 0.0f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerCenter, circleRadius);
        }
  
    }
}
