using UnityEngine;
using UnityEngine.AI;

public class AITarget : MonoBehaviour
{
    [Header("AI Settings")]
    public Transform target;
    public float chaseDistance = 10f;
    public float attackRange = 2f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float distanceToTarget;
    private float enemySpeed;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemySpeed = 0;
        navMeshAgent.speed = 0;
    }

    void Update()
    {
        if (target == null)
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("Attack", false);
            return;
        }

        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget > chaseDistance)
        {
            // Target is too far, stop move and stop attack
            navMeshAgent.isStopped = true;
            navMeshAgent.speed = 0;
            animator.SetBool("Attack", false);
            animator.SetBool("isRunning", false);
        }
        else if (distanceToTarget > attackRange)
        {
            // Chase target
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = target.position;
            navMeshAgent.speed = 3.5f;
            animator.SetFloat("moveSpeed", 3.5f);
            animator.SetBool("Attack", false);
            animator.SetBool("isRunning", true);
        }
        else
        {
            // In attack range
            navMeshAgent.isStopped = true;
            navMeshAgent.speed = 0;
            animator.SetFloat("moveSpeed", 0f);
            animator.SetBool("Attack", true);
            animator.SetBool("isRunning", false);

            // Rotate to face target
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;
            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);
            }
        }
        enemySpeed = navMeshAgent.speed;
        animator.SetFloat("moveSpeed", enemySpeed);
    }
}
