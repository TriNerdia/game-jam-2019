using UnityEngine;
using UnityEngine.AI;

public class chasePlayer : MonoBehaviour
{
    NavMeshAgent agent;
    Transform goal;
    public SphereEnemy obj;
    public float speed;

    void Start()
    {
        goal = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        obj = gameObject.transform.GetChild(2).GetComponent<SphereEnemy>();
    }

    void Update()
    {
        if (obj.IsAttacking)
        {
            agent.speed = 0;
        }
        else
        {
            agent.speed = 3;
        }
        agent.destination = goal.position;
    }
}