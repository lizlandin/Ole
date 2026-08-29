using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float salto = 5f;

    

    private Rigidbody rb;
    private Vector3 movimiento;
    private Animator animator;
    private bool enSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Evita que el jugador gire al colisionar con otros objetos
        rb.freezeRotation = true;
        
        // Busca el Animator en el jugador o en alguno de sus objetos hijos
        animator = GetComponentInChildren<Animator>();
        
    }

    void Update()
    {
        // Lee que controles se pusieron en Input Manager para cuando se 
        // tenga que mover 
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Normaliza el vector para mantener velocidad constante
        movimiento = new Vector3(x, 0f, z).normalized;
        

        // Animación del movimiento 
        if (animator != null)
        {
            animator.SetFloat("velocidad", movimiento.magnitude);
        }

        // SALTAR
        // se configura en el Input Manager y solo va a permitir el salto si el personaje esta en el suelo.
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            rb.AddForce(Vector3.up * salto, ForceMode.Impulse);
            // una vez que se presiona space para saltar actualizamos el valor de enSuelo a false
            enSuelo = false;

            if (animator != null)
            {
                animator.SetTrigger("saltar");
                animator.SetBool("enSuelo", false);
            }

        }

        // ROTACIÓN DEL PERSONAJE
        // gira al jugador en la dirección en la que se esta moviendo

        if (movimiento.sqrMagnitude > 0.01f)
        {
            // Gira al jugador en la dirección del movimiento
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(movimiento),
                12f * Time.deltaTime
            );
        }
    }

// Uso de AI: cmabio sugerido en el movimiento por AI debido a que mi personaje estaba atrevasando los Mesh Collider.

// MOVIMIENTO DEL JUGADOR:
// Se utiliza linearVelocity para controlar la velocidad del Rigidbody en los
// ejes X y Z. En lugar de indicar directamente la siguiente posición del jugador,
// se establece la velocidad con la que debe desplazarse y Unity se encarga de
// actualizar su posición y el como actuar con las colisiones mediante el sistema de físicas.
//
// Aquí se conserva la velocidad del eje Y para no interferir con la gravedad y para
// que después se puedan hacer movimientos verticales, como saltar.

// Antes, con MovePosition, es como si le dijeramos al personaje "muévete a esta posición" con una
// fórmula, y ahora es "muévete a esta velocidad y deja que la física de Unity determine el "desplazamiento"
// lo cual ya hizo que el Cowboy pudiera chocar con los límites de la Plaza/
void FixedUpdate()
{
    rb.linearVelocity = new Vector3( movimiento.x * velocidad, rb.linearVelocity.y, movimiento.z * velocidad);
}

// SUELO

// Uso de AI: Se me propusieron diferentes alternativas para detectar si el jugador estaba
// en el suelo, una de ellas era utilizar un Raycast, sin embargo, decidí
// utilizar Tags porque fue la opción que más entendí ya que ya habiamos visto como se usan los Tags en clase.

// Si el personaje esta tocando un objeto que tenga el tag de "suelo" puede saltar.


// Revisa si el objeto que está tocando tiene el Tag "Superficie".
// Si si lo tiene, significa que el jugador está sobre una superficie en la que puede apoyarse y volver a saltar.
void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Superficie"))
        {
            enSuelo = true;

            // Si el jugador tiene un Animator, le avisamos que está en el suelo para que pueda 
            // cambiar a la animación que toca.

            if (animator != null)
            {
                animator.SetBool("enSuelo", true);
                
            }
        }
    }

// Esta función es para cuando el jugador deja de tocar otro objeto.
void OnCollisionExit(Collision collision)
    {

        // Si el objeto que dejó de tocar era una "Superficie", quiere decir que el jugador 
        // ya no está en  ella, por ejemplo, si acaba de saltar.
        if (collision.gameObject.CompareTag("Superficie"))
        {
            enSuelo = false;

             // Aquí se le dice al Animator que el jugador ya no está en el suelo para que cambie 
             // las animaciones del salto.

            if(animator != null)
            {
                animator.SetBool("enSuelo", false);
            }
        }
    }

}
