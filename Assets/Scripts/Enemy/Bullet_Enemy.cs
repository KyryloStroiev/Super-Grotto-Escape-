using System;
using UnityEngine;
using Zenject;

public class Bullet_Enemy : MonoBehaviour
{
	[SerializeField] private float damage = 10;
    [SerializeField] private float speed = 40;

	private const string Fireball = "Fireball";
	private const string BulletEffect = "BulletEffect";
	private PlayerMovement playerMovement;
	private PlayerHealth playerHealth;
	private EnemyLogic enemyLogic;
	private AudioManager audioManager;
	private Rigidbody2D rb;
	private Vector2 playerOrientation;

	public event Action<float> DamageHandler;

	[Inject]
	public void Construct( EnemyLogic enemyLogic, AudioManager audioManager,
		PlayerHealth playerHealth)
	{
		this.audioManager = audioManager;
		this.enemyLogic = enemyLogic;
		this.playerHealth = playerHealth;
	}
	private void OnEnable()
	{
		SetupBullet();
		audioManager.Play("Fireball");
	}
	private void OnDisable()
	{
		audioManager.Stop("Fireball");
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{

	
		if (playerHealth.gameObject == collision.gameObject) 
		{
			playerHealth.TakeDamage(damage);
		}
		ObjectPooler.Instance.ReturnToPool(Fireball, gameObject);
		ObjectPooler.Instance.SpawnFromPool(BulletEffect, transform.position, Quaternion.identity);
	}

	private void SetupBullet()
	{ 
		rb = GetComponent<Rigidbody2D>();
		playerOrientation = enemyLogic.transform.right;
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
