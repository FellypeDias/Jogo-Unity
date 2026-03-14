using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public Text timeText;
    public GameTime gameTime;

    void Update()
    {
        timeText.text = gameTime.GetFormattedTime();
    }
}
