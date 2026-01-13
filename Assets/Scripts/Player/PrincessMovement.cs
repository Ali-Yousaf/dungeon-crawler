using UnityEngine;
using System.Collections;

public class GirlMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
    }

    void HandleInput()
    {
        moveInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            moveInput.y = 1;
        else if (Input.GetKey(KeyCode.S))
            moveInput.y = -1;

        if (Input.GetKey(KeyCode.D))
            moveInput.x = 1;
        else if (Input.GetKey(KeyCode.A))
            moveInput.x = -1;
    }

    void HandleMovement()
    {
        rb.linearVelocity = moveInput.normalized * moveSpeed;

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
}
