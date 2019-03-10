using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	//public Transform target;
	NavMeshAgent agent;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(GameObject.Find("End(Clone)").transform.position);
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
