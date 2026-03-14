using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public GameTime gameTime;
    public Light2D globalLight;

    void Update()
    {
        float normalizedTime = gameTime.hour / 24f;
        float intensity = Mathf.Sin(normalizedTime * Mathf.PI);
        globalLight.intensity = Mathf.Lerp(0.1f, 1f, intensity);
    }
}
