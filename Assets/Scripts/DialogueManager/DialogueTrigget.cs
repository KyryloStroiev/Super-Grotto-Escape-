
using UnityEngine;

public class DialogueTrigget : MonoBehaviour
{
	public Dialogue dialogue;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			TriggerDialogue();
			gameObject.SetActive(false);
		}
	}


	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
}
