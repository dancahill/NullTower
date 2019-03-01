using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public GameObject m_EnemyBasicPrefab;
	public GameObject m_EnemyToughPrefab;
	public GameObject m_EnemyFastPrefab;
	public GameObject m_EnemyTankPrefab;

	public static int EnemiesAlive = 0;
	public static int waveNumber = 0;
	public static int totalWaves = 0;

	public static Transform spawnPoint;

	public float m_TimeBetweenWaves = 10f;
	private float countdown = 5f;
	public Text waveCountdownText;
	public GameManager gameManager;

	Territory territory;

	private void Start()
	{
		territory = Territories.Get(GameManager.Territory);
		spawnPoint = null;
		totalWaves = territory.waves.Length;
		waveNumber = 0;
		EnemiesAlive = 0;
	}

	private void Update()
	{
		if (EnemiesAlive > 0)
		{
			return;
		}
		if (GameManager.GameIsOver) return;
		if (waveNumber == territory.waves.Length && PlayerStats.Lives > 0)
		{
			gameManager.WinLevel();
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
		waveCountdownText.text = string.Format("{0:0.0}", countdown);
	}
	IEnumerator SpawnWave()
	{
		waveNumber++;

		EnemyWave[] wave = territory.waves[waveNumber - 1];

		EnemiesAlive = 0;

		for (int i = 0; i < wave.Length; i++)
		{
			if (wave[i].type != Enemy.Type.None) EnemiesAlive += wave[i].count;
		}
		string s = string.Format("Starting wave {0} ({1} enemies)", waveNumber, EnemiesAlive);
		Debug.Log(s);
		GameToast.Add(s);
		for (int i = 0; i < wave.Length; i++)
		{
			for (int j = 0; j < wave[i].count; j++)
			{
				switch (wave[i].type)
				{
					case Enemy.Type.Basic:
						SpawnEnemy(m_EnemyBasicPrefab);
						break;
					case Enemy.Type.Tank:
						SpawnEnemy(m_EnemyTankPrefab);
						break;
					case Enemy.Type.Tough:
						SpawnEnemy(m_EnemyToughPrefab);
						break;
					case Enemy.Type.Fast:
						SpawnEnemy(m_EnemyFastPrefab);
						break;
					default:
						break;
				}
				float delay = wave[i].delay > 0 ? wave[i].delay : 1f / wave[i].rate;
				yield return new WaitForSeconds(delay);
			}
		}
		//waveIndex++;
	}
	private void SpawnEnemy(GameObject enemy)
	{
		if (spawnPoint == null)
		{
			spawnPoint = GameObject.Find("Start").transform;
		}
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}
}
