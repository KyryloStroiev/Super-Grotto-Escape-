using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
   public Animator animator;
	public GameObject[] gate;

	private void Start()
	{
		animator = GetComponent<Animator>();
		
	}

	private void Update()
	{
	
	}

	void PressButton()
	{
		animator.SetBool("isOpen", true);

		
		foreach (GameObject gateObject in gate)
		{
			GateController gateController = gateObject.GetComponent<GateController>();
			if (gateController != null)
			{
				gateController.GateOpen();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			PressButton();
		}
	}
}
