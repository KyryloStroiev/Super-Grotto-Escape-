using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public  CharacterController2D controller;
    private Rigidbody2D rb;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool isLookingUp = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!jump)
        {
			animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
		}
	    
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(Input.GetKeyDown(KeyCode.Space))
        {

            animator.SetBool("isJumping", true);
            jump = true;

        }
        animator.SetBool("isLookingUp", isLookingUp);
        if (Input.GetKey(KeyCode.W))
        {
            if (!isLookingUp)
            {
                isLookingUp = true;
            }
        }
        else
        {
            if (isLookingUp)
            {
                isLookingUp = false;
            }
        }
        
    }

    public void OnLanding()
    {
       animator.SetBool("isJumping", false);
    }
   

    private void FixedUpdate()
	{
		
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
       

	}
   
}
