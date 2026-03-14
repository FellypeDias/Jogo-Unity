using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private PlayerStun playerStun; // referência ao componente de stun

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerStun = GetComponent<PlayerStun>();
    }

    void Update()
{
    // Se estiver atordoado, não move nem sobrescreve a velocidade
    if (playerStun != null && playerStun.isStunned)
    {
        animator.SetBool("isWalking", false);
        animator.SetFloat("InputX", 0);
        animator.SetFloat("InputY", 0);
        return; // ⚠️ Não mexe no linearVelocity aqui
    }

    // Aplica movimento normalmente se não estiver stunado
    rb.linearVelocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Se estiver atordoado, ignora o input
        if (playerStun != null && playerStun.isStunned)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = context.ReadValue<Vector2>();

        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

        if (context.performed)
        {
            animator.SetBool("isWalking", true);
        }

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
    }
}
