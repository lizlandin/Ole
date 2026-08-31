using UnityEngine;
using System.Collections;

public class MovimientoToro : MonoBehaviour
{
    public GameObject jugador;
    public float velocidad = 2f;
    public float velocidadGiro = 5f;

    private Rigidbody rb;
    private Animator animator;
    private bool atacando = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        // Calcula la dirección desde el toro hasta el jugador.
        Vector3 direccion = jugador.transform.position - transform.position;

        // El toro solamente se mueve sobre el piso.
        direccion.y = 0f;

        // Normaliza la dirección para que la distancia al jugador
        // no cambie la velocidad del toro.
        direccion = direccion.normalized;

        // Mueve al toro hacia el jugador utilizando su Rigidbody.
        // Se conserva la velocidad en Y para no interferir con la gravedad.
        if (! atacando)
        {   
            rb.linearVelocity = new Vector3(
                direccion.x * velocidad,
                rb.linearVelocity.y,
                direccion.z * velocidad );
        
        

            // Hace que el toro vaya girando hacia el jugador.
            if (direccion.sqrMagnitude > 0.01f)
            {
                Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    rotacionObjetivo,
                    velocidadGiro * Time.fixedDeltaTime
                );
        }
        }
    }

    // Detecta cuando el toro choca físicamente contra el jugador.
    void OnTriggerEnter(Collider other)
    {
    if (other.CompareTag("Jugador"))
    {
        Debug.Log("¡El toro golpeó al jugador!");

        if (animator != null)
            {
                StartCoroutine(Atacar());
            }

        VidaJugador vidaJugador = other.GetComponentInParent<VidaJugador>();

        if (vidaJugador != null)
        {
            vidaJugador.RecibirGolpe();
        }
    }
    }

    // Detiene al toro por un momento mientras realiza su ataque.
    // Después puede volver a perseguir al jugador.
    IEnumerator Atacar()
    {
        atacando = true;

        animator.SetTrigger("atacar");

        // Espera mientras se reproduce el ataque.
        yield return new WaitForSeconds(1f);

        atacando = false;
    }


    // Detiene el movimiento del toro y activa su animación Idle
    // cuando el jugador pierde todas sus vidas.
    public void DetenerToro()
    {
        animator.SetTrigger("stop");
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        enabled = false;
    }

    public void Derrotado()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (animator != null)
        {
            animator.SetTrigger("triste");
        }

        enabled = false;
    }

    // Aumenta la velocidad del toro durante los últimos 20 segundos.
    public void Enfurecer()
    {
        velocidad = velocidad + 0.5f;
    }

}