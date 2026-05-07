//---------------------------------------------------------
// Zona que mantiene el ciclo de ataques del mono activos mientras el jugador esté dentro
// Adrián de la Calle
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class MonkeyArea : MonoBehaviour
{
    private bool detected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if(player != null) detected = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null && detected) detected = false;
    }
    public bool PlayerInArea()
    {
        //El mono utiliza este bool para controlar su ciclo de ataques
        return detected;
    }
} // class MonkeyArea 
// namespace
