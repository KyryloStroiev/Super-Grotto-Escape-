using CodeBase.Player;
using ModestTree;
using UnityEngine;

namespace CodeBase.Items
{
    public class Battery : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayer;
        private bool _isTaken = false;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isTaken &&playerLayer == (1 << other.gameObject.layer))
            {
                other.GetComponent<PlayerTakeBattery>().Found();
                Destroy(gameObject);
                _isTaken = true;
            }
        }
    }
}