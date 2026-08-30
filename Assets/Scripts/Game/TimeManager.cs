using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Avisa cuando pasa un segundo.
    public static Action OnSecondChanged;

    // Guarda cuántos segundos han pasado desde que inició la partida.
    public static int Second { get; private set; }
    
    // Un segundo del juego equivale a un segundo real.
    private float secondToRealTime = 1f;

     // Nos permite contar cuánto falta para que pase el siguiente segundo.
    private float timer;

    void Start()
    {
        Second = 0;
        timer = secondToRealTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Second++;

            OnSecondChanged?.Invoke();

            timer = secondToRealTime;
        }
    }
}
