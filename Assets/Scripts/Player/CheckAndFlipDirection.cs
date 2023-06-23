
using UnityEngine;

public class CheckAndFlipDirection : MonoBehaviour
{
	[HideInInspector] public bool isFacingRight = true;

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
