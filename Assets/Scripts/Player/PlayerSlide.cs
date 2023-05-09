using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public float slideSpeed = 25f;
	private bool isSliding = false;
	public float slideCooldown = 0.5f;
	private float slideTime = 0.2f;
	private CharacterController2D characterController;

    private Rigidbody2D rb;
    private Animator animator;
    public Collider2D slideCollider; // Колайдер який прибираємо при підкаті

	private void Awake()
	{
		characterController = GetComponent<CharacterController2D>();
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
		Vector2 slideDirection = characterController.isFacingRight ? Vector2.right : Vector2.left; // отримуємо напрямок підкату
		 // тривалість підкату (в секундах)
		float t = 0f;
		float initialSpeed = characterController.moveSpeed;
		slideCollider.enabled = false;
		characterController.moveSpeed = slideSpeed;
		while (t < slideTime)
		{
			t += Time.deltaTime;
			
			yield return null;
		}
		characterController.moveSpeed = initialSpeed;
		slideCollider.enabled = true;
	    isSliding = false;
	}

	
}
