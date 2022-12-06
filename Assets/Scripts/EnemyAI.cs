using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] public float chaseRange = 5f;
    [SerializeField] float returnRange = 20f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] Transform target;
    public AudioClip deathSound;
    public AudioClip shoutClip;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    Vector3 startPosition;
    EnemyHealth health;
    AudioSource audioSource;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        GetComponent<Animator>().SetTrigger("idle");
    }

    void Update()
    {
        if (health.IsDead())
        {
            audioSource.Stop();
            audioSource.clip = deathSound;
            audioSource.PlayOneShot(deathSound, 1.5f);
            enabled = false;
            navMeshAgent.enabled = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<EnemyAttack>().enabled = false;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        if(isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange)
        {
            audioSource.Play();
            audioSource.PlayOneShot(shoutClip, 1f);
            isProvoked = true;
        }

        if(transform.position.x <= startPosition.x +2 
        && transform.position.x >= startPosition.x -2)
        {
            GetComponent<Animator>().SetTrigger("idle");
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    void EngageTarget()
    {
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance + 0.5f)
        {
            AttackTarget();
        }
        else if(distanceToTarget >= returnRange)
        {
            isProvoked = false;
            navMeshAgent.SetDestination(startPosition);
            audioSource.Stop();
        }
    }

    void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
            Gizmos.DrawWireSphere(transform.position, returnRange);
        }
}
