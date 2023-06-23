
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    private Animator animator;
	[SerializeField] private UnityEvent pressButton;
	[SerializeField] private UnityEvent releaseButton;
	private bool isTriggered = false;
	private void Start()
	{
		animator = GetComponent<Animator>();	
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!isTriggered) 
		{
			isTriggered = true;
			pressButton.Invoke();
			animator.SetTrigger("EnterButton");
		}
		
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if(isTriggered)
		{
			isTriggered = false;
			releaseButton.Invoke();
			animator.SetTrigger("ReleaseButton");
		}
		
	}
	
}
