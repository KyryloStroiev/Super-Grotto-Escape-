using UnityEngine;
using Zenject;

public class EnemyLogic : MonoBehaviour
{
	[SerializeField] private float touchDamage = 11f;
	[SerializeField] private float health;
    [SerializeField] private int points;

	private const string ExplosionSmall = "ExplosionSmall";

	private GameManager gameManager;
	private PlayerHealth playerHealth;
	[Inject]
	public void Construct(GameManager gameManager, PlayerHealth playerHealth)
	{
		this.playerHealth = playerHealth;
		this.gameManager = gameManager;
	}
	private void Awake()
	{
	}
	public void Damage(float damage)
    {

        health -= damage;
        if (health <= 0)
        {
            gameManager.Scoring(points);
			Die();
           
        }
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{

		if (playerHealth.gameObject == collision.gameObject)
		{
			playerHealth.TakeDamage(touchDamage);
		}
	}

	void Die()
    {
		
		ObjectPooler.Instance.SpawnFromPool(ExplosionSmall, transform.position, Quaternion.identity);
        Destroy(gameObject);
	}
	public Vector3 GetPosition()
	{
		return transform.position;
	}

}
