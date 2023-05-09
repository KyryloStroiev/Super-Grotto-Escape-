using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetection : MonoBehaviour
{
   
    [Range(1,50)] [SerializeField] private float maxDistance;
    public LayerMask obstacleLayerMask;
    public Transform firePoint;
    private EnemyPatrol enemyPatrol;
    public Animator animator;
    public UnityEvent attackPlayer;
    public bool rangeAttack = false;
    public bool isWalk;
	void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, maxDistance, obstacleLayerMask);
        Debug.DrawRay(firePoint.position, firePoint.right * maxDistance, Color.red);
        if (hit)
        {
            enemyPatrol.enabled = false;
            isWalk = false;
            attackPlayer.Invoke();

        }
        else if (!hit)
        {
            enemyPatrol.enabled = true;
            isWalk = true;
		}
		animator.SetBool("isWalk", isWalk);
	}
   
   
}
