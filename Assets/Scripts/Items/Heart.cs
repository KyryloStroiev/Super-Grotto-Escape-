using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Heart : MonoBehaviour
{
    [SerializeField] private float healing = 25.0f;
	private PlayerHealth playerHealth;
	[Inject]
	private void Construct(PlayerHealth playerHealth)
	{
		this.playerHealth = playerHealth;
	}
	private void Awake()
	{
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		playerHealth.Heal(healing);
		gameObject.SetActive(false);

	}

}
