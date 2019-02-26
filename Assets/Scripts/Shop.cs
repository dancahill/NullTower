using UnityEngine;

public class Shop : MonoBehaviour
{
	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
	public TurretBlueprint laserBeamer;
	BuildManager buildManager;

	private void Start()
	{
		buildManager = BuildManager.instance;
	}

	public void SelectStandardTurret()
	{
		//print("Standard Turret Selected");
		buildManager.SelectTurretToBuild(standardTurret);
	}
	public void SelectMissileLauncher()
	{
		//print("Missile Launcher Purchased");
		buildManager.SelectTurretToBuild(missileLauncher);
	}
	public void SelectLaserBeamer()
	{
		//print("Laser Beamer Purchased");
		buildManager.SelectTurretToBuild(laserBeamer);
	}
}
