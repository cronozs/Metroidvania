using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class HeroController_1 : MonoBehaviour
{
    [Header("Animation Variable")]
    [SerializeField] AnimatorController_1 animatorController;

    [Header("Checker Variables")]                                //Cabecera del ComboBox "Variables"  //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.
    [SerializeField] LayerChecker_1 footA;                  //Instanciamento a la Clase "LayerChecker_1" = footA
    [SerializeField] LayerChecker_1 footB;                  //Instanciamento a la Clase "LayerChecker_1" = footB

    [Header("Rigid Variables")]
    [SerializeField] private float jumpForce;               //Agregamos una variable flotante para agrear furza al salto
    [SerializeField] private float speed_;                  //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.
    [SerializeField] private Vector2 movementDirection;     //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.

    private Rigidbody2D rigidbody2D_;                       //Variable de instanciamiento
    private bool jumpPressed = false;                       //variable usadas para saber si se apretó la barra espaciadora
                                                            //y es personaje saltó.
    private bool playerIsOnGround;                          //Variable privada tipo Bool, el Heroe esta tocando el piso?


    void Start()
    {
        rigidbody2D_ = GetComponent<Rigidbody2D>();         //Instanciando la variable.
        animatorController.Play(AnimationId.Idle);
        jumpPressed = Input.GetButtonDown("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        HandleControls();                                    //invocando el método "HandleControls" (abre el puerto de entrada del teclado)
        HandleMovement();                                    //invocando el método "HandleMovement" (multiplica el valor de "x" por "speed".
        HandleFlip();
        HandleJump();                                         //invocando el método "HandleFlip" (rota el personaje a la izquierda o a la derecha)
        HandleIsGrounding();                                 //Invoca al método "HandleIsGrounding" (El héroe está tocando el piso?). 
    }

    void HandleIsGrounding()
    {
        playerIsOnGround = footA.isTouching || footB.isTouching;  //Falta comentar..........
    }

void HandleControls()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        jumpPressed = Input.GetButtonDown("Jump");

    }

    void HandleMovement()
    {
        rigidbody2D_.velocity = new Vector2(movementDirection.x * speed_, rigidbody2D_.velocity.y);

        if (playerIsOnGround)
        { 
            if (Mathf.Abs(rigidbody2D_.velocity.x) > 0)                         //comprobamos si se esta moviendo en el eje "X"
            {
                animatorController.Play(AnimationId.Run);
            }
            else
            {
                animatorController.Play(AnimationId.Idle);
            }
        }

    }
    void HandleFlip()
    {
        if (rigidbody2D_.velocity.magnitude > 0)                //Sólo si el personaje se está moviendo ejecuta estas lineas...
        {
            if (rigidbody2D_.velocity.x >= 0)                           //si la velocidad en "x" es mayor que cero ejecuta la siguiente linea....
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);            //No rotes
            }
            else                                                                            //de otro modo ejecuta las siguientes lineas.....
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);              //rota en "y" 180º
            }
        }
    }

    void HandleJump()           //Método para agregarle fuerza la RigidBody del Hero
     {

        if(jumpPressed && playerIsOnGround)
            
        {
             this.rigidbody2D_.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
             animatorController.Play(AnimationId.Jump);
        }
    }
}
