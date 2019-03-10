﻿using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	public Transform target;
	NavMeshAgent agent;

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update()
	{
		agent.SetDestination(target.position);
	}
}
