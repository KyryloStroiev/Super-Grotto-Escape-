using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
	public  float moveSpeed = 5f;
	[SerializeField] private float jumpForce = 17f;
	[SerializeField] private float climpSpeed = 3f;
	private float gravityScale = 3f;
	internal Vector2 moveDirection;
	private const float groundedRadius = .2f; 

	internal bool isCrouch = false;
	internal bool isGrounded;
	internal bool inFlight = false; 
	internal bool isOnLadder; 
	internal bool isLookingUp = false;
	internal bool isClimbing = false;

	[SerializeField] private Collider2D crouchDisableCollider;
	[SerializeField] private LayerMask whatIsGround; 
	[SerializeField] private Transform pointGroundCheck; 

	private Rigidbody2D rb;

	private PlayerInput input;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		
		input = new PlayerInput();
		input.Player.Jump.performed += _ => Jump();
	}

	private void Update()
	{
		CheckGround();
		CheckFlight();
	}

	private void FixedUpdate()
	{	
		Move();
		Crounch();
		LookUp();
	}

	 public  void Move()
	 {
		moveDirection = input.Player.Move.ReadValue<Vector2>();
		if(isCrouch || isLookingUp)
		{
			return;
		}
		else if (isOnLadder)
		{
			MoveOnLadder();
		}
		else
		{
			MoveHorizontal();	
		}
	 }
	private void Jump()
	{
		if (isGrounded)
		{
			rb.velocity = new Vector2(0, jumpForce);
		}
	}

	private void Crounch()
	{
		if(moveDirection.y < 0 && !inFlight && !isOnLadder)
		{
			crouchDisableCollider.enabled = false;
			isCrouch = true;
		}
		else
		{
			crouchDisableCollider.enabled = true;
			isCrouch = false;
		}
	}

	private void MoveOnLadder()
	{
		rb.gravityScale = 0;

		rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * climpSpeed);
		isLookingUp = false;

		if (!isGrounded)
		{
			isClimbing = true;
		}
	}

	private void  MoveHorizontal()
	{
		rb.gravityScale = gravityScale;

		rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
		isLookingUp = false;
		isCrouch = false;
	}

	private void LookUp()
	{
		if (moveDirection.y > 0 && !inFlight && !isOnLadder)
		{
			isLookingUp = true;
		}
		else { isLookingUp = false; }
	}

	private void CheckGround()
	{
		isGrounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(pointGroundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
			}
		}
	}
	private void CheckFlight()
	{
		inFlight = !isGrounded && !isOnLadder;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		SetLadderState(other, true);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Ladder"))
		{
			SetLadderState(other, false);
			isClimbing = false;
		}
	}

	private void SetLadderState(Collider2D other, bool isOnLadder)
	{
		if (other.gameObject.CompareTag("Ladder"))
		{
			this.isOnLadder = isOnLadder;
		}
	}

	private void OnEnable()
	{
		input.Enable();
	}
	private void OnDisable()
	{
		input?.Disable();
	}

}









