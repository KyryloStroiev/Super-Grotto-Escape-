
using UnityEngine;
using Zenject;

public class BatteryController : MonoBehaviour
{
    [SerializeField] private int batteryId;

	private const string BatteryCollected = "BatteryCollected";

	private bool collected;
	private GameManager gameManager;
	private PlayerHealth playerHealth;

	[Inject]
	public void Contract(GameManager gameManager, PlayerHealth playerHealth)
	{
		this.gameManager = gameManager;
		this.playerHealth = playerHealth;
	}
	private void Start()
	{
		if (PlayerPrefs.HasKey(BatteryCollected + batteryId))
		{
			collected = true;
			gameObject.SetActive(false);
		}
		else
		{
			collected = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
		if (player !=null && !collected)
		{
			CollectItem();
		}
	}

	private void CollectItem()
	{
		
		PlayerPrefs.SetInt(BatteryCollected + batteryId, 1);
		collected = true;
		gameObject.SetActive(false);
		gameManager.CollectedBattery();
	}
}
