using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
	public GameObject bulletEffect;
	[SerializeField] private float damage = 40;
    [SerializeField] private float speed = 40;
	private Vector2 playerOrientation;
	private EnemyLogic enemyLogic;
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		playerOrientation = FindObjectOfType<PlayerShot>().transform.right;
		rb.velocity = transform.TransformDirection(playerOrientation) * speed;
        if(playerOrientation.x <0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(1,1,1);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.gameObject.CompareTag("Enemy"))
		{
			EnemyLogic enemyLogic = collision.gameObject.GetComponent<EnemyLogic>();
			if (enemyLogic != null)
			{
				enemyLogic.TakeDamage(damage);
			}
		}

		Instantiate(bulletEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	
   

}
