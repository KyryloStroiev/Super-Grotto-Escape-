using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    private Animator animator;
	public UnityEvent pressButton;
	public UnityEvent releaseButton;
	private bool isTriggered = false;
	private void Start()
	{
		animator = GetComponent<Animator>();
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!isTriggered) 
		{
			isTriggered = true;
			pressButton.Invoke();
			animator.SetTrigger("EnterButton");
		}
		
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if(isTriggered)
		{
			isTriggered = false;
			releaseButton.Invoke();
			animator.SetTrigger("ReleaseButton");
		}
		
	}
	
}
