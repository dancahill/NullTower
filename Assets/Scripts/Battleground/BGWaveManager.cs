using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BGWaveManager : MonoBehaviour
{
	public GameObject m_EnemyJeepPrefab;
	public GameObject m_EnemyTankPrefab;
	public GameObject m_EnemyHeavyTankPrefab;
	public GameObject m_EnemyBuggyPrefab;
	public GameObject m_EnemyGunshipPrefab;

	GameObject m_Attackers;

	public static int EnemiesAlive = 0;
	public static int waveNumber = 0;
	public static int totalWaves = 0;

	public static Transform spawnPoint;

	public float m_TimeBetweenWaves = 10f;
	private float countdown = 10f;
	public Text waveCountdownText;
	BattleManager battleManager;

	Territory territory;

	private void Start()
	{
		battleManager = GetComponent<BattleManager>();
		territory = Territories.Get(GameManager.instance.TerritoryName);
		m_Attackers = new GameObject("Attackers");
		spawnPoint = null;
		totalWaves = territory.waves.Length;
		waveNumber = 0;
		EnemiesAlive = 0;
		if (battleManager.attackMode)
		{
			countdown = 300;
		}
	}

	private void Update()
	{
		if (BattleManager.GameIsOver) return;
		if (battleManager.attackMode)
		{
			if (countdown <= 0)
			{
				//Debug.Log("time's up. you lose.");
				battleManager.WinLevel();
				return;
			}
			countdown -= Time.deltaTime;
			countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
			waveCountdownText.text = countdown > 0 ? string.Format("{0:0.0}", countdown) : "";
		}
		else
		{
			if (EnemiesAlive > 0)
			{
				return;
			}
			if (waveNumber == territory.waves.Length && BattleManager.instance.stats.Lives > 0)
			{
				battleManager.WinLevel();
				this.enabled = false;
			}
			if (countdown <= 0)
			{
				StartCoroutine(SpawnWave());
				countdown = m_TimeBetweenWaves;
				return;
			}
			countdown -= Time.deltaTime;
			countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
			waveCountdownText.text = countdown > 0 ? string.Format("{0:0.0}", countdown) : "";
		}
	}

	IEnumerator SpawnWave()
	{
		waveNumber++;

		EnemyWave[] wave = territory.waves[waveNumber - 1];

		EnemiesAlive = 0;

		for (int i = 0; i < wave.Length; i++)
		{
			if (wave[i].type != Attacker.Type.None) EnemiesAlive += wave[i].count;
		}
		string s = string.Format("Wave {0} ({1} enemies)", waveNumber, EnemiesAlive);
		//Debug.Log(s);
		GameToast.Add(s);
		for (int i = 0; i < wave.Length; i++)
		{
			for (int j = 0; j < wave[i].count; j++)
			{
				SpawnAttackerType(wave[i].type);
				float delay = wave[i].delay > 0 ? wave[i].delay : 1f / wave[i].rate;
				yield return new WaitForSeconds(delay);
			}
		}
		//waveIndex++;
	}

	public void SpawnAttackerType(string type)
	{
		Attacker.Type t = (Attacker.Type)Enum.Parse(typeof(Attacker.Type), type);
		int cost;
		switch (t)
		{
			case Attacker.Type.Jeep: cost = 50; break;
			case Attacker.Type.Tank: cost = 150; break;
			case Attacker.Type.HeavyTank: cost = 250; break;
			case Attacker.Type.Buggy: cost = 50; break;
			case Attacker.Type.Gunship: cost = 100; break;
			default: cost = 50; break;
		}
		if (BattleManager.instance.stats.Money >= cost)
		{
			BattleManager.instance.stats.Money -= cost;
			SpawnAttackerType(t);
		}
	}

	public void SpawnAttackerType(Attacker.Type type)
	{
		switch (type)
		{
			case Attacker.Type.Jeep:
				SpawnEnemy(m_EnemyJeepPrefab);
				break;
			case Attacker.Type.Tank:
				SpawnEnemy(m_EnemyTankPrefab);
				break;
			case Attacker.Type.HeavyTank:
				SpawnEnemy(m_EnemyHeavyTankPrefab);
				break;
			case Attacker.Type.Buggy:
				SpawnEnemy(m_EnemyBuggyPrefab);
				break;
			case Attacker.Type.Gunship:
				SpawnEnemy(m_EnemyGunshipPrefab);
				break;
			default:
				break;
		}
	}

	private void SpawnEnemy(GameObject enemy)
	{
		if (spawnPoint == null)
		{
			spawnPoint = GameObject.Find("Start").transform;
		}
		Vector3 sp = new Vector3(spawnPoint.transform.position.x, 0.5f, spawnPoint.transform.position.z);
		GameObject e = Instantiate(enemy, sp, spawnPoint.rotation, m_Attackers.transform);
		// bit of a cheat. will break with multiple spawn points
		if (Waypoints.points.Length > 2) e.transform.LookAt(Waypoints.points[1]);
	}
}
