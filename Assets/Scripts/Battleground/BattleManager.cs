using System;
using UnityEngine;

[Serializable]
public class PlayerStats
{
	public int startMoney = 400;
	public int startLives = 20;
	public int Money;
	public int Lives;
	public int Rounds;

	public PlayerStats()
	{
		Money = startMoney;
		Lives = startLives;
		Rounds = 0;
	}
}

public class BattleManager : MonoBehaviour
{
	#region Singleton
	public static BattleManager instance;
	#endregion

	public static bool GameIsOver;
	public GameObject gameOverUI;
	public GameObject completeLevelUI;
	public PlayerStats stats;
	public bool attackMode;
	public Territory territory;

	private void Awake()
	{
		instance = this;
		stats = new PlayerStats();
	}

	private void Start()
	{
		territory = GameManager.instance.Game.territories.Find(f => f.name == GameManager.instance.TerritoryName);
		//attackMode = GameManager.instance.attackMode;
		attackMode = (territory.ownership != Territory.Ownership.Player);
		if (attackMode) GetComponent<BGLevelBuilder>().AddAIDefenders();
		GameIsOver = false;
	}

	void Update()
	{
		if (GameIsOver) return;
		if (stats.Lives <= 0)
		{
			EndGame();
		}
	}

	void EndGame()
	{
		Territory t = GameManager.instance.Game.territories.Find(f => f.name == GameManager.instance.TerritoryName);
		GameIsOver = true;
		// attacker won
		if (attackMode)
		{
			completeLevelUI.SetActive(true);
			GameManager.instance.soundManager.PlayMusic(SoundManager.Music.Victory);
			if (t != null)
			{
				t.ownership = Territory.Ownership.Player;
				GameSave.SaveGame();
			}
		}
		else
		{
			gameOverUI.SetActive(true);
			GameManager.instance.soundManager.PlayMusic(SoundManager.Music.Defeat);
		}
	}

	public void WinLevel()
	{
		Territory t = GameManager.instance.Game.territories.Find(f => f.name == GameManager.instance.TerritoryName);
		GameIsOver = true;
		// defender won
		if (!attackMode)
		{
			completeLevelUI.SetActive(true);
			GameManager.instance.soundManager.PlayMusic(SoundManager.Music.Victory);
			if (t != null)
			{
				int score = 0;
				if (stats.Lives == t.startLives) score = 3;
				else if (stats.Lives > t.startLives / 2) score = 2;
				else if (stats.Lives > 0) score = 1;
				if (t.highScore < score) t.highScore = score;
				t.ownership = Territory.Ownership.Player;
				GameSave.SaveGame();
			}
		}
		else
		{
			gameOverUI.SetActive(true);
			GameManager.instance.soundManager.PlayMusic(SoundManager.Music.Defeat);
		}
	}

	public void ReturnToMap()
	{
		GameManager.instance.sceneController.FadeAndLoadScene("RiskMap");
	}
}
