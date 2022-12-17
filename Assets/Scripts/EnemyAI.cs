
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
    public bool sphereRange = false;

    NavMeshAgent navMeshAgent;
    new Vector3 startPosition;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth health;
    AudioSource audioSource;
    bool insideCollider = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;
        GetComponent<Animator>().SetTrigger("idle");
        if(!sphereRange)
        {
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            Vector3 boxCollider = transform.GetChild(0).GetComponent<BoxCollider>().size;    
        }
    }

    void Update()
    {
        if(!sphereRange)
        {
            insideCollider = transform.GetChild(0).GetComponent<CPUCollider>().InsideCollider();
            CubeAction();
        }
        else
        {
            SphereAction();
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

    void CubeAction()
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
            this.enabled = true;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position);
            
        if(isProvoked)
        {
            EngageTarget();
        }
        else if(insideCollider)
        {
            audioSource.Play();
            audioSource.PlayOneShot(shoutClip, 1f);
            isProvoked = true;
        }

        if(transform.position.x <= startPosition.x +2 
        && transform.position.x >= startPosition.x -2 && !isProvoked)
        {
            GetComponent<Animator>().SetTrigger("idle");
            audioSource.Stop();
        }
    }

    void SphereAction()
    {
        if (health.IsDead())
        {
            audioSource.Stop();
            audioSource.clip = deathSound;
            audioSource.PlayOneShot(deathSound, 1.5f);
            GetComponent<Collider>().enabled = false;
            GetComponent<EnemyAttack>().enabled = false;
            this.enabled = false;
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
        && transform.position.x >= startPosition.x -2 && !isProvoked)
        {
            GetComponent<Animator>().SetTrigger("idle");
            audioSource.Stop();
        }
    }

    void OnDrawGizmosSelected()
        {
            if(sphereRange)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, chaseRange);
                Gizmos.DrawWireSphere(transform.position, returnRange);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, returnRange);
            }
        }
}
