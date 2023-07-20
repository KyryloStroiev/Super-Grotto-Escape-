
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class ColliderfromNextLevel : MonoBehaviour
{
	public string levelScene;
	[SerializeField] private Image background;
	private bool isNextLevel=false;

	private PlayerMovement playerMovement;

	[Inject]
	private void Construct(PlayerMovement playerMovement)
	{
		this.playerMovement = playerMovement;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerMovement.gameObject == collision.gameObject)
		{
			isNextLevel = true;
		}
	}

	private void Update()
	{
		if(isNextLevel)
		{
			LoadNextLevel();
		}
	}

	private void LoadNextLevel()
	{
		var color = background.color;
		color.a += 0.01f;
		background.color = color;
		if (background.color.a >= 1)
		{ SceneManager.LoadScene(levelScene); }
	}
}
