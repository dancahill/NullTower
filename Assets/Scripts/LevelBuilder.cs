using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
	[Header("Prefabs")]
	public GameObject m_GroundPlanePrefab;
	public GameObject m_GroundPrefab;
	public GameObject m_NodePrefab;
	public GameObject m_WaypointPrefab;
	public GameObject m_StartPrefab;
	public GameObject m_EndPrefab;
	[Header("Runtime Stuff")]
	public GameObject m_Environment;
	public GameObject m_Nodes;
	public GameObject m_Waypoints;

	void Start()
	{
		List<int[]> wpdata = new List<int[]>() {
			new int[2] { 1, 1 },
			new int[2] { 6, 1 },
			new int[2] { 6, 3 },
			new int[2] { 3, 3 },
			new int[2] { 3, 6 },
			new int[2] { 10, 6 },
			new int[2] { 10, 3 },
			new int[2] { 12, 3 },
			new int[2] { 12, 9 },
			new int[2] { 5, 9 },
			new int[2] { 5, 11 },
			new int[2] { 3, 11 },
			new int[2] { 3, 14 },
			new int[2] { 14, 14 }
		};
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
		m_Environment = new GameObject("Environment");
		m_Nodes = new GameObject("Nodes");
		m_Waypoints = new GameObject("Waypoints");

		GameObject gpprefab = (GameObject)Resources.Load("Prefabs/GroundPlane", typeof(GameObject));
		if (gpprefab != null)
		{
			print("found groundplaneprefab");
			GameObject gp = Instantiate(gpprefab, new Vector3(0, -1, 0), Quaternion.identity);
		}
		else
		{
			print("groundplaneprefab is null - using linked prefabs");
			GameObject gp = Instantiate(m_GroundPlanePrefab, new Vector3(0, -1, 0), Quaternion.identity, m_Environment.transform);
			gp.transform.localScale = new Vector3(1000, 1, 1000);
			for (int y = 0; y < 16; y++)
			{
				for (int x = 0; x < 16; x++)
				{
					GameObject node = Instantiate(m_NodePrefab, nodecords(x, y), Quaternion.identity, m_Nodes.transform);
					node.transform.localScale = new Vector3(4, 1, 4);
					foreach (int[] wp in wpdata)
					{
						if (wp[0] == x && wp[1] == y)
						{
							Renderer rend = node.GetComponent<Renderer>();
							rend.material.color = Color.black;
						}
					}
				}
			}
			GameObject startnode = Instantiate(m_StartPrefab, endcords(1, 1), Quaternion.identity);
			startnode.transform.localScale = new Vector3(4, 4, 4);
			WaveSpawner.spawnPoint = startnode.transform;
			GameObject endnode = Instantiate(m_EndPrefab, endcords(14, 14), Quaternion.identity);
			endnode.transform.localScale = new Vector3(4, 4, 4);

			Waypoints.points = new Transform[wpdata.Count];
			int i2 = 0;
			foreach (int[] wp in wpdata)
			{
				// 
				GameObject waypoint = Instantiate(m_WaypointPrefab, wpcords(wp[0], wp[1]), Quaternion.identity, m_Waypoints.transform);
				Waypoints.points[i2++] = waypoint.transform;
			}

			// slow and dumb way to mark all tiles along the waypoint path - replace later
			for (int i = 0; i < wpdata.Count - 1; i++)
			{
				if (wpdata[i][0] != wpdata[i + 1][0])
				{
					int y = wpdata[i][1];
					Node[] nl = Resources.FindObjectsOfTypeAll<Node>();
					for (int x = Mathf.Min(wpdata[i][0], wpdata[i + 1][0]); x < Mathf.Max(wpdata[i][0], wpdata[i + 1][0]) + 1; x++)
					{
						foreach (Node n in nl)
						{
							//print(string.Format("{0},{1}", n.transform.position.x / 5, -n.transform.position.z / 5));
							if (n.transform.position.x / 5 == x && -n.transform.position.z / 5 == y)
							{
								Destroy(n.gameObject);
								//Renderer rend = n.GetComponent<Renderer>();
								//rend.material.color = Color.grey;
							}
						}
					}
				}
				else
				{
					int x = wpdata[i][0];
					Node[] nl = Resources.FindObjectsOfTypeAll<Node>();
					for (int y = Mathf.Min(wpdata[i][1], wpdata[i + 1][1]); y < Mathf.Max(wpdata[i][1], wpdata[i + 1][1]) + 1; y++)
					{
						foreach (Node n in nl)
						{
							//print(string.Format("{0},{1}", n.transform.position.x / 5, -n.transform.position.z / 5));
							if (n.transform.position.x / 5 == x && -n.transform.position.z / 5 == y)
							{
								Destroy(n.gameObject);
								//Renderer rend = n.GetComponent<Renderer>();
								//rend.material.color = Color.grey;
							}
						}
					}
				}
			}
		}
	}
}
