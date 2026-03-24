using UnityEngine;
//CODIGO PROVISIONAL PARA PROBAR LAS ANIMACIONES


public class animacionprov : MonoBehaviour
{
    [SerializeField]
    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //bool moving = horizontal != 0f;

        bool moving = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        _animator.SetBool("isMoving", moving);
    }
}
