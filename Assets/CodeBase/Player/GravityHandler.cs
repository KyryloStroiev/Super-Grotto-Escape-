using UnityEngine;


namespace CodeBase.Player
{
    public class GravityHandler
    {
        private  float Gravity = -9.81f;
        
        private ColliderChecking _colliderChecking;
        
        public GravityHandler(ColliderChecking colliderChecking)
        {
            _colliderChecking = colliderChecking;
        }
        
        public void ApplyGravity(ref Vector2 direction, ref bool isJumping)
            {
                if (!_colliderChecking.IsLadder)
                {
                    direction.y += Gravity * Time.deltaTime;
                    direction.y = Mathf.Max(direction.y, Gravity * 8f);
                }
                
                if (direction.y < 0 && _colliderChecking.isGround)
                {
                    direction.y = -2f;
                    isJumping = false;
                }
                else if (_colliderChecking.isObstacleUp & !_colliderChecking.IsLadder)
                {
                    direction.y = -2f;
                }
                
            }
            
            
        }
}