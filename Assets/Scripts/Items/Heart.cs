using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Heart : MonoBehaviour
{
    [SerializeField] private float healing = 25.0f;

	private PlayerHealth playerHeart;

	[Inject]
	public void Construct(PlayerHealth playerHeart)
	{
		this.playerHeart = playerHeart;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
		if (player != null && playerHeart.health < playerHeart.maxHealth)
		{
			player.Doctor(healing);
			gameObject.SetActive(false);
		}
	}

}
