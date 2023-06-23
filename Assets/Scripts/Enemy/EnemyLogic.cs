using UnityEngine;
using Zenject;

public class EnemyLogic : MonoBehaviour
{
	[SerializeField] private float touchDamage = 11f;
	[SerializeField] private float health;
    [SerializeField] private int points;

	private const string ExplosionSmall = "ExplosionSmall";

	private GameManager gameManager;
    private PlayerHealth player;

	[Inject]
	public void Construct(GameManager gameManager, PlayerHealth player)
	{
		this.gameManager = gameManager;
        this.player = player;
	}
	public void TakeDamage(float damage)
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
		PlayerHealth players = collision.gameObject.GetComponent<PlayerHealth>();

		if (players != null && player.canTakeDamage)
		{
			player.TakeDamage(touchDamage);
		}
	}

	void Die()
    {
		
		ObjectPooler.Instance.SpawnFromPool(ExplosionSmall, transform.position, Quaternion.identity);
        Destroy(gameObject);
	}


}
