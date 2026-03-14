using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 5f;
    public float separationDistance = 0.5f;
    public float separationForce = 1f;

    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = Vector2.zero;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange && distanceToPlayer > 0.5f)
        {
            direction = (player.position - transform.position).normalized;
        }

        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, separationDistance);
        foreach (Collider2D col in nearbyEnemies)
        {
            if (col.gameObject != gameObject && col.CompareTag("Enemy"))
            {
                Vector2 away = transform.position - col.transform.position;
                direction += away.normalized * separationForce;
            }
        }

        // Move o inimigo
        transform.position += (Vector3)(direction.normalized * speed * Time.deltaTime);

        // Atualiza animação com base no movimento
        if (anim != null)
        {
            anim.SetFloat("speed", direction.magnitude);

        }
    }
}
