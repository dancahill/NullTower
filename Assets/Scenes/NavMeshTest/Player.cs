using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	public Transform target;
	NavMeshAgent agent;

	private void Awake()
	{
		GameObject env = GameObject.Find("Environment");
		NavMeshSurface nms = env.AddComponent<NavMeshSurface>();
		//nms.collectObjects = CollectObjects.Children;
		nms.layerMask = 1 << LayerMask.NameToLayer("Environment");
		nms.BuildNavMesh();
	}

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(target.position);
	}

	void Update()
	{
		//Debug.Log(string.Format("agent [hasPath={0}][remainingDistance={1}]", agent.hasPath, agent.remainingDistance));
		if (agent.remainingDistance > 0)
		{
			//agent.SetDestination(target.position);
		}
		else
		{
			Debug.Log("we're here?");
			gameObject.SetActive(false);
		}
	}
}
