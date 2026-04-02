using UnityEngine;
using UnityEngine.U2D;
//CODIGO PROVISIONAL PARA PROBAR LAS ANIMACIONES


public class animacionprov : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField] private Transform sprite;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Adri te he cambiado lo que habia aqui antes porque si no el joystick no lo detecta y ad+ este código esta + clean
        float move = InputManager.Instance.MovementVector.x;

        //el 0.1 es x si el mando tiene drift
        bool moving = Mathf.Abs(move) > 0.1f;

        _animator.SetBool("isMoving", moving);
    }

    public void Flip(int side)
    {

        Vector3 scale = sprite.localScale;
        scale.x = Mathf.Abs(scale.x) * side;
        sprite.localScale = scale;

    }
}
