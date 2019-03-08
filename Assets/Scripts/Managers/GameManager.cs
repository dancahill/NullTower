using UnityEngine;

//original class

public partial class GameManager : MonoBehaviour
{
	public static bool GameIsOver;
	public static string Territory;
	public GameObject gameOverUI;
	public GameObject completeLevelUI;
	public AudioSource m_Audio;

	private void Start()
	{
		GameIsOver = false;
		if (Manager.manager.playMusic)
		{
			// removed audio for now, messes up with my background music.  uncomment later.
			// I LIKE MUSIC
			m_Audio = gameObject.AddComponent<AudioSource>();
			//https://downloads.khinsider.com/game-soundtracks/album/warcraft-2-tides-of-darkness-cda
			AudioClip clip = (AudioClip)Resources.Load("Music/02 - human battle 1");
			m_Audio.PlayOneShot(clip);
		}
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
		if (Manager.manager.playMusic)
		{
			m_Audio.Stop();
			AudioClip clip = (AudioClip)Resources.Load("Music/09 - human defeat");
			m_Audio.PlayOneShot(clip);
		}
	}

	public void WinLevel()
	{
		GameIsOver = true;
		completeLevelUI.SetActive(true);
		if (Manager.manager.playMusic)
		{
			m_Audio.Stop();
			AudioClip clip = (AudioClip)Resources.Load("Music/08 - human victory");
			m_Audio.PlayOneShot(clip);
		}
	}
}
