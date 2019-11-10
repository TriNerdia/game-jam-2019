using UnityEngine;
using UnityEngine.AI;

public class chasePlayer : MonoBehaviour
{

    public Transform goal;

    void Start()
    {
        goal = GameObject.FindGameObjectWithTag("Player").transform;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }
}