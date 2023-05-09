using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    public float damageEnemy = 10;
    public Animator animator;
    public bool canTakeDamage = true;
	public float damageDelay = 2f;
	void Start()
    {
        
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if( collision.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            TakeDamage(damageEnemy);
            Debug.Log("You take damage");
            

        }
	}
    IEnumerator TakeDamageDelay()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageDelay);
        canTakeDamage = true;
		
	}
}
