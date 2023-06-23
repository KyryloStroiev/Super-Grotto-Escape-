using UnityEngine;
using Zenject;

public class Bullet_Enemy : MonoBehaviour
{
	[SerializeField] float damage = 2;
    [SerializeField] private float speed = 40;

	private const string Fireball = "Fireball";
	private const string BulletEffect = "BulletEffect";
	private PlayerHealth playerHealth;
	private EnemyLogic enemyLogic;
	private Rigidbody2D rb;
	private Vector2 playerOrientation;

	[Inject]
	public void Construct(PlayerHealth playerHealth, EnemyLogic enemyLogic)
	{
		this.playerHealth = playerHealth;
		this.enemyLogic = enemyLogic;
	}
	private void OnEnable()
	{
		SetupBullet();
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

		if (player != null)
		{
			player.TakeDamage(damage);

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
