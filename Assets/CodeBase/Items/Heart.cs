using System;
using CodeBase.Player;
using UnityEngine;

namespace CodeBase.Items
{
    public class Heart : MonoBehaviour
    {
        [SerializeField] private float health = 1;
        [SerializeField] private LayerMask playerLayer;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (playerLayer == (1 << other.gameObject.layer))
            {
                other.GetComponent<PlayerHealth>().Healing(health);
                Destroy(gameObject);
                
            }
        }
    }
}