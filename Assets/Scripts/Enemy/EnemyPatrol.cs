using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
	public float patrolSpeed = 3f;
    [Space]
	public Transform startPosition;
    public Transform endPosition;
    public bool rotateEnemy;
	private Animator animator;
	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
    {
        if (startPosition != null && endPosition != null)
        {
            animator.SetBool("isWalk", true);
            transform.position = Vector2.MoveTowards(transform.position,
                endPosition.position, patrolSpeed * Time.deltaTime);
            Vector2 direction = (endPosition.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (rotateEnemy)
                transform.rotation = Quaternion.Euler(0, angle, 0);
            if (Vector2.Distance(transform.position, endPosition.position) < 1f)
            {
                Transform temp = endPosition;
                endPosition = startPosition;
                startPosition = temp;
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }
	
}
