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
			//return;
		}
	}

	public static void Start()
	{
		// doesn't really need to do anything yet except make sure the class is triggered
	}
}
