using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections; // Va a servir para la cuenta regresiva

public class GestorPartida : MonoBehaviour
{
    public int metaEstrellas = 5;
    public FeedbackUI feedbackUI;
    public TextMeshProUGUI resultadoText;

    // Imágenes para la UI
    public Image[] estrellasVisuales;
    public Sprite estrellaVacia;
    public Sprite estrellaLlena;
    public GameObject panelVictoria;
    public GameObject panelDerrota;


    // Elementos del menú inicial
    public GameObject panelInicio;
    public TextMeshProUGUI cuentaRegresiva;

    // Elementos del HUD
    public GameObject fondoTiempo;
    public GameObject barraVidas;
    public GameObject visualEstrellas;

    // Scripts que estarán en pausa antes de comenzar
    public MovimientoJugador jugador;
    public MovimientoToro toro;
    public TimeManager timeManager;


    private int estrellasRecolectadas = 0;
    private bool terminada = false;

    // Guarda si la escena se está reiniciando después de terminar una partida.
    public static bool reinicioDirecto = false;


    // Revisa si ya se recolectaron todas las estrellas.
    public bool MisionCumplida
    {
        get { return estrellasRecolectadas >= metaEstrellas; }
    }


    // Uso de IA:
    // Se utilizó IA como apoyo para hacer que el menú de inicio aparezca solamente
    // cuando se abre el juego por primera vez y para que después
    // de perder o reiniciar, la partida pueda comenzar directamente sin volver
    // a mostrar el menú inicial.

    void Awake()
    {
        // Esta condición revisa si estamos entrando al juego por primera vez.
        // Si sí, detenemos al jugador, al toro y al tiempo para que
        // no empiecen a moverse mientras estamos en el menú.
        if (!reinicioDirecto)
        {
            jugador.enabled = false;
            toro.enabled = false;
            toro.GetComponent<Animator>().enabled = false;
            timeManager.enabled = false;
        }
    }

    void Start()
    {
        // Si reinicioDirecto es true significa que ya jugamos una partida
        // y estamos reiniciando, por lo que ya no quiero mostrar el menú.
        if (reinicioDirecto)
        {
            // Lo regresamos a false para dejarlo listo para el siguiente reinicio.
            reinicioDirecto = false;

            // Quitamos el menú y nos aseguramos de que tampoco aparezca
            // la cuenta regresiva.
            panelInicio.SetActive(false);
            cuentaRegresiva.gameObject.SetActive(false);

            // Volvemos a mostrar toda la interfaz necesaria para jugar.
            fondoTiempo.SetActive(true);
            barraVidas.SetActive(true);
            visualEstrellas.SetActive(true);

            // Aquí activamos al jugador, al toro y el tiempo
            // para que la nueva partida empiece directamente.
            jugador.enabled = true;
            toro.enabled = true;
            timeManager.enabled = true;
        }
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

    public void IniciarPartida()
    {
        panelInicio.SetActive(false);

        StartCoroutine(ConteoInicial());
    }

    // Uso de AI: quería que el juego se sintiera más real y estético,
    // por lo cual agregué una cuenta regresiva antes de que inicie el juego
    // con apoyo de la inteligencia artificial.
    
    // Corrutina que muestra la cuenta regresiva antes de iniciar la partida.
    IEnumerator ConteoInicial()
    {
        // Activa el texto de la cuenta regresiva que inicia oculto.
        cuentaRegresiva.gameObject.SetActive(true);

        // Muestra el número 3 y espera 1 segundo.
        cuentaRegresiva.text = "3";
        yield return new WaitForSeconds(1f);

        // Cambia el texto a 2 y vuelve a esperar 1 segundo.
        cuentaRegresiva.text = "2";
        yield return new WaitForSeconds(1f);

        // Cambia el texto a 1 y espera otro segundo.
        cuentaRegresiva.text = "1";
        yield return new WaitForSeconds(1f);

        // Al terminar la cuenta muestra ¡OLÉ! durante 1 segundo.
        cuentaRegresiva.text = "¡OLÉ!";
        yield return new WaitForSeconds(1f);

        // Oculta la cuenta regresiva porque ya va a comenzar la partida.
        cuentaRegresiva.gameObject.SetActive(false);

        // Activa los elementos de la interfaz que se usan durante el juego.
        fondoTiempo.SetActive(true);
        barraVidas.SetActive(true);
        visualEstrellas.SetActive(true);

        // Activa nuevamente los scripts para que ahora sí comience el juego:
        // el jugador puede moverse, el toro comienza a perseguirlo
        // y el cronómetro empieza a contar.
        jugador.enabled = true;
        toro.enabled = true;
        toro.GetComponent<Animator>().enabled = true;
        timeManager.enabled = true;
    }


    // Se llama cada vez que el jugador recoge una estrella.
    public void RecolectarEstrella()
    {
        if (terminada) return;

        estrellasRecolectadas++;
        ActualizarEstrellasVisuales();

    

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
        panelVictoria.SetActive(true);
        resultadoText.text = "¡VICTORIA!";

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

        // Oculta la interfaz de la partida para dejar solo la pantalla de derrota.
        fondoTiempo.SetActive(false);
        barraVidas.SetActive(false);
        visualEstrellas.SetActive(false);

        // Muestra el panel de derrota.
        panelDerrota.SetActive(true);

        // Espera 5 segundos antes de reiniciar el nivel.
        Invoke("Reiniciar", 5f);
    }

    public void Reiniciar()
    {
        // Indica que al cargar de nuevo la escena no queremos el menú inicial.
        reinicioDirecto = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }



    // Uso de AI: me ayudó a crear una función para cambiar en la UI cuando 
    // el jugador recolectaba una estrella.
    void ActualizarEstrellasVisuales()
    {
        for (int i = 0; i < estrellasVisuales.Length; i++)
        {
            if (i < estrellasRecolectadas)
            {
                estrellasVisuales[i].sprite = estrellaLlena;
            }
            else
            {
                estrellasVisuales[i].sprite = estrellaVacia;
            }
        }
    }

}
