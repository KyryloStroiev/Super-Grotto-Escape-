using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
	private int kill = 0;
	public TextMeshProUGUI killed;

	private void Start()
	{
		GlobalEventManager.OnEnemyKilled.AddListener(EnemyKilled);
		

	}
	

	void EnemyKilled()
	{
		kill++;
		killed.text = "Killed: " + kill;
	}
}

