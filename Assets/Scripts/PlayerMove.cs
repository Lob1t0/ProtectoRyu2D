using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float jumpForce = 12f;

    [Header("Ataques")]
    public GameObject hitbox1;   // Hitbox para Attack1
    public GameObject hitbox2;   // Hitbox para Attack2
    public GameObject hitbox3;   // Hitbox para Attack3
    public GameObject hitbox4;   // Hitbox para Attack4

    private Rigidbody2D rb;
    private GroundCheck2D groundCheck;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundCheck2D>();
        animator = GetComponent<Animator>();

        if (hitbox1 != null) hitbox1.SetActive(false);
        if (hitbox2 != null) hitbox2.SetActive(false);
        if (hitbox3 != null) hitbox3.SetActive(false);
        if (hitbox4 != null) hitbox4.SetActive(false);
    }

    private void Update()
    {
        float move = Input.GetAxis("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isGrounded = groundCheck.IsGrounded();

        // Velocidad según si corre o camina
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(move * currentSpeed, rb.linearVelocity.y);

        // Flip por Transform
        if (move > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (move < 0)
            transform.localScale = new Vector3(1, 1, 1);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Debug.Log("Saltando");
        }

        // --- Ataques ---
        if (Input.GetKeyDown(KeyCode.J))
            animator.SetTrigger("Attack1");   // Attack1

        if (Input.GetKeyDown(KeyCode.K))
            animator.SetTrigger("Attack2");  // Attack2

        if (Input.GetKeyDown(KeyCode.L))
            animator.SetTrigger("Attack3");  // Attack3

        if (Input.GetKeyDown(KeyCode.P))
            animator.SetTrigger("Attack4");  // Attack4

        // --- Animaciones ---
        if (!isGrounded)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Jump", false);

            if (Mathf.Abs(move) > 0.01f)
            {
                animator.SetBool("Run", isRunning);
                animator.SetBool("Walk", !isRunning);
            }
            else
            {
                animator.SetBool("Run", false);
                animator.SetBool("Walk", false);
            }
        }
    }

    // --- Métodos para Animation Events Attack1 ---
    public void EnableHitbox1() { if (hitbox1 != null) hitbox1.SetActive(true); }
    public void DisableHitbox1() { if (hitbox1 != null) hitbox1.SetActive(false); }

    // --- Métodos para Animation Events Attack2 ---
    public void EnableHitbox2() { if (hitbox2 != null) hitbox2.SetActive(true); }
    public void DisableHitbox2() { if (hitbox2 != null) hitbox2.SetActive(false); }

    // --- Métodos para Animation Events Attack3 ---
    public void EnableHitbox3() { if (hitbox3 != null) hitbox3.SetActive(true); }
    public void DisableHitbox3() { if (hitbox3 != null) hitbox3.SetActive(false); }

    // --- Métodos para Animation Events Attack4 ---
    public void EnableHitbox4() { if (hitbox4 != null) hitbox4.SetActive(true); }
    public void DisableHitbox4() { if (hitbox4 != null) hitbox4.SetActive(false); }
}
