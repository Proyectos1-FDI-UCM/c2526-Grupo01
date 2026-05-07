//---------------------------------------------------------
// Código para la zona de final de persecución del boss
// Leopoldo Gutiérrez Cobo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;



/// <summary>
/// Trigger que finaliza el boss. Activa el metodo de still de bosszone
/// </summary>
public class BossEnder : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

   
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    //Si detecta a la BossZone activa su EndChase()
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BossZone boss = collision.GetComponent<BossZone>();


        if (boss != null) boss.EndChase();
    }

    #endregion

   
} // class BossEnder 
// namespace
