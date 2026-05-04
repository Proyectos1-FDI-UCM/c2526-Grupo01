//---------------------------------------------------------
// Contiene el componente ChangeScene
// Guillermo Jiménez Díaz
// Template-P1
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Componente de pruebas que cambia a otra escena que se
/// configura desde el editor. Se usa principalmente para
/// comunicarse con el GameManager desde un botón y hacer el
/// cambio de escena
/// </summary>
public class ChangeScene : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    
    /// <summary>
    /// Índice de la escena (en el build settings)
    /// que se cargará. 
    /// </summary>

    private const string LAST_SCENE_NAME = "Creditos";
    private const string FIRST_LEVEL_NAME = "Nivel_1";
    //no me deja poner un Vector3 como constante asi que para que nadie lo toque lo dejo en así en lugar de serializable.
    private Vector3 FIRST_SPAWNPOINT = new Vector3(-3,-25,0);

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    /// <summary>
    /// Cambia de escena haciendo uso del GameManager
    /// </summary>
    /// 


    public void MenuPrincipal()
    {
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuPrincipal");
        System.GC.Collect();
    }

    public void NivelPrueba()
    {
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel_1");
        System.GC.Collect();
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void DeadRespawn()
    {
        string level = GameManager.Instance.getActualLevel();

        //esto es para que si has completado el juego
        //al darle a jugar te vuelva a llevar al primer nivel
        //y resetamos tmb el spawnpoint
        if(level == LAST_SCENE_NAME)
        {
            level = FIRST_LEVEL_NAME;
            GameManager.Instance.SetRespawnPoint(FIRST_SPAWNPOINT);
        }

        GameManager.Instance.ChangeScene(level);
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class ChangeScene 
// namespace
