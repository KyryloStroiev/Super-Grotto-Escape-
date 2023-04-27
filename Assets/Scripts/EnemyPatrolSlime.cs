using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolSlime : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Animator animator;
    public float speed = 3f;
    public bool rotateEnemy;
	private int currentPatrolIndex = 0;
	void Start()
    {
       if(patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[currentPatrolIndex].position;
            UpdatePatrolDestination();
        } 
    }

    
    void Update()
    {
        if(patrolPoints.Length > 0)
        {
            //animator.SetBool("isWalk", true);
			transform.position = Vector2.MoveTowards(transform.position,
			   patrolPoints[currentPatrolIndex].position, speed * Time.deltaTime);

			if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
			{
				if (rotateEnemy)
					transform.Rotate(0, 0, 90);

				currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
				UpdatePatrolDestination();
			}
		}
     
    }
    void UpdatePatrolDestination()
    {
        int nextPatrolIndex = (currentPatrolIndex+1)% patrolPoints.Length;
        transform.up = patrolPoints[nextPatrolIndex].position - transform.position;
    }
	
}
