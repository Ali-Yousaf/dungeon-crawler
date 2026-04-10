using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Detection")]
    public float attackRadius = 1.5f;
    public Transform player;

    [Header("Attack Settings")]
    public float attackCooldown = 1f; // Time between attacks
    private float lastAttackTime;

    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRadius && !PlayerMovement.Instance.sheildActived)
        {
            TryAttack();
        }

        else
        {
            isAttacking = false;
            animator.SetBool("isAttacking", false);
        }
    }

    void TryAttack()
    {
        // Cooldown check
        if (Time.time < lastAttackTime + attackCooldown) return;

        lastAttackTime = Time.time;

        isAttacking = true;
        animator.SetBool("isAttacking", true);

        // Calculate direction from enemy to player
        Vector2 dir = (player.position - transform.position).normalized;

        // Update attack blend tree params
        animator.SetFloat("InputX", dir.x);
        animator.SetFloat("InputY", dir.y);

        // Also save idle direction
        animator.SetFloat("LastInputX", dir.x);
        animator.SetFloat("LastInputY", dir.y);
    }

    public void DealDamage()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Only damage if still close
        if (distance <= attackRadius + 0.3f)
        {
            // Apply player damage
            PlayerHealth hp = player.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(1);
            }

            Debug.Log("Enemy dealt damage to the player.");
        }
    }

    public void InitiateCameraShake()
    {
        print("Called Camera Shake");
        CameraShake.Instance.Shake();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
