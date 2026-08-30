using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    

    // Distancia que se mantiene entre la cámara y el jugador.
    // X mueve la cámara hacia los lados.
    // Y controla la altura.
    // Z controla qué tan atrás se encuentra.
    public Vector3 offset = new Vector3(0, 4, -7);

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
