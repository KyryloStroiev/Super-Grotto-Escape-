
using UnityEngine;

public class CheckAndFlipDirection : MonoBehaviour
{
	[HideInInspector] public bool isFacingRight = true;

	private PlayerMovement playerMovement;

	private void Awake()
	{
		playerMovement = GetComponent<PlayerMovement>();
	}
	void Update()
    {
		if (playerMovement.moveDirection.x > 0 && !isFacingRight)
		{
			FlipDirection();
		}
		else if (playerMovement.moveDirection.x < 0 && isFacingRight)
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
