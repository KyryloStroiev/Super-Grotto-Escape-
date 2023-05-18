using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{

	public Transform shotPoints;
	public GameObject bulletPrefab;
	private Animator animator;
	private CharacterController2D characterController;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		characterController = GetComponent<CharacterController2D>();
	}
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			if(characterController.isCrouch)
			{
				ShotInCrouch();
			}
			else
			{
				Shot();
			}
	
		}
	}
	void Shot()	
	{
		
		animator.SetTrigger("Shot");
		

	}
	void ShotInCrouch()
	{
		animator.SetTrigger("ShotInCrouch");
	}
	void ShotBull()
	{
		Vector3 bulletPosition = shotPoints.position;
		if(characterController.isCrouch)
		{
			bulletPosition -= shotPoints.up * 0.3f;
		}
		Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);

	}
}
