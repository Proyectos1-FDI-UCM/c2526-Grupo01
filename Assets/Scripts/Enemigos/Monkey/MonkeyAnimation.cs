//---------------------------------------------------------
// Control de las animaciones del mono
// Adrián de la Calle Valiente
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class MonkeyAnimation : MonoBehaviour
{
    [SerializeField]
    private Monkey Mono;
    [SerializeField]
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(Mono.IsAttacking()) animator.SetTrigger("IsAttacking");
        else animator.SetTrigger("NotAttacking");
    }
} // class MonkeyAnimation 
// namespace
