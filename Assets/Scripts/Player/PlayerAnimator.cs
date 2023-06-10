using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private CharacterController2D controller;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
		animator.SetBool("isLookingUp", controller.isLookingUp);
		animator.SetBool("Crouch", controller.isCrouch);
		animator.SetBool("isOnLadder", controller.isClimbing);
		animator.SetFloat("ClimpUp", Mathf.Abs(controller.moveDirection.y));
		animator.SetFloat("Speed", Mathf.Abs(controller.moveDirection.x));
		animator.SetBool("Jump", controller.inFlight);
	}
}
