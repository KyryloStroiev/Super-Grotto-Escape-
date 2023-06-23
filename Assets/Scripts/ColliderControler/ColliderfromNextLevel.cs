
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColliderfromNextLevel : MonoBehaviour
{
	public string levelScene;
	[SerializeField] private Image background;
	private bool isNextLevel=false;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
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
