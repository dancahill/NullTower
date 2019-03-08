using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameToast : MonoBehaviour
{
	public static GameToast instance;

	public GameObject panel;

	private IEnumerator coroutine;
	public Text m_ToastText;
	public static string Text = "";

	private void Awake()
	{
		if (instance != null)
		{
			Debug.Log("More than one GameToast in scene!");
			return;
		}
		instance = this;
	}

	void Update()
	{
		if (Text != "")
		{
			coroutine = ShowText();
			StartCoroutine(coroutine);
		}
		//if (panel.activeSelf && m_ToastText.text == "") panel.SetActive(false);
	}
	IEnumerator ShowText()
	{
		//RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
		//panelRectTransform.sizeDelta.Set(0, 32);
		//panel.SetActive(true);
		// adjusting sizefitter seems to work best. toggling active and changing size both seem broken
		ContentSizeFitter csf = panel.GetComponent<ContentSizeFitter>();
		csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		m_ToastText.text = " " + Text + " ";
		Text = "";
		yield return new WaitForSeconds(2f);
		m_ToastText.text = "";
		//ContentSizeFitter csf = panel.GetComponent<ContentSizeFitter>();
		csf.horizontalFit = ContentSizeFitter.FitMode.MinSize;
		//RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
		//panelRectTransform.localScale.Set(0, 0, 0);
		//panel.SetActive(false);
		yield return 0;
	}

	public void Toast(string text)
	{
		// should do something to queue messages for display
		//if (coroutine != null) StopCoroutine(coroutine);
		StopAllCoroutines();
		Text = text;
	}

	public static void Add(string text)
	{
		GameToast.instance.Toast(text);
	}
}
