using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private float slideSpeed = 25f;
	private float slideTime = 0.2f;

	private bool isSliding = false;
	
    private Rigidbody2D rb;
    private Animator animator;

	private CharacterController2D characterController;
	private CheckAndFlipDirection checkAndFlip;
	private void Awake()
	{
		characterController = GetComponent<CharacterController2D>();
		checkAndFlip = GetComponent<CheckAndFlipDirection>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding && characterController.isGrounded)
		{
			StartCoroutine(PrefromSlide());
		}
			
		animator.SetBool("isSlide", isSliding);
	}

	IEnumerator PrefromSlide()
	{
		isSliding = true;
		Vector2 slideDirection = checkAndFlip.isFacingRight ? Vector2.right : Vector2.left;
		float t = 0f;
		float initialSpeed = characterController.moveSpeed;
		characterController.moveSpeed = slideSpeed;

		while (t < slideTime)
		{
			t += Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(slideTime);

		characterController.moveSpeed = initialSpeed;
		isSliding = false;


	}

	
}
