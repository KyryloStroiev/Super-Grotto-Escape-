
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
		if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Fireball"))
		{
			spriteChange.Change();
			
		}
	}
}
