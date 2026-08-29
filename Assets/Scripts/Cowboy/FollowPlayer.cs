using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    // Distancia que se mantiene entre la cámara y el jugador.
    // X mueve la cámara hacia los lados.
    // Y controla la altura.
    // Z controla qué tan atrás se encuentra.
    public Vector3 offset = new Vector3(0, 8, -8);

    void LateUpdate()
    {
        // La cámara toma la posición actual del jugador y le suma el offset.
        // De esta manera sigue al jugador manteniendo siempre la misma distancia.
        transform.position = player.transform.position + offset;
    }
}
