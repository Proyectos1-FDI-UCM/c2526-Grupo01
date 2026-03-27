//---------------------------------------------------------
// Script que modifica el salto para dar una mejor sensación y mayor sencación de control 
// Daniel García Andrés con créditos a André Cardoso de "Mix and Jam", al canal "Board To Bits Games" y a Kyle Pittman
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;



//Script que modifica el salto para dar una mejor sensación y mayor sencación de control 
//haciendo uso de la gravedad y de la cantidad de tiempo que pulses el botón del salto
//para hacer que el personaje no "flota" (como si que nos pasaba antes)

public class BetterJumping : MonoBehaviour
{

    private Rigidbody2D rb;

    //factor de velocidad de caida
    public float fallMultiplier = 2.5f;

    //factor de velocidad de la gravedad para un salto corto (para cortarlo casi en seco)
    public float lowJumpMultiplier = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
