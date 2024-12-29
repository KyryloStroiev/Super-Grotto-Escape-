using UnityEngine;

namespace CodeBase.Logic
{
    public class DeathTrigger: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            IHealth health = other.gameObject.GetComponent<IHealth>();

            if (health != null)
            {
                health.TakeDamage(health.MaxHP);
            }
        }
    }
}