using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float returnRange = 20f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    Vector3 startPosition;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        if(isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    void EngageTarget()
    {
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
        else if(distanceToTarget >= returnRange)
        {
            isProvoked = false;
            navMeshAgent.SetDestination(startPosition);
        }
    }

    void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        Debug.Log("ATTACK!!");
    }

    void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
            Gizmos.DrawWireSphere(transform.position, returnRange);
        }
}
