using CodeBase.Logic;
using UnityEngine;

public class TakeDamageItem : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerLayer == (1 << other.gameObject.layer))
        {
            var player = other.GetComponent<IHealth>();
            player.TakeDamage(player.MaxHP/16);
            Debug.Log(player.CurrentHP);
        }

        
    }
}   
