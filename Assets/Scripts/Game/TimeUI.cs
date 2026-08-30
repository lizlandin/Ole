using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void OnEnable()
    {
        TimeManager.OnSecondChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnSecondChanged -= UpdateTime;
    }

    void Start()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        int tiempoRestante = 90 - TimeManager.Second;

        if (tiempoRestante < 0)
        {
            tiempoRestante = 0;
        }

        int minutos = tiempoRestante / 60;
        int segundos = tiempoRestante % 60;

        timeText.text = "Tiempo: " + minutos + ":" + segundos.ToString("00");
    }
}
