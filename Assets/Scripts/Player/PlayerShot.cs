using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
	private const float bulletOffset = 0.3f;

	[SerializeField] private Transform shotPoints;
	[SerializeField] private Object bulletPrefab;

	private Animator animator;
	private CharacterController2D characterController;
	private PlayerInput input;

	private void Awake()
	{
		input = new PlayerInput();
		animator = GetComponent<Animator>();
		characterController = GetComponent<CharacterController2D>();
		input.Player.Shoot.performed += _ => Shoot();
	}

	void Shoot()	
	{
		if(characterController.isCrouch) 
		{
			animator.SetTrigger("ShootInCrouch");
		}
		else
		{
			animator.SetTrigger("Shoot");
		}
	}

	void ShootBull()
	{
		Vector3 bulletPosition = shotPoints.position;
		if(characterController.isCrouch)
		{
			bulletPosition -= shotPoints.up * bulletOffset;
		}
		Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);

	}

	private void OnEnable()
	{
		input.Enable();
	}
	private void OnDisable()
	{
		input.Disable();
	}
}
