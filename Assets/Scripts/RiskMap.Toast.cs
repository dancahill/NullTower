using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public partial class RiskMap : MonoBehaviour
{
	private IEnumerator coroutine;
	public Text m_ToastText;
	public static string Text = "";

	void Update()
	{
		if (Text != "")
		{
			coroutine = ShowText();
			StartCoroutine(coroutine);
		}
	}

	IEnumerator ShowText()
	{
		m_ToastText.text = Text;
		Text = "";
		yield return new WaitForSeconds(2f);
		m_ToastText.text = "";
		yield return 0;
	}

	public void Toast(string text)
	{
		// should do something to queue messages for display
		//if (coroutine != null) StopCoroutine(coroutine);
		StopAllCoroutines();
		Text = text;
	}
}
