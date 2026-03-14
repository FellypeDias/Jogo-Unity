using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;
    public float damageCooldown = 1f;
    public float pushForce = 5f;
    public float stunDuration = 1f; // ⏳ duração do stun

    private float lastHitTime = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time > lastHitTime + damageCooldown)
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            lastHitTime = Time.time;

            // Empurra o jogador
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 pushDirection = (other.transform.position - transform.position).normalized;
                playerRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }

            // Aplica o stun
            PlayerStun playerStun = other.GetComponent<PlayerStun>();
            if (playerStun != null)
            {
                playerStun.Stun(stunDuration);
            }
        }
    }
}
