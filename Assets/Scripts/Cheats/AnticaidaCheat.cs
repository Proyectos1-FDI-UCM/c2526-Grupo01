//---------------------------------------------------------
// Cheat para evitar el recorrido del boss final
// Leopoldo Gutiérrez Cobo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Código para desarrollador que hace aparecer una plataforma de ayuda
/// debajo del nivel para que el nivel pueda pasarse fácilmente en caso de atasco.

/// </summary>
public class AnticaidaCheat : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    //Collider y sprites de la plataforma
    private Collider2D col;
    private SpriteRenderer imagen;

    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
   
   
    void Start()
    {
        //Obtenemos los componentes
        col = GetComponent<Collider2D>();
        imagen = GetComponent<SpriteRenderer>();

        //Inicializamos la plataforma de manera invisible e intangible
        col.isTrigger = true;
        imagen.enabled = false;
    }


    void Update()
    {
        //Se llama al método de activación con la C.
        if (Input.GetKeyDown(KeyCode.C))
        {
            CaidaCheat();
        }
        
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos


    //Método que activa la plataforma de ayuda
    public void CaidaCheat()
    {
        //Cambiamos el estado del trigger del collider
        col.isTrigger = !col.isTrigger;

        //Activamos o desactivamos 
        imagen.enabled = !imagen.enabled;
        Debug.Log("Trigger activado: " + col.isTrigger);

    }



    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion   

} // class AnticaidaCheat 
// namespace
