using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;
    public Animator animator;
    public float speed = 3f;
    public bool rotateEnemy;
    void Start()
    {
        
    }

    void Update()
    {
        if (startPosition != null && endPosition != null)
        {
            animator.SetBool("isWalk", true);
            transform.position = Vector2.MoveTowards(transform.position,
                endPosition.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, endPosition.position) < 1f)
            {
                if (rotateEnemy)
                    transform.Rotate(0, 180, 0);
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
