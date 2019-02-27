using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RiskToast : MonoBehaviour
{
	public Text m_ToastText;
	public static string Text = "";

	void Update()
	{
		if (Text != "")
		{
			StartCoroutine(ShowText());
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

	public static void Toast(string text)
	{
		// should do something to queue messages for display
		Text = text;
	}
}
