using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
	public GameObject bulletEffect;
	public float damage = 2;
    [SerializeField] private float speed = 40;
	void Start()
    {
		rb.velocity = transform.right * speed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
		if (collision.gameObject.CompareTag("Player"))
		{
			playerHealth.TakeDamage(damage);
			Debug.Log("Shot fot Player");
		}
		Instantiate(bulletEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}




}
