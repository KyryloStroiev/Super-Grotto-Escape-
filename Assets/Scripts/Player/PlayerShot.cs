using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{

	public Transform shotPoints;
	public GameObject bulletPrefab;
	public Animator animator;
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Shot();
		}

	}
	void Shot()
	{
		
		animator.SetTrigger("Shot");
		

	}
	void ShotBull()
	{
		Instantiate(bulletPrefab, shotPoints.position, Quaternion.identity);

	}
}
