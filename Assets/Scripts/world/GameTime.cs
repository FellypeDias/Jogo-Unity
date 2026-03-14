using UnityEngine;

public class GameTime : MonoBehaviour
{
    public int hour = 6;
    public int day = 1;
    public int month = 1;
    public int year = 1;

    public float timeSpeed = 60f; // quanto mais alto, mais rápido o tempo
    private float timer = 0f;

    public enum Season { Primavera, Verao, Outono, Inverno, Chuvosa, Seca }
    public Season currentSeason;

    public enum Weather { Sol, Nublado, Chuva, Neve }
    public Weather currentWeather;

    public delegate void OnTimeChange();
    public static event OnTimeChange OnTimeAdvanced;

    void Update()
    {
        timer += Time.deltaTime * timeSpeed;

        if (timer >= 60f)
        {
            timer = 0f;
            hour++;

            if (hour >= 24)
            {
                hour = 0;
                day++;
                UpdateDate();
                OnTimeAdvanced?.Invoke(); // Dispara evento para outros sistemas
            }
        }
    }

    void UpdateDate()
    {
        int daysInMonth = 15;
        int monthsInYear = 6;

        if (day > daysInMonth)
        {
            day = 1;
            month++;

            if (month > monthsInYear)
            {
                month = 1;
                year++;
            }
        }

        UpdateSeason();
        UpdateWeather();
    }

    void UpdateSeason()
    {
        switch (month)
        {
            case 1: currentSeason = Season.Primavera; break;
            case 2: currentSeason = Season.Verao; break;
            case 3: currentSeason = Season.Outono; break;
            case 4: currentSeason = Season.Inverno; break;
            case 5: currentSeason = Season.Chuvosa; break;
            case 6: currentSeason = Season.Seca; break;
        }
    }

    void UpdateWeather()
    {
        int chance = Random.Range(0, 100);

        switch (currentSeason)
        {
            case Season.Chuvosa:
                currentWeather = (chance < 70) ? Weather.Chuva : Weather.Nublado;
                break;
            case Season.Inverno:
                currentWeather = (chance < 40) ? Weather.Neve : Weather.Nublado;
                break;
            case Season.Seca:
                currentWeather = (chance < 80) ? Weather.Sol : Weather.Nublado;
                break;
            default:
                currentWeather = (chance < 50) ? Weather.Sol : Weather.Nublado;
                break;
        }
    }

    public string GetFormattedTime()
    {
        return $"{hour:D2}:00 | Dia {day} | Mês {month} | Ano {year} | {currentSeason} | {currentWeather}";
    }
}
