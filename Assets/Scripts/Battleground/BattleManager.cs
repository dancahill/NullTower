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

public partial class BattleManager : MonoBehaviour
{
	#region Singleton
	public static BattleManager instance;
	#endregion

	public static bool GameIsOver;
	public GameObject gameOverUI;
	public GameObject completeLevelUI;
	public PlayerStats stats;

	private void Awake()
	{
		instance = this;
		stats = new PlayerStats();
		//AppGlobals.Start();
	}

	private void Start()
	{
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
		GameIsOver = true;
		gameOverUI.SetActive(true);
		GameManager.instance.soundManager.PlayMusic(SoundManager.Music.Defeat);
	}

	public void WinLevel()
	{
		GameIsOver = true;
		completeLevelUI.SetActive(true);
		GameManager.instance.soundManager.PlayMusic(SoundManager.Music.Victory);
	}

	public void ReturnToMap()
	{
		GameManager.instance.sceneController.FadeAndLoadScene("RiskMap");
	}
}
