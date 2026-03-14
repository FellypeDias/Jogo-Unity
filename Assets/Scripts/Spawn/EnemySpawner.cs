using UnityEngine;

public class EnemySpawner2D : MonoBehaviour
{
    public int initialSpawnCount = 3;
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public int maxEnemies = 10;
    public float minDistanceFromPlayer = 2f;
    public Transform player;
    public LayerMask obstacleLayer;

    private BoxCollider2D boxCollider;
    private int currentEnemyCount = 0;

    void Start()
{
    boxCollider = GetComponent<BoxCollider2D>();

    // Spawna inimigos iniciais
    for (int i = 0; i < initialSpawnCount; i++)
    {
        Vector2 spawnPos = GetSafeSpawnPosition();
        if (spawnPos != Vector2.zero)
        {
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            currentEnemyCount++;
        }
    }

    // Começa o spawn periódico
    InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
}

    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies) return;

        Vector2 spawnPos = GetSafeSpawnPosition();
        if (spawnPos != Vector2.zero)
        {
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            currentEnemyCount++;
        }
    }

    Vector2 GetSafeSpawnPosition()
    {
        int tries = 50;

        while (tries > 0)
        {
            Vector2 pos = GetRandomPositionInBox();

            bool tooCloseToPlayer = player != null && Vector2.Distance(pos, player.position) < minDistanceFromPlayer;
            bool collidingWithObstacle = Physics2D.OverlapCircle(pos, 0.3f, obstacleLayer);

            if (!tooCloseToPlayer && !collidingWithObstacle)
                return pos;

            tries--;
        }

        return Vector2.zero; // se não encontrar posição válida
    }

    Vector2 GetRandomPositionInBox()
    {
        Bounds bounds = boxCollider.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        if (bc != null)
        {
            Gizmos.DrawCube(bc.bounds.center, bc.bounds.size);
        }
    }
}
