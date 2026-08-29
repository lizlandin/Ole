using UnityEngine;

public class Estrella : MonoBehaviour
{

    public float velocidadRotacion = 100f;
    private ContadorEstrellas contadorEstrellas;

    void Start()
    {
        // Aquí se busca en la escena el objeto que tiene el script
        // que lleva el contador de estrellas.
        contadorEstrellas = FindFirstObjectByType<ContadorEstrellas>();
    }

    void Update()
    {
        // Hace que la estrella gire continuamente para que sea vea más atractiva y sea
        // más fácil identificarla como un objeto coleccionable.
        transform.Rotate(0f, 0f, velocidadRotacion * Time.deltaTime);
    }



    void OnTriggerEnter(Collider other)
    {
        // Revisa si el objeto que tocó la estrella es el jugador.
        if (other.CompareTag("Jugador"))
        {
            Debug.Log("¡Estrella recolectada!");

            // Esta parte le avisa al contador que debe sumar una estrella.
            if (contadorEstrellas != null)
            {
                contadorEstrellas.SumarEstrella();
            }

            // Desactiva la estrella para que desaparezca de la escena.
            gameObject.SetActive(false);
        }
    }
}
