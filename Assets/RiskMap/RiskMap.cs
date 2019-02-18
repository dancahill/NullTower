using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PolygonCollider2D))]
public class RiskMap : MonoBehaviour
{
	public SpriteRenderer sprite;
	//public LevelSelector levelSelector;
	public string TerritoryName = "Unknown";
	public Text TerritoryLabel;
	public SceneFader sceneFader;

	void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		//levelSelector = (LevelSelector)gameObject.AddComponent(typeof(LevelSelector));
	}

	private void OnMouseEnter()
	{
		Debug.Log("RiskMap OnMouseEnter: " + TerritoryName);
		TerritoryLabel.text = TerritoryName;
	}

	private void OnMouseExit()
	{
		Debug.Log("RiskMap OnMouseExit: " + TerritoryName);
		TerritoryLabel.text = "";
	}

	private void OnMouseUp()
	{
		Debug.Log("RiskMap OnMouseUp: " + TerritoryName);
		TerritoryLabel.text = TerritoryName + " clicked";
		//levelSelector.Select("Level01");
		string maptoload = TerritoryName == "Ontario" ? "Level01" : "Level02";

		// should use scenefader...
		//Time.timeScale = 1;
		//SceneManager.LoadScene(maptoload);
		sceneFader.FadeTo(maptoload);
	}
}
