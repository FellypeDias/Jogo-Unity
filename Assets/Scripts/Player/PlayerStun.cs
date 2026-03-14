using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    public bool isStunned { get; private set; }
    private float stunTimer = 0f;

    public void Stun(float duration)
    {
        isStunned = true;
        stunTimer = duration;
    }

    void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
            }
        }
    }
}
