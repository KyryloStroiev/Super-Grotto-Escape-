using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
   
    [Range(1,50)] [SerializeField] private float maxDistance;
    public LayerMask obstacleLayerMask;
    public Transform firePoint;
    private EnemyPatrol enemyPatrol;
    private EnemyAttackDistance enemyAttackDistance;
    public Animator animator;
    public bool rangeAttack = false;

	void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
        enemyAttackDistance = GetComponent<EnemyAttackDistance>();
    }

    
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, maxDistance, obstacleLayerMask);
        Debug.DrawRay(firePoint.position, firePoint.right * maxDistance, Color.red);
        if (hit)
        {
            enemyPatrol.enabled = false;
            animator.SetBool("isWalk", false);
            enemyAttackDistance.Shot();

        }
        else if (!hit)
        {
            enemyPatrol.enabled = true;
            animator.SetBool("isWalk", true);
		}

    }
   
   
}
