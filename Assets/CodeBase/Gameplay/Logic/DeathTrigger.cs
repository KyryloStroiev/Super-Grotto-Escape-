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
                Debug.Log("De   a");
                health.TakeDamage(health.MaxHP);
            }
        }
    }
}