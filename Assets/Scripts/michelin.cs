using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class michelin : MonoBehaviour
{
    [SerializeField] GameObject AI;
    NavMeshAgent agent;
    public Transform[] waypoints;

    public Vector3[] checkpoints;
    int checkpointIndex;
    public float EnemyDistanceRun = 4.0f;
    private Transform player;
    public string playerTag;
    int waypointIndex;
    Vector3 target;
    bool translating;
    // Start is called before the first frame update
    void Start()
    {
        player = null;
        agent = GetComponent<NavMeshAgent>();
        checkpointIndex = 0;
        translating = false;
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (player is null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
            if (playerObj is not null)
                player = playerObj.transform;
        }
        float distance = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distance: "+distance);

        if(distance < EnemyDistanceRun && !translating)
        {
            IterateCheckpointIndex();
            UpdateDestinationCP();
        }
        if(Vector3.Distance(transform.position, target) < 1 && !translating)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if(waypointIndex%4 == 0)
        {
            waypointIndex = 0;
        }
    }

    void IterateCheckpointIndex()
    {
        checkpointIndex++;
        waypointIndex = 4*checkpointIndex;
        if(checkpointIndex == checkpoints.Length)
        {
            checkpointIndex = 0;
        }
    }

    void UpdateDestinationCP()
    {
        translating = true;
    }
}
