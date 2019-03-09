using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FaderTest : MonoBehaviour
{
	float alpha;
	float time = 2;

	void Start()
	{
		StartCoroutine(FadeIn());
	}

	public void FadeTo(string scene)
	{
		Time.timeScale = 1;
		alpha = 1;
		StartCoroutine(FadeOut(scene));
	}

	public void FadeTo(string scene, string territory)
	{
		GameManager.Territory = territory;
		Time.timeScale = 1;
		alpha = 1;
		StartCoroutine(FadeOut(scene));
	}

	IEnumerator FadeIn()
	{
		float t = time;

		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = alpha - Time.deltaTime;//curve.Evaluate(t);
							 //img.color = new Color(0, 0, 0, a);
			yield return 0;
		}
	}

	IEnumerator FadeOut(string scene)
	{
		float t = 0f;

		SpriteRenderer sr;
		GameObject obj = new GameObject("FaderThing");
		obj.transform.localScale = new Vector3(60000, 60000);
		obj.transform.position += new Vector3(0, 0, -100);
		sr = obj.AddComponent<SpriteRenderer>();
		Sprite square = Resources.Load<Sprite>("Images/WhiteSquare");
		//Debug.Log(square ? "square is not null" : "square is null");
		sr.sprite = square;
		sr.sortingOrder = 666;
		sr.color = Color.black;
		while (t < time + 1)
		{
			t += Time.deltaTime;
			float a = t / time; //curve.Evaluate(t);
			Debug.Log(string.Format("a={0}", a));
			sr.color = new Color(0, 0, 0, a);
			//sr.color = new Color(0, 0, 0, 1);
			yield return 0;
		}
		Time.timeScale = 1;
		SceneManager.LoadScene(scene);
	}
}
