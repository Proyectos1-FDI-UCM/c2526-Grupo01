//---------------------------------------------------------
// Contiene el componente ChangeScene
// Guillermo Jiménez Díaz, Daniel García Andrés
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
    private const string FIRST_LEVEL_NAME = "Nivel1Final";
    private const string SECOND_LEVEL_NAME = "Nivel2Final";
    private const string BOSS_LEVEL_NAME = "Nivel3Boss";
    //no me deja poner un Vector3 como constante asi que para que nadie lo toque lo dejo en así en lugar de serializable.
    private Vector3 FIRST_SPAWNPOINT = new Vector3(-124f,0f,0f);
    private Vector3 SECOND_SPAWNPOINT = new Vector3(3f, -26f, 0f);
    private Vector3 THIRD_SPAWNPOINT = new Vector3(0f, 4f, 0f);

    private string level;

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

    //lleva al menu principal
    public void MenuPrincipal()
    {
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuPrincipal");
        System.GC.Collect();
    }

    //lleva al primer nivel
    public void PrimerNivel()
    {
        GameManager.Instance.SetInitialRespawnPoint(FIRST_SPAWNPOINT);
        GameManager.Instance.SetRespawnPoint(FIRST_SPAWNPOINT);
        GameManager.Instance.setActualLevelScene(FIRST_LEVEL_NAME);
        GameManager.Instance.ChangeScene("Nivel1Final");
   

    }

    //lleva a la cinemática
    public void Cinematica()
    {
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Cinematica");
        System.GC.Collect();
    }

    //sale del juego
    public void SalirDelJuego()
    {
        Application.Quit();
    }

    //nivel 2
    public void SegundoNivel()
    {
        GameManager.Instance.SetInitialRespawnPoint(SECOND_SPAWNPOINT);
        GameManager.Instance.SetRespawnPoint(SECOND_SPAWNPOINT);
        GameManager.Instance.setActualLevelScene(SECOND_LEVEL_NAME);
        GameManager.Instance.ChangeScene("Nivel2Final");

    }

    //nivel boss
    public void BossNivel()
    {
        GameManager.Instance.SetInitialRespawnPoint(THIRD_SPAWNPOINT);
        GameManager.Instance.SetRespawnPoint(THIRD_SPAWNPOINT);
        GameManager.Instance.setActualLevelScene(BOSS_LEVEL_NAME);
        GameManager.Instance.ChangeScene("Nivel3Boss");

    }

    //te lleva al menu de selección de nivel
    public void SelectorDeNivel()
    {
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelectorNivel");
        System.GC.Collect();
    }

    //te lleva a los créditos
    public void Creditos()
    {
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Creditos");
        System.GC.Collect();
    }

    public void DeadRespawn()
    {
        level = GameManager.Instance.getActualLevel();

        if (level == FIRST_LEVEL_NAME)
        {
            GameManager.Instance.SetInitialRespawnPoint(FIRST_SPAWNPOINT);
            GameManager.Instance.SetRespawnPoint(FIRST_SPAWNPOINT);
        }

        if (level == SECOND_LEVEL_NAME)
        {
            GameManager.Instance.SetInitialRespawnPoint(SECOND_SPAWNPOINT);
            GameManager.Instance.SetRespawnPoint(SECOND_SPAWNPOINT);
        }

        if (level == BOSS_LEVEL_NAME)
        {
            GameManager.Instance.SetInitialRespawnPoint(THIRD_SPAWNPOINT);
            GameManager.Instance.SetRespawnPoint(THIRD_SPAWNPOINT);
        }


        //UnityEngine.SceneManagement.SceneManager.LoadScene(level);
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
