
using TMPro;
using UnityEngine;
using Zenject;

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
    private EnemyLogic enemyLogic;
    private PlayerHealth playerHealth;

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        this.playerHealth = playerHealth;
    }
    void Start()
    {
		LoadGameProgress();
        playerHealth.OnHealthChange += (health) =>
        healthPlayer.text = health + "/100";
    }

    void Update()
    {
       ElapsedTime();
	}

    public void CollectedBattery()
    {
        batteryCollect++;
		battery.text = "X " + batteryCollect + "/4";
		SaveGameProgress();
    }

    public void Scoring(int points)
    {
		
		scoring += points;
		score.text = "Score: " + scoring;
		SaveGameProgress();
    }

	public void SaveGameProgress()
    {
        PlayerPrefs.SetInt(batteryCollectKey, batteryCollect);
        PlayerPrefs.SetInt(healthKey, (int)playerHealth.Health);
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

        if (PlayerPrefs.HasKey(healthKey))
            playerHealth.Health = PlayerPrefs.GetInt(healthKey);

        if (PlayerPrefs.HasKey(timeKey))
            timeGame = PlayerPrefs.GetString(timeKey);

        if (PlayerPrefs.HasKey(scoreKey))
            scoring = PlayerPrefs.GetInt(scoreKey);
	}
    

    private void ElapsedTime()
    {
		float elepsedTime = Time.time - startTime;
		timeGame = FormatTime(elepsedTime);
		gameTime.text = timeGame;
	}
	private string FormatTime(float time)
    {
        int hours = (int)time / 3600;
        int minutes = ((int)time % 3600)/60;
        int seconds =  ((int) time % 3600) % 60;

        string formattedTime = string.Format("{00:00}:{01:00}:{02:00}", hours, minutes, seconds);
        return formattedTime;
    }

	//private void OnDisable()
	//{
	//	playerHealth.OnHealthChange -= (health) =>
	//   healthPlayer.text = health + "/100";
	//}
}
