using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour
{
    public  float health = 100.0f;
	[HideInInspector] public float maxHealth;
    [HideInInspector] public bool canTakeDamage = true;
	private float damageDelay = 2f;
	private SpriteRenderer spriteRenderer;
    private PlayerAnimator playerAnimator;

    private void Start()
	{
		maxHealth = health;
		playerAnimator = GetComponent<PlayerAnimator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void TakeDamage( float damage)
    {
        health -= damage;
        playerAnimator.TakeDamage();
		StartCoroutine(TakeDamageDelay());
		if (health < 0)
        {
            Die();
        }
        else if (health >= maxHealth)
			health = maxHealth;
	}

	public void Doctor(float healing)
    {
        health += healing;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
	private void Die()
	{
		Debug.Log("Game Over");
	}

	IEnumerator TakeDamageDelay()
	{
		canTakeDamage = false;
		spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
		yield return new WaitForSeconds(damageDelay);
		canTakeDamage = true;
		spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}
}
