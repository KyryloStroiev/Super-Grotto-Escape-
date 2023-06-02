using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    private float maxHealth;
    public float damageEnemy = 10;
    public float heart = 10;
    private Animator animator;
    internal bool canTakeDamage = true;
	private float damageDelay = 2f;
	private void Start()
	{
        maxHealth = health;
		animator = GetComponent<Animator>();
	}

	public void TakeDamage( float damage)
    {
        health -= damage;
        animator.SetTrigger("Damage");
		StartCoroutine(TakeDamageDelay());
		if (health < 0)
        {
            Die();
        }
        else if (health >= maxHealth)
			health = maxHealth;

	}
	void Doctor()
    {
        health += heart;
    }
	void Update()
    {
        if(!canTakeDamage)
        {
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * 10.0f));
            GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,alpha);

        }
        else
        {
			GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
    }
    void Die()
    {
        Debug.Log("Game Over");
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("ObjectDamage"))
        {
			TakeDamage(damageEnemy);
		}
        if(collision.gameObject.CompareTag("Heart"))
        {
            Doctor();
            Destroy(collision.gameObject);
        }
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if( collision.gameObject.CompareTag("Enemy")  && canTakeDamage)
        {
            TakeDamage(damageEnemy);
        }
	}
    IEnumerator TakeDamageDelay()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageDelay);
        canTakeDamage = true;
		
	}
}
