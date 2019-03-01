using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static bool GameIsOver;
	public static string Territory;
	public GameObject gameOverUI;
	public GameObject completeLevelUI;

	public AudioSource audio;

	private void Start()
	{
		GameIsOver = false;

		audio = gameObject.AddComponent<AudioSource>();
		//https://downloads.khinsider.com/game-soundtracks/album/warcraft-2-tides-of-darkness-cda
		AudioClip clip = (AudioClip)Resources.Load("Music/02 - human battle 1");
		audio.PlayOneShot(clip);
	}

	void Update()
	{
		if (GameIsOver) return;
		if (PlayerStats.Lives <= 0)
		{
			EndGame();
		}
	}

	void EndGame()
	{
		GameIsOver = true;
		gameOverUI.SetActive(true);
		audio.Stop();
		AudioClip clip = (AudioClip)Resources.Load("Music/09 - human defeat");
		audio.PlayOneShot(clip);
	}

	public void WinLevel()
	{
		GameIsOver = true;
		completeLevelUI.SetActive(true);
		audio.Stop();
		AudioClip clip = (AudioClip)Resources.Load("Music/08 - human victory");
		audio.PlayOneShot(clip);
	}
}
