using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
	public Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}
	public void GateOpen()
	{
		
		animator.SetBool("isOpen", true);
	}
}
