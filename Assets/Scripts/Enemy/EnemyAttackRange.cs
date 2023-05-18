using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
	public Transform shotPoints;
	public GameObject bulletPrefab;
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
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
