using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
	private void Awake()
	{
		GameObject env = GameObject.Find("Environment");
		NavMeshSurface nms = env.AddComponent<NavMeshSurface>();
		//nms.collectObjects = CollectObjects.Children;
		nms.layerMask = 1 << LayerMask.NameToLayer("Environment");
		nms.BuildNavMesh();
	}
}
