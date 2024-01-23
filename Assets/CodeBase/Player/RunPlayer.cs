using UnityEngine;

namespace CodeBase.Player
{
    public class RunPlayer
    {
        private ColliderChecking _colliderChecking;

        public RunPlayer(ColliderChecking colliderChecking)
        {
            _colliderChecking = colliderChecking;
        }


        public float Run(float direction, float input, float speed)
        {
            direction  = input * speed;
            
            if (_colliderChecking.isObstacleRight)
                direction = Mathf.Clamp(direction, float.MinValue, 0);
            else if (_colliderChecking.isObstacleLeft)
                direction = Mathf.Clamp(direction, 0, float.MaxValue);

            return direction;
        }
    }
}