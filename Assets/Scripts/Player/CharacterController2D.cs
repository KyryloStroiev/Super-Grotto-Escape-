using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	


	public  float moveSpeed = 5f;
	[SerializeField] private float jumpForce = 17f;
	[SerializeField] private float climpSpeed = 3f;
	private float gravityScale = 3f;
	private  float horizontalInput;
	private float verticalInput;
/*	[Range(0, .3f)][SerializeField] private float movementSmooting = .05f;*/ // На скільки згладити рух

	internal bool isGrounded;
	[SerializeField] private bool inFlight = false; //Зміна яка вмикається після того як гравець підстрибнув і не знаходиться на землі
	private bool isOnLadder; // Підьйом на драбини чи інші 
	internal bool isLookingUp = false;
	private bool beginsToRise = false; // Становиться на сходи
	[SerializeField] private LayerMask whatIsGround; //Слой який визначає землю 
	[SerializeField] private Transform pointGroundCheck; //Точка з якої роблять коло яке оприділяє слой землі
	const float groundedRadius = .2f; // Радіус кола для зіткнення з землею
	private Vector3 m_velosity = Vector3.zero;
	internal bool isFacingRight = true;
	internal bool isCrouch = false;
	private Rigidbody2D rb;
	private Animator animator;
	[SerializeField] private Collider2D crouchDisableCollider; //Колайдер який вимикається коли гравець сковзить
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");

		if (Input.GetButtonDown("Jump") && isGrounded )
		{
			rb.velocity = new Vector2 (horizontalInput, jumpForce);
		}
		if (!isGrounded && !isOnLadder)
		{
			inFlight = true;
		}
		else
		{
			inFlight = false;
		}
		animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
		animator.SetBool("Jump", inFlight);
		
	}
	private void FixedUpdate()
	{
		isGrounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(pointGroundCheck.position, groundedRadius, whatIsGround);
		for(int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject !=gameObject)
			{
				isGrounded = true;
			}
		}
		if(horizontalInput > 0 && !isFacingRight)
		{
			Flip();
		}
		else if(horizontalInput < 0 && isFacingRight)
		{
			Flip();
		}
		OnLadder();

	}
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Ladder"))
		{
			isOnLadder = true;	
		}
	
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Ladder"))
		{
			isOnLadder = false;
			beginsToRise = false;
		}
	}

	void OnLadder()
	{
		
		if (isOnLadder)
		{
			rb.gravityScale = 0;
			
			rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * climpSpeed);
			isLookingUp = false;

			if(!isGrounded)
			{
				beginsToRise = true;
			}
		}
		else if (verticalInput > 0 && !inFlight)
		{
			isLookingUp = true;
		}
		else if (verticalInput < 0 && isGrounded)
		{
			crouchDisableCollider.enabled = false;
			isCrouch = true;
		}
		else
		{
			rb.gravityScale = gravityScale;
			
			rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
			isLookingUp = false;
			isCrouch = false;
		}
		animator.SetBool("isLookingUp", isLookingUp);
		animator.SetBool("Crouch", isCrouch);
		animator.SetBool("isOnLadder", beginsToRise);
		animator.SetFloat("ClimpUp", Mathf.Abs(verticalInput));
	}
	
	void Flip()
	{
		isFacingRight = !isFacingRight;
		transform.Rotate(0, 180,0);
	}

	
}









