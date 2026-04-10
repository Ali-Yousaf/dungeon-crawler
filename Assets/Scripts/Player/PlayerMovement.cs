using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Attack Settings")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireballSpeed = 8f;
    [SerializeField] private float attackDuration = 0.3f;

    [Header("Shield Settings")]
    [SerializeField] private GameObject sheildGameObject;
    [SerializeField] private float shieldDuration = 5f;
    public bool sheildActived = false;
    private Coroutine shieldRoutine;

    // ----- Knockback -----
    private bool isKnockedBack = false;
    private Vector2 knockbackVelocity;
    private float knockbackTimer;
    
    AudioManager audioManager;
    private Animator animator;
    private bool isAttacking = false;
    public bool canAttack = false;

    private void Awake()
    {
        Instance = this;

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>(); 
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();

        if(Input.GetKeyDown(KeyCode.T))
        {
            ActiveShield();
        }

        if (Input.GetKeyDown(KeyCode.Q) && !isAttacking)
        {
            if (canAttack)
            {
                StartCoroutine(AttackRoutine());
                audioManager.PlaySFX(audioManager.fireballThrowSound);
            }

            else
                print("Need to pickup a weapon");
        }
    }

    void HandleMovement()
    {
        // If player is being knocked back, apply knockback velocity instead of input
        if (isKnockedBack)
        {
            rb.linearVelocity = knockbackVelocity;
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
                isKnockedBack = false;
            return; // skip normal movement
        }

        rb.linearVelocity = moveInput * moveSpeed;

        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;

        float dirX = animator.GetFloat("LastInputX");
        float dirY = animator.GetFloat("LastInputY");

        animator.SetTrigger("Attack");
        animator.SetFloat("AttackX", dirX);
        animator.SetFloat("AttackY", dirY);

        Vector2 attackDir = new Vector2(dirX, dirY).normalized;
        Vector2 spawnPos = (Vector2)firePoint.position + attackDir * 0.3f;
        GameObject fireball = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
        fireball.GetComponent<Rigidbody2D>().linearVelocity = attackDir * fireballSpeed;

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
    }

    // ----- NEW: Knockback function -----
    public void ApplyKnockback(Vector2 direction, float force, float duration)
    {
        isKnockedBack = true;
        knockbackTimer = duration;
        knockbackVelocity = direction.normalized * force;
    }

    private void ActiveShield()
    {
        if (sheildActived) return;

        if (ShieldUIManager.Instance.shieldCount <= 0)
        {
            return;
        }

        ShieldUIManager.Instance.UseShield();

        if (shieldRoutine != null)
            StopCoroutine(shieldRoutine);

        shieldRoutine = StartCoroutine(ShieldRoutine());
    }

    private IEnumerator ShieldRoutine()
    {
        sheildActived = true;
        sheildGameObject.SetActive(true);

        yield return new WaitForSeconds(shieldDuration);

        sheildGameObject.SetActive(false);
        sheildActived = false;
    }
}
