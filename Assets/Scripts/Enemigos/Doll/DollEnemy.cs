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
    private float fallDelay = 2f; //PARA QUE CUADRE CON LA ANIMACIÓN: 2f
    [SerializeField]
    private float screamDelay = 2f; //2f
    [SerializeField]
    private float destroyTime = 0; //0f


    [SerializeField]
    private GameObject largeDollHitbox;

    [SerializeField] //ANIMATOR DE ADRIÁN
    private Animator _animator;

    private bool playerDetected = false;
    private float timer = 0f;
    private Vector3 spawnPoint;

    private int state = 0;
    // 0 = idle
    // 1 = waiting scream
    // 2 = waiting fall
    // 3 = destroy

    public void Start()
    {
        _animator = GetComponent<Animator>();
        spawnPoint = transform.position;
    }

    private void FixedUpdate()
    {
        if (!playerDetected) return;

        timer += Time.deltaTime;

        switch (state)
        {
            case 1: // Espera grito
                if (timer >= screamDelay)
                {
                    timer = 0f;
                    Scream();
                }
                break;

            case 2: // Espera caída
                if (timer >= fallDelay)
                {
                    timer = 0f;
                    FallAttack();
                }
                break;

            case 3: // Espera destruirse
                if (timer >= destroyTime)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null && !playerDetected)
        {
            playerDetected = true;

            _animator.SetTrigger("grito");

            state = 1;
            timer = 0f;
        }
    }

    public void Scream()
    {
        _animator.SetTrigger("caida");

        state = 2;
        timer = 0f;
    }

    public void FallAttack()
    {
        Instantiate(largeDollHitbox, spawnPoint, Quaternion.identity);

        state = 3;
        timer = 0f;
    }
}
