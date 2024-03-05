using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic
{
    public class FlipDirection : MonoBehaviour
    {
        public bool IsFacingRight = true;
        
        protected void CheckDirection(float horizontalVelocity)
        {
            if (horizontalVelocity > 0 && !IsFacingRight || horizontalVelocity < 0 && IsFacingRight )
            {
                Flip();
            }
            
            /*if (!isLookLeft)
            {
               
            }
            else
            {
                if (horizontalVelocity < 0 && !IsFacingRight || horizontalVelocity > 0 && IsFacingRight)
                {
                    Flip();
                }
            }*/
           
        }
        public void Flip()
        {
            IsFacingRight = !IsFacingRight;

            transform.Rotate(0,180,0);
        }
    }
}