using UnityEngine;

public static class AppGlobals
{
	public static Manager manager = Manager.manager;

	private static string m_InitialScene;

	public static string InitialScene
	{
		get { return m_InitialScene; }
	}

	static AppGlobals()
	{
		m_InitialScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		if (m_InitialScene != "Main")
		{
			Debug.Log("started from scene " + m_InitialScene + " - redirecting to Main");
			m_InitialScene = "Main";
			UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
		}
		if (manager == null)
		{
			if (Manager.manager == null)
			{
				Debug.Log("manager == null - trying to make one");
				GameObject GO = new GameObject("AppGlobals");
				Manager.manager = GO.AddComponent<Manager>();
			}
			manager = Manager.manager;
			Debug.Log("did it work?");
		}
	}

	public static void Start()
	{
		// doesn't really need to do anything yet except make sure the class is triggered
	}
}
