using UnityEngine;
using TMPro;

public class ContadorEstrellas : MonoBehaviour
{
    public TMP_Text textoEstrellas;

    private int estrellasRecolectadas = 0;

    void Start()
    {
        ActualizarTexto();
    }

    // Esta función se llama cada vez que el jugador recoge una estrella.
    public void SumarEstrella()
    {
        estrellasRecolectadas++;

        ActualizarTexto();

        Debug.Log("Estrellas recolectadas: " + estrellasRecolectadas);
    }

    // Actualiza el texto que aparece en la interfaz del juego.
    void ActualizarTexto()
    {
        textoEstrellas.text = "Estrellas: " + estrellasRecolectadas;
    }
}
