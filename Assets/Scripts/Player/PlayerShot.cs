
using UnityEngine;
using Zenject;

public class PlayerShot : MonoBehaviour
{
	private const float bulletOffset = 0.3f;
	private const string ShotSound = "ShotSound";
	private const string Bullet = "Bullet";

	[SerializeField] private Transform shotPoints;
	private AudioManager audioManager;
	private PlayerAnimator playerAnimator;
	private PlayerMovement playerMovement;
	private PlayerInput input;
	
	

	[Inject]
	private void Construct(AudioManager audioManager)
	{
		this.audioManager = audioManager;
	}
	private void Awake()
	{
		input = new PlayerInput();
		playerAnimator = GetComponent<PlayerAnimator>();
		playerMovement = GetComponent<PlayerMovement>();
		input.Player.Shoot.performed += _ => Shoot();
	}

	void Shoot()	
	{
		
		if (playerMovement.isCrouch) 
		{
			playerAnimator.ShootInCrouch();
		}
		else
		{
			playerAnimator.Shoot();
		}
		audioManager.Play(ShotSound);
	}

	void ShootBull()
	{
		Vector3 bulletPosition = shotPoints.position;
		if(playerMovement.isCrouch)
		{
			bulletPosition -= shotPoints.up * bulletOffset;
		}
		ObjectPooler.Instance.SpawnFromPool(Bullet, bulletPosition, Quaternion.identity);

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
