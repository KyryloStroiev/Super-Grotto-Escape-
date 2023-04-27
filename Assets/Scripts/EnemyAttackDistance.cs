using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDistance : MonoBehaviour
{
	public Transform shotPoints;
	public GameObject bulletPrefab;
	public Animator animator;
	private EnemyPatrol enemyPatrol;
	public float attackDelay = 1;
	public void Shot()
	{
		animator.SetBool("isShot", true);
	}

	public void ShotBullet()
	{
		Instantiate(bulletPrefab, shotPoints.position, shotPoints.rotation);
		animator.SetBool("isShot", false);
	}

}
