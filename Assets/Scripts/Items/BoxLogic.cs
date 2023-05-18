using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{
	private SpriteChange spriteChange;
	

	private void Start()
	{
		spriteChange = GetComponent<SpriteChange>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Bullet"))
		{
			spriteChange.Change();
			
		}
	}
}