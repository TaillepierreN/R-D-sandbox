using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class enemyBehavior : MonoBehaviour
{
	[SerializeField] GameObject target;
	[SerializeField] Transform RayOrigin;
	NavMeshAgent navMeshAgent;
	RaycastHit hit;

    private void Awake() {
		navMeshAgent = GetComponent<NavMeshAgent>();
	}


    // Update is called once per frame
    void Update()
    {
		var distance = Vector3.Distance(transform.position, target.transform.position);
		
		if (distance > 5.0f)
		{
			if (Physics.Raycast(RayOrigin.position, target.transform.position, out hit))
			{
				Debug.DrawRay(RayOrigin.position, hit.point, Color.green);
				if (hit.transform.name == "Player")
				{
					Debug.Log("i see the player");
					navMeshAgent.SetDestination(target.transform.position);
				}
			}
		} else
		{
			navMeshAgent.SetDestination(transform.position);
		}
    }
}
