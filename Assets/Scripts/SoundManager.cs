using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public enum Music
	{
		Default,
		Victory,
		Defeat,
	};

	public enum Sounds
	{
	};
	AudioSource m_MusicSource;
	AudioSource m_SoundSource;

	private void Awake()
	{
		m_MusicSource = gameObject.AddComponent<AudioSource>();
		m_SoundSource = gameObject.AddComponent<AudioSource>();
	}

	private void Update()
	{
		if (m_MusicSource)
		{
			if (m_MusicSource.isPlaying)
			{
				if (!GameManager.instance.Settings.PlayMusic) m_MusicSource.Stop();
			}
			else
			{
				if (GameManager.instance.Settings.PlayMusic) PlayMusic();
			}
		}
	}

	public void PlayMusic()
	{
		PlayMusic(Music.Default);

	}

	public void PlayMusic(Music music)
	{
		//https://downloads.khinsider.com/game-soundtracks/album/warcraft-2-tides-of-darkness-cda
		if (!GameManager.instance.Settings.PlayMusic) return;
		string currentscene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		string songname = "";
		switch (GameManager.instance.sceneController.CurrentScene)
		{
			case "GameOver": songname = ""; break;
			case "Persistent":
			case "MainMenu":
			case "RiskMap":
				songname += "07 - human briefing"; break;
			default:
				switch (music)
				{
					case Music.Victory:
						songname += "08 - human victory"; break;
					case Music.Defeat:
						songname += "09 - human defeat"; break;
					default:
						songname += "02 - human battle 1"; break;
				}
				break;
		}
		if (songname == "")
		{
			m_MusicSource.Stop();
			return;
		}
		if (m_MusicSource.isPlaying && m_MusicSource.clip && m_MusicSource.clip.name == songname)
		{
			//Debug.Log("song already playing - bail");
			return;
		}
		m_MusicSource.clip = (AudioClip)Resources.Load("Music/" + songname);
		if (m_MusicSource.clip == null) Debug.Log("couldn't find music " + songname);
		m_MusicSource.loop = true;
		m_MusicSource.Play();
	}

	public void PlaySound(string soundname)
	{
		AudioClip clip = (AudioClip)Resources.Load("Sounds/" + soundname);
		if (clip != null) m_SoundSource.PlayOneShot(clip);
		else Debug.Log("missing sound effect");
	}

	public static SoundManager GetCurrent()
	{
		return GameManager.instance.soundManager;
	}
}
