using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int EnemyID = 10;

    [Header("Chase Settings")]
    public float moveSpeed = 2f;
    public float chaseRadius = 5f;

    private Transform player;
    private Animator animator;
    private Rigidbody2D rb;

    private EnemyAttack enemyAttack; // reference to your attack script

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Enemy should NOT move while attacking
        if (enemyAttack != null && animator.GetBool("isAttacking"))
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isMoving", false);
            return;
        }

        // Chase the player when inside chase radius
        if (distance <= chaseRadius && distance > enemyAttack.attackRadius)
        {
            ChasePlayer();
        }
        else
        {
            StopMoving();
        }
    }

    private void ChasePlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;

        // set run blend tree parameters
        animator.SetFloat("InputX", dir.x);
        animator.SetFloat("InputY", dir.y);

        animator.SetBool("isMoving", true);

        // update last facing direction for idle
        animator.SetFloat("LastInputX", dir.x);
        animator.SetFloat("LastInputY", dir.y);
    }

    private void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isMoving", false);
    }

    // visualize chase radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
