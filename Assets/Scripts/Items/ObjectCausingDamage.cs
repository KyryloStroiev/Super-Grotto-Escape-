using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectCausingDamage : MonoBehaviour
{
    [SerializeField] private float damage = 8.5f;

    private PlayerHealth playerHealth;

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        this.playerHealth = playerHealth;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
	}
}
