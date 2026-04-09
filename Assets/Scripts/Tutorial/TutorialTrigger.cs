//---------------------------------------------------------
// Trigger de tutorial Cuando el jugador entra, avisa al TutorialHUD para que muestre su imagen asignada.
// Hector Prous Arroyo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField]
    private Sprite image;

    [SerializeField]
    private TutorialHUD hud;

    private bool triggered;
    private void OnTriggerEnter2D(Collider2D colision
        )
    {
        if (triggered)
        {
            return;
        }

        if (colision.GetComponent<PlayerMovement>() == null)
        {
            return;
        }

        triggered = true;
        hud.Show(image);
    }
} 

