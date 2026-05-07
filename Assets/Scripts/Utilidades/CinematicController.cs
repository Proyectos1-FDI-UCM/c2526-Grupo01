//---------------------------------------------------------
// Controlador de cinemática
// Adrián de la Calle y Daniel García Andrés 
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
// Añadir aquí el resto de directivas using


/// <summary>
/// Este script controla la reproducción de la cinemática inicial.
/// Cuando el vídeo termina se cambia automáticamente a otra escena.
/// Además, el jugador puede saltarse la cinemática
/// pulsando el botón de interactuar (E/Y).
/// </summary>
public class CinematicController : MonoBehaviour
{
    //videoplayer con la cinemática
    private VideoPlayer videoPlayer;

    //nombre de la escena del primer nivel
    private const string FIRST_SCENE_NAME = "Nivel_1";

    // Evita cambiar varias veces de escena
    private bool hasFinished;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        hasFinished = false;
    }

    private void Update()
    {
        if (!videoPlayer.isPlaying && !hasFinished)
        {
            //comprueba que el vídeo haya avanzado algo para evitar que cambie instantáneamente
            if (videoPlayer.frame > 0)
            {
                hasFinished = true;
                LoadFirstLevelScene();
            }
        }

        //Si el jugador pulsa la tecla de interactuar, cambiamos directamente de escena
        if (InputManager.Instance.InteractWasPressedThisFrame())
        {
            LoadFirstLevelScene();
        }
    }


    //Carga la escena del primer nivel
    private void LoadFirstLevelScene()
    {
        GameManager.Instance.ChangeScene(FIRST_SCENE_NAME);
    }

} // class CinematicController 
// namespace
