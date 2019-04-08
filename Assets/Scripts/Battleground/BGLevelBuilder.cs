using UnityEngine;
using UnityEngine.AI;

public class BGLevelBuilder : MonoBehaviour
{
	[Header("Prefabs")]
	public GameObject m_GroundPlanePrefab;
	public GameObject m_GroundPrefab;
	public GameObject m_NodePrefab;
	public GameObject m_WaypointPrefab;
	public GameObject m_StartPrefab;
	public GameObject m_EndPrefab;
	public TurretBlueprint[] blueprints;
	//[Header("Runtime Stuff")]
	GameObject m_Environment;
	GameObject m_Nodes;
	GameObject m_Waypoints;

	void Start()
	{
		Vector3 nodecords(float x, float y)
		{
			return new Vector3(x * 5, 0, -y * 5);
		}
		Vector3 endcords(float x, float y)
		{
			return new Vector3(x * 5, 2.5f, -y * 5);
		}
		Vector3 wpcords(float x, float y)
		{
			return new Vector3(x * 5, 1f, -y * 5);
		}
		void removenodeat(Node[] nodes, float x, float y)
		{
			foreach (Node n in nodes)
			{
				if (n.transform.position.x / 5 == x && -n.transform.position.z / 5 == y)
				{
					n.gameObject.SetActive(false);
					Destroy(n.gameObject);
				}
			}
		}
		void setpathnode(Node[] nodes, float x, float y)
		{
			removenodeat(nodes, x, y);
			GameObject ground = Instantiate(m_GroundPrefab, nodecords(x, y), Quaternion.identity, m_Environment.transform);
			ground.transform.localScale = new Vector3(4.9f, 1, 4.9f);
		}
		Territory territory = Territories.Get(GameManager.instance.TerritoryName);
		if (territory == null)
		{
			print("territory is null name='" + GameManager.instance.TerritoryName + "'");
		}

		m_Environment = new GameObject("Environment");
		m_Nodes = new GameObject("Nodes");
		m_Waypoints = new GameObject("Waypoints");

		BattleManager.instance.stats.Money = territory.startMoney;
		BattleManager.instance.stats.Lives = territory.startLives;

		/* SET UP A GROUND PLANE */
		GameObject gp = Instantiate(m_GroundPlanePrefab, new Vector3(0, -1, 0), Quaternion.identity, m_Environment.transform);
		gp.transform.localScale = new Vector3(1000, 1, 1000);

		/* SET UP A SMALL BACKGROUND */
		GameObject gp2 = Instantiate(m_GroundPlanePrefab, new Vector3(37.5f, -0.9f, -37.5f), Quaternion.identity, m_Environment.transform);
		gp2.transform.localScale = new Vector3(80, 1, 80);
		Renderer rend2 = gp2.GetComponent<Renderer>();
		rend2.material.color = new Color32(0, 32, 0, 255);

		/* SET UP TURRET NODES */
		// what's better? a map full of nodes, or a small number of nodes we can place at key points on the map?
		if (territory.turretnodes.Length == 0)
		{
			for (int y = 0; y < 16; y++)
			{
				for (int x = 0; x < 16; x++)
				{
					GameObject node = Instantiate(m_NodePrefab, nodecords(x, y), Quaternion.identity, m_Nodes.transform);
					node.transform.localScale = new Vector3(4, 1, 4);
					foreach (MapPoint wp in territory.waypoints)
					{
						if (wp.x == x && wp.y == y)
						{
							Renderer rend = node.GetComponent<Renderer>();
							rend.material.color = Color.black;
						}
					}
				}
			}
		}
		else
		{
			foreach (MapPoint np in territory.turretnodes)
			{
				GameObject node = Instantiate(m_NodePrefab, nodecords(np.x, np.y), Quaternion.identity, m_Nodes.transform);
				node.transform.localScale = new Vector3(4, 1, 4);
			}
		}
		/* SET UP START/END NODES */
		// crashes if no nodes are defined
		// the xml parser should be fixed to guarantee we have a valid map, or fail long before we reach this point
		if (territory.waypoints.Length < 1) Debug.LogError("territory.waypoints for " + territory.name + " is empty - add some to the XML file");
		GameObject startnode = Instantiate(m_StartPrefab, endcords(territory.waypoints[0].x, territory.waypoints[0].y), Quaternion.identity);
		startnode.transform.localScale = new Vector3(4, 4, 4);
		startnode.name = "Start";

		Node[] nl = m_Nodes.GetComponentsInChildren<Node>();

		for (int y = -2; y < 3; y++)
			for (int x = -2; x < 3; x++)
				removenodeat(nl, territory.waypoints[0].x + x, territory.waypoints[0].y + y);

		BGWaveManager.spawnPoint = startnode.transform;
		GameObject endnode = Instantiate(m_EndPrefab, endcords(territory.waypoints[territory.waypoints.Length - 1].x, territory.waypoints[territory.waypoints.Length - 1].y), Quaternion.identity);
		endnode.transform.localScale = new Vector3(4, 4, 4);
		endnode.name = "End";

		for (int y = -1; y < 2; y++)
			for (int x = -1; x < 2; x++)
				removenodeat(nl, territory.waypoints[territory.waypoints.Length - 1].x + x, territory.waypoints[territory.waypoints.Length - 1].y + y);

		/* SET UP WAYPOINTS */
		Waypoints.points = new Transform[territory.waypoints.Length];
		int i2 = 0;
		foreach (MapPoint wp in territory.waypoints)
		{
			GameObject waypoint = Instantiate(m_WaypointPrefab, wpcords(wp.x, wp.y), Quaternion.identity, m_Waypoints.transform);
			Waypoints.points[i2++] = waypoint.transform;
		}
		/* SET UP WAYPOINT PATH TILES */
		for (int i = 0; i < territory.waypoints.Length - 1; i++)
		{
			int minx = Mathf.Min((int)territory.waypoints[i].x, (int)territory.waypoints[i + 1].x);
			int maxx = Mathf.Max((int)territory.waypoints[i].x, (int)territory.waypoints[i + 1].x);
			int miny = Mathf.Min((int)territory.waypoints[i].y, (int)territory.waypoints[i + 1].y);
			int maxy = Mathf.Max((int)territory.waypoints[i].y, (int)territory.waypoints[i + 1].y);

			for (int x = minx; x <= maxx; x++)
				for (int y = miny; y <= maxy; y++)
					setpathnode(nl, x, y);
		}
		// rebuild the nav mesh
		NavMeshSurface nms = m_Environment.AddComponent<NavMeshSurface>();
		nms.layerMask = 1 << LayerMask.NameToLayer("Environment");
		nms.BuildNavMesh();
		if (BattleManager.instance.attackMode) AddAIDefenders();
	}

	public void AddAIDefenders()
	{
		GameObject nodes = GameObject.Find("Nodes");
		Node[] nl = nodes.GetComponentsInChildren<Node>();
		foreach (Node n in nl)
		{
			//if (!n.gameObject.activeSelf)
			//{
			//	Debug.Log("!n.gameObject.activeSelf");
			//}
			BGBuildManager.instance.SelectNodeToBuild(n);
			n.BuildTurret(blueprints[0]);
		}
		NodeUI ui = FindObjectOfType<NodeUI>();
		ui.Hide();
	}
}
