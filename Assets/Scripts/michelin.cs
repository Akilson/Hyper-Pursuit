using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class michelin : MonoBehaviour
{
    [SerializeField] GameObject AI;
    NavMeshAgent agent;
    public Transform[] waypoints;
    private float speeed = 0.01f;
    private float tValue = 0.0f;

    public Vector3[] checkpoints;
    int checkpointIndex;
    public float EnemyDistanceRun = 4.0f;
    private Transform playerWhite;
    private Transform playerBlack;
    public string playerWhiteTag;
    public string playerBlackTag;
    int waypointIndex;
    Vector3 target;
    bool translating;
    // Start is called before the first frame update
    void Start()
    {
        playerWhite = null;
        playerBlack = null;
        agent = GetComponent<NavMeshAgent>();
        checkpointIndex = 0;
        translating = false;
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerWhite is null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag(playerWhiteTag);
            if (playerObj is not null)
                playerWhite = playerObj.transform;
        }
        float distanceWhite;
        if (playerWhite is null)
            distanceWhite = float.MaxValue;
        else
            distanceWhite = Vector3.Distance(transform.position, playerWhite.position);
        
        if (playerBlack is null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag(playerBlackTag);
            if (playerObj is not null)
                playerBlack = playerObj.transform;
        }
        float distanceBlack;
        if (playerBlack is null)
            distanceBlack = float.MaxValue;
        else
            distanceBlack = Vector3.Distance(transform.position, playerBlack.position - new Vector3(0, 100, 0));

        float distanceCP = Vector3.Distance(transform.position, checkpoints[checkpointIndex]);

        if (distanceCP < 1)
        {
            translating = false;
            transform.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            UpdateDestination();
        }
        if (translating)
        {
            UpdateDestinationCP();
            Debug.Log("translating");
        }

        float distance = Mathf.Min(distanceBlack, distanceWhite);
        if(distance < EnemyDistanceRun && !translating)
        {
            agent.SetDestination(transform.position);
            transform.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            IterateCheckpointIndex();
            Debug.Log("Index:" + checkpointIndex + "position: " + checkpoints[checkpointIndex]);
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
            waypointIndex = 0 + checkpointIndex*4;
        }
    }

    void IterateCheckpointIndex()
    {
        checkpointIndex++;
        if(checkpointIndex == checkpoints.Length)
        {
            checkpointIndex = 1;
        }
        waypointIndex = 4*checkpointIndex;
    }

    void UpdateDestinationCP()
    {
        tValue += Time.deltaTime * speeed;
        transform.position = Vector3.Lerp(transform.position, checkpoints[checkpointIndex], tValue);
        translating = true;
    }
}
