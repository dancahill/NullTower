using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
	[Header("Prefabs")]
	public GameObject m_Ground;
	public GameObject m_GroundPlane;
	public GameObject m_Node;
	public GameObject m_Waypoint;
	public GameObject m_Start;
	public GameObject m_End;
	[Header("Stuff")]
	public GameObject environment;
	public GameObject nodes;
	public GameObject waypoints;

	void Start()
	{
		List<int[]> wpdata = new List<int[]>() {
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
		environment = new GameObject("Environment");
		nodes = new GameObject("Nodes");
		waypoints = new GameObject("Waypoints");

		GameObject gpprefab = (GameObject)Resources.Load("Prefabs/GroundPlane", typeof(GameObject));
		if (gpprefab != null)
		{
			print("found groundplaneprefab");
			GameObject gp = Instantiate(gpprefab, new Vector3(0, -1, 0), Quaternion.identity);
		}
		else
		{
			print("groundplaneprefab is null - using linked prefabs");
			GameObject gp = Instantiate(m_GroundPlane, new Vector3(0, -1, 0), Quaternion.identity, environment.transform);
			gp.transform.localScale = new Vector3(1000, 1, 1000);
			for (int y = 0; y < 16; y++)
			{
				for (int x = 0; x < 16; x++)
				{
					GameObject node = Instantiate(m_Node, nodecords(x, y), Quaternion.identity, nodes.transform);
					node.transform.localScale = new Vector3(4, 1, 4);
					Renderer rend = node.GetComponent<Renderer>();
					foreach (int[] wp in wpdata)
					{
						if (wp[0] == x && wp[1] == y)
						{
							rend.material.color = Color.black;
						}
					}
				}
			}
			GameObject startnode = Instantiate(m_Start, endcords(1, 1), Quaternion.identity);
			startnode.transform.localScale = new Vector3(4, 4, 4);
			WaveSpawner.spawnPoint = startnode.transform;
			GameObject endnode = Instantiate(m_End, endcords(14, 14), Quaternion.identity);
			endnode.transform.localScale = new Vector3(4, 4, 4);
		}
		Waypoints.points = new Transform[wpdata.Count];
		int i = 0;
		foreach (int[] wp in wpdata)
		{
			GameObject waypoint = Instantiate(m_Waypoint, wpcords(wp[0], wp[1]), Quaternion.identity, waypoints.transform);
			Waypoints.points[i++] = waypoint.transform;
		}
	}

	void Update()
	{
	}
}
