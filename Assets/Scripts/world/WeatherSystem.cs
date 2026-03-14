using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    public ParticleSystem chuvaEffect;
    public ParticleSystem neveEffect;
    public GameTime gameTime;

    void Start()
    {
        GameTime.OnTimeAdvanced += AtualizarClima;
        AtualizarClima(); // iniciar já aplicando o clima
    }

    void AtualizarClima()
    {
        chuvaEffect.Stop();
        neveEffect.Stop();

        switch (gameTime.currentWeather)
        {
            case GameTime.Weather.Chuva:
                chuvaEffect.Play();
                break;
            case GameTime.Weather.Neve:
                neveEffect.Play();
                break;
        }
    }

    void OnDestroy()
    {
        GameTime.OnTimeAdvanced -= AtualizarClima;
    }
}
