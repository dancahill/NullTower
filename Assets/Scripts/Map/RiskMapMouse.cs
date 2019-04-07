using UnityEngine;

public class RiskMapMouse : MonoBehaviour
{
	public RiskMap m_RiskMap;

	private void OnMouseEnter()
	{
		m_RiskMap.TerritoryLabel.text = name;
	}

	private void OnMouseExit()
	{
		m_RiskMap.TerritoryLabel.text = "";
	}

	private void OnMouseUp()
	{
		m_RiskMap.TerritoryLabel.text = name + " selected";
		m_RiskMap.GoToBattleground(name);
	}
}
