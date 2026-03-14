using UnityEngine;

public class SpriteOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Quanto mais embaixo (menor Y), maior a ordem
        spriteRenderer.sortingOrder = Mathf.Clamp(Mathf.RoundToInt(-transform.position.y * 100), -10, 1000);

    }
}
