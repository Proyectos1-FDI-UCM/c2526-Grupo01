//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class DollEnemy : MonoBehaviour
{
    [SerializeField]
    private float knockbackForceX = 6f;
    [SerializeField]
    private float knockbackForceY = 4f;

    [SerializeField]
    private float fallDelay = 2f;
    [SerializeField]
    private float screamDelay = 2f;
    [SerializeField]
    private float destroytime = 0;


    //[SerializeField]
    //private GameObject smalldollHitbox;       //HITBOX SECUNDARIA 
    [SerializeField]
    private GameObject largedollHitbox;
    
    [SerializeField]
    private Transform SpawnPoint;

    [SerializeField] //ANIMATOR DE ADRIÁN
    private Animator _animator;

    private bool playerDetected = false;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null && !playerDetected)
        {
            playerDetected = true;
            _animator.SetTrigger("grito");
            Invoke(nameof(Scream), screamDelay);
        }
    }
    public void Scream()
    {
        _animator.SetTrigger("caida");
        Invoke(nameof(FallAttack), fallDelay);
    }

    public void FallAttack()
    {
        //Instantiate(smalldollHitbox, SpawnPoint.position, Quaternion.identity);       //HITBOX SECUNDARIA 
        Instantiate(largedollHitbox, SpawnPoint.position, Quaternion.identity);
        Invoke(nameof(deleteself), destroytime);
    }
    private void deleteself()
    {
        Destroy(gameObject);
    }
}
