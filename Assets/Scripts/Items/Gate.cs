
using UnityEngine;

public class Gate : MonoBehaviour
{
	private Animator animator;
	private int pressedButton = 0;
	[SerializeField] private int pressedButtonToOpen;
	[SerializeField] private GameObject indicator;
	[SerializeField] private  Sprite IndicatorOpen;
	private SpriteRenderer indicatorSpriteRenderer;
	private void Start()
	{
		indicatorSpriteRenderer = indicator.GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

	public void PressedButton()
	{
		pressedButton++;
		
	}
	public void ReleaseButton()
	{
		pressedButton--;
	}
	private void Update()
	{
		if (pressedButton >= pressedButtonToOpen)
		{
			GateOpen();
		
		}
	}
	public void GateOpen()
	{
		animator.SetBool("GateOpen", true);
		indicatorSpriteRenderer.sprite = IndicatorOpen;
	}
}
