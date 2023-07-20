using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private float slideSpeed = 25f;
	private float slideTime = 0.2f;
	private const string SlidingSound = "SlidingPlayer";
	[HideInInspector] public bool isSliding = false;

    private Rigidbody2D rb;
	private PlayerMovement playerMovement;
	private CheckAndFlipDirection checkAndFlip;
	private AudioManager audioManager;
	private PlayerInput input;

	[Inject]
	private void Construct(AudioManager audioManager)
	{
		this.audioManager = audioManager;
	}
	private void Awake()
	{
		input = new PlayerInput();
		playerMovement = GetComponent<PlayerMovement>();
		checkAndFlip = GetComponent<CheckAndFlipDirection>();
        rb = GetComponent<Rigidbody2D>();
		input.Player.Slide.performed += _ => Slide();
	}

	private void Slide()
	{
		if(playerMovement.isGrounded)
		{
			audioManager.Play(SlidingSound);
			StartCoroutine(PrefromSlide());
		}
	}

	IEnumerator PrefromSlide()
	{
		
		isSliding = true;
		Vector2 slideDirection = checkAndFlip.isFacingRight ? Vector2.right : Vector2.left;
		float t = 0f;
		float initialSpeed = playerMovement.moveSpeed;
		playerMovement.moveSpeed = slideSpeed;

		while (t < slideTime)
		{
			t += Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(slideTime);

		playerMovement	.moveSpeed = initialSpeed;
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
