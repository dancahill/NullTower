using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
	public Image img;
	public AnimationCurve curve;

	void Start()
	{
		StartCoroutine(FadeIn());
	}

	public void FadeTo(string scene)
	{
		Time.timeScale = 1;
		StartCoroutine(FadeOut(scene));
	}

	public void FadeTo(string scene, string territory)
	{
		GameManager.Territory = territory;
		Time.timeScale = 1;
		StartCoroutine(FadeOut(scene));
	}

	IEnumerator FadeIn()
	{
		float t = 1f;

		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0, 0, 0, a);
			yield return 0;
		}
	}

	IEnumerator FadeOut(string scene)
	{
		float t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0, 0, 0, a);
			yield return 0;
		}
		Time.timeScale = 1;
		SceneManager.LoadScene(scene);
	}
}
