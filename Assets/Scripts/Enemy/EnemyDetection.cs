using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetection : MonoBehaviour
{
    [Header("Enemy detection distance")]
    [Range(1,50)] [SerializeField] private float forward;
	[Range(1, 20)][SerializeField] private float back;
    [Space]
	public LayerMask obstacleLayerMask;
    public Transform firePointForward;
    public Transform firePointBack;
    private EnemyPatrol enemyPatrol;
    private Animator animator;
    public UnityEvent attackorFollowingPlayer;
    public UnityEvent stopFollowingPlayer;
    private bool isWalk;
	void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        RaycastHit2D hitForward = Physics2D.Raycast(firePointForward.position, firePointForward.right, forward, obstacleLayerMask);
        RaycastHit2D hitBack = Physics2D.Raycast(firePointBack.position, -firePointBack.right, back, obstacleLayerMask);
		Debug.DrawRay(firePointForward.position, firePointForward.right * forward, Color.red);
		Debug.DrawRay(firePointBack.position, -firePointBack.right * back, Color.green);
		if (hitForward)
        {
            enemyPatrol.enabled = false;
            isWalk = false;
            attackorFollowingPlayer.Invoke();

        }
		else if (hitBack)
		{
			transform.Rotate(0, 180, 0);
		}
		else if (!hitForward)
        {
            enemyPatrol.enabled = true;
            isWalk = true;
            stopFollowingPlayer.Invoke();
          
		}
		animator.SetBool("isWalk", isWalk);
	}
   
   
}
