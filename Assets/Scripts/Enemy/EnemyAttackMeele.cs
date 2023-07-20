
//using System.Runtime.CompilerServices;
//using UnityEngine;
//using Zenject;
//using Zenject.SpaceFighter;

//public class EnemyAttackMeele : MonoBehaviour
//{
//    [SerializeField] private float speedEnemy = 1.0f;
//	[SerializeField] private float damage = 10.0f;
//	[SerializeField] private float distanceAttack = 3.0f;

//	private bool isFollowing = false;
//    private bool isAttacking = false;
//    private Animator animator;
    
//    void Start()
//    {
//        animator = GetComponent<Animator>();
//    }

//    public void StartFollowing()
//    {
//        isFollowing = true;
//    }
//    public void StopFollowing()
//    {
//        isFollowing=false;
//    }
//    void Update()
//    {
//        if(player != null && isFollowing)
//        {
//            Vector3 direction = player.GetPosition() - transform.position;
//            float distance = direction.sqrMagnitude;
//            if(distance <= distanceAttack) 
//            {
//               isFollowing = false;
//               isAttacking = true;
//            }
//            else
//            {
//                isFollowing = true;
//                isAttacking = false;
//				direction.Normalize();
//				transform.position += direction * speedEnemy * Time.deltaTime;
//			}
            
//        }
//        animator.SetBool("isFollowing", isFollowing);
//        animator.SetBool("isAttack", isAttacking);
        
//    }

//    private void AttackPlayer()
//    {
//        player.Damage(damage);  
//	}

   
//}
