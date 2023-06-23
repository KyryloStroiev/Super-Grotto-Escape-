using System.Collections;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private float slideSpeed = 25f;
	private float slideTime = 0.2f;
	[HideInInspector] public bool isSliding = false;
    private Rigidbody2D rb;
	private CharacterController2D characterController;
	private CheckAndFlipDirection checkAndFlip;
	private PlayerInput input;
	private void Awake()
	{
		input = new PlayerInput();
		characterController = GetComponent<CharacterController2D>();
		checkAndFlip = GetComponent<CheckAndFlipDirection>();
        rb = GetComponent<Rigidbody2D>();
		input.Player.Slide.performed += _ => Slide();
	}

	private void Slide()
	{
		StartCoroutine(PrefromSlide());
	}

	IEnumerator PrefromSlide()
	{
		isSliding = true;
		Vector2 slideDirection = checkAndFlip.isFacingRight ? Vector2.right : Vector2.left;
		float t = 0f;
		float initialSpeed = characterController.moveSpeed;
		characterController.moveSpeed = slideSpeed;

		while (t < slideTime)
		{
			t += Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(slideTime);

		characterController.moveSpeed = initialSpeed;
		isSliding = false;
	}

	private void OnEnable()
	{
		input.Enable();
	}
	private void OnDisable()
	{
		input.Disable();
	}

}
