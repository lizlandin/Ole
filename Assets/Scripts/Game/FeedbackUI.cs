using UnityEngine;
using TMPro;
using System.Collections;

public class FeedbackUI : MonoBehaviour
{
    public TextMeshProUGUI mensajeText;

    void Start()
    {
        // Oculta solamente el texto, pero mantiene activo
        // el GameObject y el script FeedbackUI.
        mensajeText.enabled = false;
    }

    public void MostrarMensaje(string mensaje)
    {
        StopAllCoroutines();
        StartCoroutine(MostrarTemporalmente(mensaje));
    }

    IEnumerator MostrarTemporalmente(string mensaje)
    {
        mensajeText.text = mensaje;
        mensajeText.enabled = true;

        yield return new WaitForSeconds(2f);

        mensajeText.enabled = false;
    }
}