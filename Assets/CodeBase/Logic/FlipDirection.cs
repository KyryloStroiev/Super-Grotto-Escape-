using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic
{
    public class FlipDirection : MonoBehaviour
    {
        public bool IsFacingRight = true;
        
        protected void CheckDirection(float horizontalVelosity)
        {
            if (horizontalVelosity> 0 && !IsFacingRight)
            {
                Flip();
            }
            else if (horizontalVelosity < 0 && IsFacingRight)
            {
                Flip();
            }
        }
        protected void Flip()
        {
            IsFacingRight = !IsFacingRight;

            transform.Rotate(0,180,0);
        }
    }
}