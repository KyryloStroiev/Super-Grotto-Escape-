using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI battery;
    [SerializeField] private TextMeshProUGUI gameTime;
    [SerializeField] private TextMeshProUGUI healthPlayer;
    [SerializeField] private TextMeshProUGUI score;
    private int batteryCollect;
    private int scoring;
    private float startTime;
    private string timeGame;

	private const string batteryCollectKey = "BatteryCollect";
    private const string healthKey = "HealthKey";
    private const string scoreKey = "ScoreKey";
    private const string timeKey = "TimeKey";
	private PlayerHealth playerHealth;
	void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        //startTime = Time.time;
        LoadGameProgress();
    }

    void Update()
    {
        float elepsedTime = Time.time - startTime;
        timeGame = FormatTime(elepsedTime);
        battery.text = "X " + batteryCollect + "/4";
        gameTime.text = timeGame;
        healthPlayer.text = playerHealth.health + "/100";
        score.text = "Score: " + scoring;
    }

    public void CollectedBattery()
    {
        batteryCollect++;
        SaveGameProgress();
    }

    public void Scoring(int points)
    {
        scoring += points;
        SaveGameProgress();
    }


	public void SaveGameProgress()
    {
        PlayerPrefs.SetInt(batteryCollectKey, batteryCollect);
        PlayerPrefs.SetInt(healthKey, (int)playerHealth.health);
        PlayerPrefs.SetString(timeKey, timeGame);
        PlayerPrefs.SetInt(scoreKey, scoring);
        PlayerPrefs.Save();
    }

    private void LoadGameProgress()
    {
        if(PlayerPrefs.HasKey(batteryCollectKey))
        {
			batteryCollect = PlayerPrefs.GetInt(batteryCollectKey);
		}
        else
        {
            batteryCollect = 0;
        }

        if(PlayerPrefs.HasKey(healthKey))
			playerHealth.health = PlayerPrefs.GetInt(healthKey);

        if(PlayerPrefs.HasKey(timeKey))
            timeGame = PlayerPrefs.GetString(timeKey);

        if (PlayerPrefs.HasKey(scoreKey))
            scoring = PlayerPrefs.GetInt(scoreKey);
	}
    
    private string FormatTime(float time)
    {
        int hours = (int)time / 3600;
        int minutes = ((int)time % 3600)/60;
        int seconds =  ((int) time % 3600) % 60;

        string formattedTime = string.Format("{00:00}:{01:00}:{02:00}", hours, minutes, seconds);
        return formattedTime;
    }

   
}
