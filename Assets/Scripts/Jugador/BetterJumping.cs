//---------------------------------------------------------
// Script que modifica el salto para dar una mejor sensación y mayor sencación de control 
// Daniel García Andrés con créditos a André Cardoso de "Mix and Jam", al canal "Board To Bits Games" y a Kyle Pittman
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;



/*Script que modifica el salto para dar una mejor sensación y un mayor control 
haciendo uso de la gravedad y de la cantidad de tiempo que pulses el botón del salto
de este modo, se consigue eliminar la sensación de “flotar” o de "salto globoso".

En concreto, aumenta la velocidad de caída cuando el personaje desciende, 
haciendo que el movimiento sea más rápido y contundente, y aplica una gravedad adicional 
cuando el jugador suelta el botón de salto mientras aún está ascendiendo, 
permitiendo realizar saltos de distinta altura (saltos cortos o largos) según la duración de la pulsación.

Este componente permanece activo de forma continua durante toda la ejecución, 
ya que su comportamiento depende únicamente de la velocidad vertical del jugador
y del estado del input (si se esta pulsando el botón d salto o no) y
al ejecutarse de forma autónoma en Update, no requiere ser gestionado desde PlayerMovement.cs
*/

public class BetterJumping : MonoBehaviour
{

    private Rigidbody2D rb;

    //factor de velocidad de caida
    [SerializeField]
    private float fallMultiplier = 2.5f;

    //factor de velocidad de la gravedad para un salto corto (para cortarlo casi en seco)
    [SerializeField]
    private float lowJumpMultiplier = 2f;

    [Header("Limite del salto (Dejadlo por defecto a 10f)")]
    [SerializeField]
    //limita la subida aunque sigas pulsando el botón de salto, a 10f a mí me parece perfecto la verdad
    private float maxJumpVelocity = 10f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //si el jugador sube más rápido d lo permitido por maxJumpVelocity entonces
        //aplicamos un cap a la velocidad en el eje Y cuando es positiva (mientras esta subiendo debido al salto)
        if (rb.linearVelocity.y > maxJumpVelocity)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxJumpVelocity);
        }


        //si el jugador esta cayendo (el eje y es menor que 0)
        if (rb.linearVelocity.y < 0)
        {
            //entonces aplicamos una gravedad extra hacia abajo para que caiga más rápido
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //si el jugador esta subiendo pero ya ha soltado el botón de saltar
        else if (rb.linearVelocity.y > 0 && !InputManager.Instance.JumpIsPressed())
        {
            //entonces aplicamos una gravedad extra para cortar el salto
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //el (fallMultiplier/lowJumpMultiplier - 1) es para que sea más claro los números en este caso (gravedad x2.5 y x2 respectivamente)
        //ya que si lo quitara y pusiera x1.5 queda menos claro y tocaría hacer cálculos 
    }

}
