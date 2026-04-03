//---------------------------------------------------------
// Dibuja elos BoxCollider2D del GameObject para debug visual
// Hector Prous Arroyo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class VisibilizadorColider : MonoBehaviour
{
    [SerializeField] private Color colorNormal = Color.red;
    [SerializeField] private Color colorTrigger = Color.green;

    private void OnDrawGizmos()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();

        if (col == null) 
        {
            return;
        }

        Vector2 centro = (Vector2)transform.position + col.offset;
        Vector2 tamaño = col.size;

        if (col.isTrigger)
        {
            Gizmos.color = colorTrigger;
        }
        else
        {
            Gizmos.color = colorNormal;
        }

        Gizmos.DrawWireCube(centro, tamaño);
    }
}