using UnityEngine;
using System.Collections;

public class VidaJugador : MonoBehaviour
{
    public int vidas = 3;
    public float tiempoInvulnerable = 2f;

    private bool vulnerable = true;
    private Animator animator;

    void Start()
    {
        // Busca el Animator del Cowboy.
        animator = GetComponentInChildren<Animator>();
    }

    // Esta función se llama cuando el toro golpea al jugador.
    public void RecibirGolpe()
    {
        // Solo recibe el golpe si no está en su tiempo de protección.
        if (vulnerable)
        {
            vidas--;

            Debug.Log("Vidas restantes: " + vidas);

            // Si todavía tiene vidas, reproduce la animación de golpe.
            if (vidas > 0)
            {
                if (animator != null)
                {
                    animator.SetTrigger("golpe");
                }

                // Inicia el tiempo durante el cual no puede volver a recibir daño.
                StartCoroutine(Invulnerabilidad());
            }
            else
            {
                Morir();
            }
        }
    }

    // Esta corrutina evita que el jugador pierda varias vidas
    // inmediatamente después de recibir un golpe.
    IEnumerator Invulnerabilidad()
    {
        vulnerable = false;

        // Espera 2 segundos antes de permitir otro golpe.
        yield return new WaitForSeconds(tiempoInvulnerable);

        vulnerable = true;
    }

    // Esta función se ejecuta cuando el jugador llega a 0 vidas.
    // Activa la animación de muerte y detiene el movimiento
    // del jugador y del toro porque la partida ya terminó.
    void Morir()
    {
        Debug.Log("¡El jugador ha perdido todas sus vidas!");

        if (animator != null)
        {   
            animator.SetTrigger("morir");
        }

        // Sirve para buscar entre los componentes asignados al mismo GameObject, en este caso 
        // puede buscar en los scripts que esten asignados al Cowboy.
        MovimientoJugador movimientoJugador = GetComponent<MovimientoJugador>();

        if (movimientoJugador != null)
        {   // descactiva el movimiento del cowboy
            movimientoJugador.enabled = false;
        }

        // Sirve para buscar en toda la scene el primer objeto que tenga el script MovimientoToro, en este caso
        //  el script esta en el toro y no en el jugador que es el GameObjecct donde estamos, entonces GetComponent 
        // no funcionaría.
        MovimientoToro movimientoToro = FindFirstObjectByType<MovimientoToro>();

        if (movimientoToro != null)
        {   // descactiva el movimiento del toro
            movimientoToro.DetenerToro();
        }

    // Uso de IA:
    // Se utilizó IA para ayudar a resolver un problema que no sabía cómo solucionar.
    // Cuando el jugador moría, su animación lo dejaba acostado, pero el Capsule
    // Collider seguía en posición vertical entonces al morir el personaje quedaba elevado
    // en lugar de estar tirado en el suelo.
    // Con ayuda de IA se encontró la solución de modificar el collider al momento
    // de morir para que se adaptara mejor a la posición del personaje.
        CapsuleCollider colliderJugador = GetComponent<CapsuleCollider>();

        if (colliderJugador != null)
        {
            colliderJugador.direction = 2;
            colliderJugador.height = 1f;
            colliderJugador.radius = 0.5f;
        }
    }
}
