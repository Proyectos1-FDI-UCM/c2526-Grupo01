//---------------------------------------------------------
// Accionador de la persecución del boss y del cambio de la cámara
// Loopoldo Gutiérrez Cobo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Activador de la persecución del boss. Cuando el jugador entra en el trigger, 
/// activa el método de persecución de la zona del Boss.
/// </summary>
public class BossTrigger : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    //Componente de la Zona de Muerte del boss
    [SerializeField]
    private BossZone ZonaBoss;
    //Componente del jugador que atravesará el trigger
    [SerializeField]
    private PlayerMovement jugador;

    //Componente de la cámara de la escena de boss
    [SerializeField]
    private CameraBoss cam;

    //Variable para el tamaño ortográfico
    [SerializeField]
    private float sizeCam;

   

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados (private fields)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //El trigger detecta al jugador
        if (collision.GetComponent<PlayerMovement>())
        {
            Debug.Log("BT detecta PlayerMovement");
            //Cambia el tamaño ortográfico de la cam al deseado
            cam.ChangeField(sizeCam);

            //Inicia la fase de persecución de la Zona de muerte
            ZonaBoss.StartChase();
        }

    }
    #endregion


} // class BossTrigger 
// namespace
