
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private CharacterController2D controller;
    private PlayerSlide playerSlide;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
        playerSlide = GetComponent<PlayerSlide>();

    }

    void Update()
    {
		animator.SetBool("isLookingUp", controller.isLookingUp);
		animator.SetBool("Crouch", controller.isCrouch);
		animator.SetBool("isOnLadder", controller.isClimbing);
		animator.SetFloat("ClimpUp", Mathf.Abs(controller.moveDirection.y));
		animator.SetFloat("Speed", Mathf.Abs(controller.moveDirection.x));
		animator.SetBool("Jump", controller.inFlight);
		animator.SetBool("isSlide", playerSlide.isSliding);
	}

    public void ShootInCrouch()
    {
		animator.SetTrigger("ShootInCrouch");
	}
	public void Shoot()
	{
		animator.SetTrigger("Shoot");
	}

	public void TakeDamage()
	{
		animator.SetTrigger("Damage");
	}
	
}
