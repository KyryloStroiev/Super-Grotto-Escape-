using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
	public GameObject bulletEffect;
	[SerializeField] float damage = 2;
    [SerializeField] private float speed = 40;
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
		if (collision.gameObject.CompareTag("Player") && playerHealth.canTakeDamage)
		{
			playerHealth.TakeDamage(damage);
		}
		Instantiate(bulletEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}




}
