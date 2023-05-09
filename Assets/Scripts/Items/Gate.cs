using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
	public Animator animator;
	private int pressedButton = 0;
	public int pressedButtonToOpen;

	private void Start()
	{
		
		animator = GetComponent<Animator>();
	}

	public void PressedButton()
	{
		pressedButton++;
		
	}
	public void ReleaseButton()
	{
		pressedButton--;
	}
	private void Update()
	{
		if (pressedButton >= pressedButtonToOpen)
		{
			GateOpen();
		}
	}
	public void GateOpen()
	{

		animator.SetBool("GateOpen", true);
	}
}
