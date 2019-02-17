using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public static int EnemiesAlive = 0;
	public Wave[] waves;

	public Transform enemyPrefab;
	public Transform spawnPoint;

	public float timeBetweenWaves = 20f;
	private float countdown = 5f;
	public Text waveCountdownText;
	public GameManager gameManager;
	private int waveIndex = 0;

	private void Update()
	{
		if (EnemiesAlive > 0)
		{
			return;
		}
		if (waveIndex == waves.Length)
		{
			gameManager.WinLevel();
			this.enabled = false;
		}
		if (countdown <= 0)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
			return;
		}
		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
		waveCountdownText.text = string.Format("{0:00.0}", countdown);
	}
	IEnumerator SpawnWave()
	{
		PlayerStats.Rounds++;
		Wave wave = waves[waveIndex];

		EnemiesAlive = wave.count;
		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}
		waveIndex++;
	}
	private void SpawnEnemy(GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}
}
