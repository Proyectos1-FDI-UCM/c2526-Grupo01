//---------------------------------------------------------
// Sript para teletransportar al jugador a otra escena cuando entra por una puerta
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
// Sript para teletransportar al jugador a otra escena cuando entra por una puerta
/// </summary>
public class ChangeLevel : MonoBehaviour
{
    //el nombre de la esena a la que queremos ir
    [SerializeField]
    private string nextSceneName;

    //para sumarle uno al index d las escenas
    [SerializeField]
    private int changeScene;

    //el punto de aparíción de la siguiente escena
    [SerializeField]
    private Vector3 nextSpawnPoint;

    //para que no se sumen de más, es decir q no pase de la escena 1 a la 3 por tener varios contactos
    private bool hasTriggered = false;


    private void Start()
    {
        hasTriggered = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerMovement jugador = collision.GetComponent<PlayerMovement>();

        if (jugador != null)
        {
            if(hasTriggered == false) 
            {
                hasTriggered = true;

                GameManager.Instance.ResetRespawn(nextSpawnPoint);
                GameManager.Instance.NextNumberScene(changeScene);
                UnityEngine.SceneManagement.SceneManager.LoadScene(GameManager.Instance.GetScene());
            }


        }
    }

} 
