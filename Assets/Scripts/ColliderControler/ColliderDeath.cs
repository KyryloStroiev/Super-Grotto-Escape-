
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ColliderDeath : MonoBehaviour
{
	[SerializeField] private PlayerMovement playerMovement;

	[Inject]
	private void Construct(PlayerMovement playerMovement)
	{
		this.playerMovement = playerMovement;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(playerMovement.gameObject == collision.gameObject)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else
		{
			collision.gameObject.SetActive(false);
		}

	}
}
