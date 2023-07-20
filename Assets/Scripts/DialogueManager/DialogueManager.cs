using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;

	private Queue<string> sentences;
	[SerializeField] private Animator animator;
	private PlayerMovement playerMovement;
	private bool isDialogueActive;

	private void Start()
	{
		sentences = new Queue<string>();
		playerMovement = FindObjectOfType<PlayerMovement>();
	}

	private void Update()
	{
		
			if (isDialogueActive && Input.anyKeyDown)
			{
				DisplayNextSentence();
			}
		
	}

	public void StartDialogue(Dialogue dialogue)
	{
		isDialogueActive = true;
		animator.SetBool("isOpen", true);
		nameText.text = dialogue.nameNpc;
		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();

		playerMovement.enabled = false;
		Animator playerAnimator = playerMovement.GetComponent<Animator>();
		playerAnimator.speed = 0f;
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	private IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";

		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.04f);
		}
	}

	private void EndDialogue()
	{
		animator.SetBool("isOpen", false);
		isDialogueActive = false;
		playerMovement.enabled = true;
		Animator playerAnimator = playerMovement.GetComponent<Animator>();
		playerAnimator.speed = 1f;
	}
}

