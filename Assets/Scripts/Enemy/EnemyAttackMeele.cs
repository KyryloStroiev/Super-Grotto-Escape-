
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class EnemyAttackMeele : MonoBehaviour
{
    [SerializeField] private float speedEnemy = 1.0f;
	[SerializeField] private float damage = 10.0f;
	[SerializeField] private float distanceAttack = 3.0f;

	private bool isFollowing = false;
    private bool isAttacking = false;
    private Animator animator;
    private PlayerHealth player;

    [Inject]
    private void Contract(PlayerHealth player )
    {
        this.player = player;
    }
    void Start()
    {
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
            Vector3 direction = player.transform.position - transform.position;
            float distance = direction.sqrMagnitude;
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
        player.TakeDamage(damage);  
	}

   
}
