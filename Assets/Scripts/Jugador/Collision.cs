//---------------------------------------------------------
// Script encargado de detectar las zonas en contacto con el personaje
// Daniel García Andrés con créditos a André Cardoso del canal "Mix and Jam"
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


///Script encargado de detectar las zonas en contacto con el personaje como suelos, paredes o enemigos.  <summary>
//Lo que hace este script es crear 3 circulos basándose en la hitbox circular del jugador (por eso cambie el collider de box a capsule)
//uno debajo por si toca el suelo y los otros 2 en ambos lados por si entra en contacto con una pared
public class Collision : MonoBehaviour
{
    [Header("Capas")]
    [SerializeField]
    private LayerMask groundLayer;

    [Space]
    //Las posibles colisiones: suelo (enemigos entraría aquí también) o pared.
    private bool onGround;
    private bool onWall;

    [Space]

    [Header("Colisiones")]
    [SerializeField]
    private float collisionRadius = 0.3f; //el tamaño de los círculos (el radio)
    [SerializeField]
    private Vector2 bottomOffset, rightOffset, leftOffset;

    [Header("Dimensiones capsula suelo")]
    [SerializeField]
    private Vector2 sizeSuelo = new Vector2(2f, 1.5f); // ancho x alto


    [Header("Dimensiones capsulas Paredes")]

    //capsulas para las paredes
    [SerializeField]
    private Vector2 size = new Vector2(0.5f, 3.18f); // ancho x alto


    void Update()
    {
        onGround = Physics2D.OverlapCapsule((Vector2)transform.position + bottomOffset, sizeSuelo, CapsuleDirection2D.Horizontal, 0f, groundLayer); //detecta suelo

        onWall = Physics2D.OverlapCapsule((Vector2)transform.position + rightOffset, size, CapsuleDirection2D.Vertical, 0f, groundLayer) //detecta pared izquierda
            || Physics2D.OverlapCapsule((Vector2)transform.position + leftOffset, size, CapsuleDirection2D.Vertical, 0f, groundLayer); //detecta pared x la derecha

        //la funcion overlapCircle() lo que hace es devolver true si toca algo.
    }


    public bool IsOnGround()
    {
        return onGround;
    }

    public bool IsOnWall()
    {
        return onWall;
    }





    //esto es únicamente para debugear y solo se ve en la escena, pero basicamente lo que hace es dibujar en la escena los circulos creados para poder visualizarlos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Dibuja los 3 círculos de rojo en este caso

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        DrawCapsuleGizmoHorizontal((Vector2)transform.position + bottomOffset, sizeSuelo);
        
        //paredes
        DrawCapsuleGizmo((Vector2)transform.position + rightOffset, size);
        DrawCapsuleGizmo((Vector2)transform.position + leftOffset, size);
    }

    //para dibujar las capsulas
    void DrawCapsuleGizmo(Vector2 position, Vector2 size)
    {
        float radius = size.x / 2f;
        float height = size.y - (radius * 2f);

        // centros de los círculos
        Vector2 top = position + Vector2.up * (height / 2f);
        Vector2 bottom = position + Vector2.down * (height / 2f);

        // círculos
        Gizmos.DrawWireSphere(top, radius);
        Gizmos.DrawWireSphere(bottom, radius);

        // líneas laterales
        Gizmos.DrawLine(top + Vector2.right * radius, bottom + Vector2.right * radius);
        Gizmos.DrawLine(top + Vector2.left * radius, bottom + Vector2.left * radius);
    }

    void DrawCapsuleGizmoHorizontal(Vector2 position, Vector2 size)
    {
        float radius = size.y / 2f;
        float width = size.x - (radius * 2f);

        Vector2 left = position + Vector2.left * (width / 2f);
        Vector2 right = position + Vector2.right * (width / 2f);

        Gizmos.DrawWireSphere(left, radius);
        Gizmos.DrawWireSphere(right, radius);

        Gizmos.DrawLine(left + Vector2.up * radius, right + Vector2.up * radius);
        Gizmos.DrawLine(left + Vector2.down * radius, right + Vector2.down * radius);
    }


} 
