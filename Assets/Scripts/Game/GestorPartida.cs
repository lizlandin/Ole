using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GestorPartida : MonoBehaviour
{
    public int metaEstrellas = 3;
    public TextMeshProUGUI estrellasText;
    public TextMeshProUGUI resultadoText;
    public FeedbackUI feedbackUI;

    private int estrellasRecolectadas = 0;
    private bool terminada = false;

    void Start()
    {
        estrellasText.text = "Estrellas: " + estrellasRecolectadas + " / " + metaEstrellas;
    }

    // Revisa si ya se recolectaron todas las estrellas.
    public bool MisionCumplida
    {
        get { return estrellasRecolectadas >= metaEstrellas; }
    }

    // Se suscribe al evento del TimeManager.
    public void OnEnable()
    {
        TimeManager.OnSecondChanged += TimeCheck;
    }

    // Se desuscribe cuando el objeto se desactiva.
    public void OnDisable()
    {
        TimeManager.OnSecondChanged -= TimeCheck;
    }

    // Se llama cada vez que el jugador recoge una estrella.
    public void RecolectarEstrella()
    {
        if (terminada) return;

        estrellasRecolectadas++;

        // Este es para el texto de la UI
        estrellasText.text = "Estrellas: " + estrellasRecolectadas + " / " + metaEstrellas;

        if (MisionCumplida)
        {
            feedbackUI.MostrarMensaje("¡Misión cumplida! Ahora sobrevive hasta el final.");
        }
        else
        {
            feedbackUI.MostrarMensaje(
                "¡Bien hecho! " + estrellasRecolectadas + " de " + metaEstrellas
            );
        }

        // Esta es para la consola
        Debug.Log("Estrella recolectada (" + estrellasRecolectadas + " de " + metaEstrellas + ")"
        );

        if (MisionCumplida)
        {
            Debug.Log("Todas las estrellas fueron recolectadas. Sobrevive hasta que se acabe el tiempo.");
        }
    }

    // Se ejecuta cada vez que cambia el segundo.
    private void TimeCheck()
    {
        if (terminada) return;

        // Cuando quedan 20 segundos, el toro aumenta su velocidad.
        if (TimeManager.Second == 70)
        {
            MovimientoToro toro = FindFirstObjectByType<MovimientoToro>();

            if (toro != null)
            {
                toro.Enfurecer();
            }

            feedbackUI.MostrarMensaje(
                "¡El toro se ha enfurecido! ¡Resiste los últimos 20 segundos!"
            );
        }

        // 120 segundos = 2 minutos.
        if (TimeManager.Second >= 90)
        {
            if (MisionCumplida)
            {
                Ganar();
            }
            else
            {
                Perder();
            }
        }
    }

    public void Ganar()
    {
        if (terminada) return;

        terminada = true;
        // Muestra en la consola cuando se gana
        Debug.Log("¡Ganaste!");

        // Muestra en la UI cuando se gana
        resultadoText.gameObject.SetActive(true);
        resultadoText.text = "¡GANASTE!";

        MovimientoJugador jugador = FindFirstObjectByType<MovimientoJugador>();

        if (jugador != null)
        {
            jugador.Celebrar();
        }

        MovimientoToro toro = FindFirstObjectByType<MovimientoToro>();

        if (toro != null)
        {
            toro.Derrotado();
        }

    }

    public void Perder()
    {
        if (terminada) return;

        terminada = true;

        // lo muestra en consola
        Debug.Log("¡Perdiste!");

        // lo muestra en UI
        resultadoText.gameObject.SetActive(true);
        resultadoText.text = "¡PERDISTE!";

        // Espera 1.5 segundos antes de reiniciar el nivel.
        Invoke("Reiniciar", 5f);
    }

    void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
