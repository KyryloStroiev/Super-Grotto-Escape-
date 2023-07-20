
using System;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float damage = 40;
    [SerializeField] private float speed = 40;

	private const string Bullets = "Bullet";
	private const string BulletEffect = "BulletEffect";
	private Vector2 playerOrientation;
	private const string Fireball = "Fireball";

	private PlayerMovement playerMovement;
	private EnemyLogic enemyLogic;
	private Rigidbody2D rb;

	public event Action<float> DamageHandler;

	[Inject]
	private void Construct(PlayerMovement playerMovement, EnemyLogic enemyLogic )
	{
		this.enemyLogic = enemyLogic;
		this.playerMovement = playerMovement;
	}
	private void OnEnable()
    {
		SetupBullet();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (enemyLogic.gameObject == collision.gameObject)
			enemyLogic.Damage(damage);

		ObjectPooler.Instance.ReturnToPool(Bullets, gameObject);
		ObjectPooler.Instance.SpawnFromPool(BulletEffect, transform.position, Quaternion.identity);
	}

	private void SetupBullet()
	{
		rb = GetComponent<Rigidbody2D>();
		playerOrientation = playerMovement.transform.right;
		rb.velocity = transform.TransformDirection(playerOrientation) * speed;
		if (playerOrientation.x < 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		
	}
   

}
