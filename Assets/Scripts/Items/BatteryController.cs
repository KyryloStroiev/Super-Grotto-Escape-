
using UnityEngine;
using Zenject;

public class BatteryController : MonoBehaviour
{
    [SerializeField] private int batteryId;

	private const string BatteryCollected = "BatteryCollected";

	private bool collected;
	private GameManager gameManager;
	private PlayerMovement player;

	[Inject]
	public void Contract(GameManager gameManager, PlayerMovement player)
	{
		this.gameManager = gameManager;
		this.player = player;
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
		
		if (player.gameObject !=null && !collected)
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
