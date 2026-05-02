//---------------------------------------------------------
// Código para zonas de cambios circunstanciales del enfoque de la cámara
// Leopoldo Gutiérrez Cobo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using



/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class CameraTrigger : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    //componente CameraFollow serializado
    [SerializeField]
    private CameraFollow camera;


    //Los límites que establecerá cada trigger individualmente
    [Header("Límites")]
    [SerializeField]
    private float CTlimitUp;
    [SerializeField]
    private float CTlimitDown;
    [SerializeField]
    private Collider2D playerBox;



    #endregion

    // ---- ATRIBUTOS PRIVADOS ----

    //Métodos auxiliares para proteger los valores iniciales de la cámara y
    //recuperarlos posteriormente.
    private float auxUp;
    private float auxDown;



    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    
    

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados


    //Detección de entrada del player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerBox)
        {
            //Guardamos valores anteriores
            auxUp = camera.GetLimitUp();
            auxDown = camera.GetLimitDown();

            Debug.Log("1Valores arriba abajo: " + auxUp + " " + auxDown);

            //Establecemos los valores deseados
            camera.SetLimitUp(CTlimitUp);
            camera.SetLimitDown(CTlimitDown);
            Debug.Log("2Valores arriba abajo: " + auxUp + " " + auxDown);

        }
    }


    //Detección de salida del player
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other==playerBox)
        {
            Debug.Log("3Valores arriba abajo: " + auxUp + " " + auxDown);

            //Se reestablecen los valores iniciales
            camera.SetLimitUp(auxUp);
            camera.SetLimitDown(auxDown);

            //Se resetean los valores auxiliares para cambios futuros
            auxUp = 0f;
           auxDown = 0f;
        }
    }
    #endregion   

} // class CameraTrigger 
// namespace
