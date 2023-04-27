using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    public float damageEnemy = 10;
    public Animator animator;
    void Start()
    {
        
    }

    public void TakeDamage( float damage)
    {
        health -= damage;
		animator.SetBool("isDamage", true);
        animator.SetBool("isJumping", false);
		if (health < 0)
        {
            Die();
        }
    }
    void Update()
    {
        
    }
    void Die()
    {
        Debug.Log("Game Over");
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if( collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(damageEnemy);
            Debug.Log("You take damage");
           

        }
	}

    void  AnimatorDamage()
    {
        animator.SetBool("isDamage", false);
    }
}
