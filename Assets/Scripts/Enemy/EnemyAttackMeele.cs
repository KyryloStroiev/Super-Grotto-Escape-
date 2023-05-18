using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMeele : MonoBehaviour
{
    private Transform player;
    public float speedEnemy = 1.0f;
    public float damage = 10.0f;
    private bool isFollowing = false;
    private bool isAttacking = false;
    private Animator animator;
    public float distanceAttack = 3.0f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        animator = GetComponent<Animator>();
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }
    public void StopFollowing()
    {
        isFollowing=false;
    }
    void Update()
    {
        if(player != null && isFollowing)
        {
            Vector3 direction = player.position - transform.position;
            float distance = direction.magnitude;
            if(distance <= distanceAttack) 
            {
               isFollowing = false;
               isAttacking = true;
            }
            else
            {
                isFollowing = true;
                isAttacking = false;
				direction.Normalize();
				transform.position += direction * speedEnemy * Time.deltaTime;
			}
            
        }
        animator.SetBool("isFollowing", isFollowing);
        animator.SetBool("isAttack", isAttacking);
        
    }

    private void AttackPlayer()
    {
		PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(damage);
	}
}
