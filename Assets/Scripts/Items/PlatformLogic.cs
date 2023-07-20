
using UnityEngine;
using Zenject;

public class PlatformLogic : MonoBehaviour
{
    private Animator animator;
	private PlayerHealth playerHealth;

	[Inject]
	private void Construct(PlayerHealth playerHealth)
	{
		this.playerHealth = playerHealth;
	}
	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		
		if (playerHealth.gameObject == collision.gameObject)
		{
			animator.SetTrigger("Destroy");
		}
	}
}
