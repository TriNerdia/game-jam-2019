using UnityEngine;
using UnityEngine.AI;

public class chasePlayer : MonoBehaviour
{
    NavMeshAgent agent;
    Transform goal;

    void Start()
    {
        goal = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    void Update()
    {
        agent.destination = goal.position;
    }
}