
using UnityEngine;
using UnityEngine.SceneManagement;
public class ColliderDeath : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		if(collision.gameObject.CompareTag("Enemy"))
		{
			collision.gameObject.SetActive(false);
		}

	}
}
