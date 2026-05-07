///---------------------------------------------------------
// Script encargado de gestionar el menú de pausa
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
// Añadir aquí el resto de directivas using


/// <summary>
/// Si pulsas ESC / Start se abre/cierra el menu de pausa, pausando el juego
/// (activando o desactivando los scripts del jugador/enemigos)
/// y permitiendo volver al menú principal o salir del juego 
/// </summary>
public class PauseHUD : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuPausa; //esto es el canvas del menu d pausa

    [SerializeField] //el script del playerMovement del jugador
    private MonoBehaviour player;


    private bool activo;

    private void Start()
    {
        //el menu empieza cerrado
        MenuPausa.SetActive(false);
        activo = false;

        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
    }

    private void Update()
    {
        if (MenuPausa != null)
        {

            if (InputManager.Instance.PauseWasPressedThisFrame())
            {
                UpdateHUD();
            }

        }
        else
        {
            //meto un warning x si se os olvida linkearlo
            Debug.LogWarning("No se ha asociado un MenuPausa en el [Serialize]");
        }

    }

    //si esta activo se oculta y viceversa
    private void UpdateHUD()
    {
        //busca todos los gameObjects en la escena que tenga el script Enemy.cs (los enemigos vaya) para luego desactivarlos
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        if (activo)
        {
            //reanuda todas las físicas
            Physics2D.simulationMode = SimulationMode2D.FixedUpdate;

            MenuPausa.SetActive(false);
            activo = false;

            //se activa el jugador
            if (player != null)
            {
                //activo d nuevo todos los scripts del jugador
                MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();

                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = true;
                }


         
            }

            //se recorre el array entero activando todos los enemigos
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null)
                {
                    //creo un array de scripts de lo enemigos
                    MonoBehaviour[] scripts = enemy.GetComponents<MonoBehaviour>();

                    //recorro todos los scripts dentro de cada enemigo y activo o deasctivo todos
                    foreach (MonoBehaviour script in scripts)
                    {
                        //activamos los scripts
                        script.enabled = true;
                    }
                }
            }

        }
        else if (!activo)
        {
            //pausa todas las físicas
            Physics2D.simulationMode = SimulationMode2D.Script;

            MenuPausa.SetActive(true);
            activo = true;

            //se desactiva el jugador
            if (player != null)
            {
                //busco y desactivo d nuevo todos los scripts del jugador
                MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();

                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = false;
                }
            }

            //se desactivan
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null)
                {
                    //recorro todos los scripts dentro de cada enemigo y activo o deasctivo todos
                    MonoBehaviour[] scripts = enemy.GetComponents<MonoBehaviour>();

                    foreach (MonoBehaviour script in scripts)
                    {
                        script.enabled = false;
                    }
                }
            }

            MonoBehaviour[] scriptsGancho = player.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scriptsGancho)
            {
                script.enabled = false;
            }

        }
    }


}
