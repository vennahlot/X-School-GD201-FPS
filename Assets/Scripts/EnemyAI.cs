using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform playerTransform;
    PlayerController playerController;
    [SerializeField] private float range = 5.0f;
    [SerializeField] private float damage = 10.0f;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < range)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        animator.SetBool("Attacking", isAttacking);
        if (isAttacking)
        {
            Attack();
        }
        else
        {
            Chase();
        }
    }

    private void Attack()
    {
        agent.isStopped = true;
    }

    private void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(playerTransform.position);
    }

    public void AttackFinished()
    {
        isAttacking = false;
    }

    public void DealDamage()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= range)
        {
            playerController.TakeDamage(damage);
        }
    }
}
