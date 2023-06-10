using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckAndFlipDirection : MonoBehaviour
{
	internal bool isFacingRight = true;

	private CharacterController2D characterController;

	private void Awake()
	{
		characterController = GetComponent<CharacterController2D>();
	}
	void Update()
    {
		if (characterController.moveDirection.x > 0 && !isFacingRight)
		{
			FlipDirection();
		}
		else if (characterController.moveDirection.x < 0 && isFacingRight)
		{
			FlipDirection();
		}
	}

	private void FlipDirection()
	{
		isFacingRight = !isFacingRight;
		transform.Rotate(0, 180, 0);
	}

 

}
