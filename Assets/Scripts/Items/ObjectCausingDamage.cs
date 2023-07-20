using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectCausingDamage : MonoBehaviour
{
    [SerializeField] private float damage = 8f;
	private PlayerHealth playerHealth;

	[Inject]
	private void Construct(PlayerHealth playerHealth)
	{
		this.playerHealth = playerHealth;
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(playerHealth.gameObject == collision.gameObject)
		{
			playerHealth.TakeDamage(damage);
		}
	}
}
