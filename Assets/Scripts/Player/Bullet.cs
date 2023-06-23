
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float damage = 40;
    [SerializeField] private float speed = 40;

	private const string Bullets = "Bullet";
	private const string BulletEffect = "BulletEffect";
	private Vector2 playerOrientation;

	private CharacterController2D characterController;
	private EnemyLogic enemyLogic;
	private Rigidbody2D rb;

	[Inject]
	private void Construct(CharacterController2D characterController, EnemyLogic enemyLogic)
	{
		this.enemyLogic = enemyLogic;
		this.characterController = characterController;
	}
	private void OnEnable()
    {
		SetupBullet();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		EnemyLogic enemy = collision.gameObject.GetComponent<EnemyLogic>();
		if (enemy !=null)
			enemy.TakeDamage(damage);

		ObjectPooler.Instance.ReturnToPool(Bullets, gameObject);
		ObjectPooler.Instance.SpawnFromPool(BulletEffect, transform.position, Quaternion.identity);
	}

	private void SetupBullet()
	{
		rb = GetComponent<Rigidbody2D>();
		playerOrientation = characterController.transform.right;
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
