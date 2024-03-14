using System;
using CodeBase.Player;
using UnityEngine;

namespace CodeBase.Items
{
    public class Heart : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayer;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (playerLayer == (1 << other.gameObject.layer))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                playerHealth.Healing(playerHealth.MaxHP/4);
                Destroy(gameObject);
                
            }
        }
    }
}