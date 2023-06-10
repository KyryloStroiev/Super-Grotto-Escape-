using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class BatteryController : MonoBehaviour
{
    [SerializeField] private int batteryId;
    private bool collected;
	private GameManager gameManager;

	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		if (PlayerPrefs.HasKey("BatteryCollected" + batteryId))
		{
			collected = true;
			Destroy(gameObject);
		}
		else
		{
			collected = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && !collected)
		{
			CollectItem();
		}
	}

	private void CollectItem()
	{
		Debug.Log("ASDAS");
		gameManager.CollectedBattery();
		PlayerPrefs.SetInt("BatteryCollected" + batteryId, 1);
		collected = true;
		gameObject.SetActive(false);
	}
}
