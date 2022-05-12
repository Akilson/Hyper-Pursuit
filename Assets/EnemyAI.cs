using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject go;

    public Transform[] waypoints;
    int waypointsIndex;

    bool destroy = false;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,target) < 5)
        {
            if(destroy)
            {
            }
            else
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointsIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointsIndex++;
        if (waypointsIndex == waypoints.Length)
        {
            Destroy(go);
            destroy = true;
            waypointsIndex = 0;
        }
    }
}
