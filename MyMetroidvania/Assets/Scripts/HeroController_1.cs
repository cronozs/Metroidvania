using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController_1 : MonoBehaviour
{
    [SerializeField] private float speed_;                  //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.
    [SerializeField] private Vector2 movementDirection;     //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.
    private Rigidbody2D rigidbody2D_;                       //Variable de instanciamiento
    [SerializeField] private float jumpForce;
    private bool jumpPressed = false;


    void Start()
    {
        rigidbody2D_ = GetComponent<Rigidbody2D>();         //Instanciando la variable.
        jumpPressed = Input.GetButtonDown("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        HandleControls();                                    //invocando el m�todo "HandleControls" (abre el puerto de entrada del teclado)
        HandleMovement();                                    //invocando el m�todo "HandleMovement" (multiplica el valor de "x" por "speed".
        HandleFlip();
        HandleJump();                                         //invocando el m�todo "HandleFlip" (rota el personaje a la izquierda o a la derecha)
    }

    void HandleControls()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        jumpPressed = Input.GetButtonDown("Jump");

    }

    void HandleMovement()
    {
        rigidbody2D_.velocity = new Vector2(movementDirection.x * speed_, rigidbody2D_.velocity.y);
    }
    void HandleFlip()
    {
        if (rigidbody2D_.velocity.magnitude > 0)                //S�lo si el personaje se est� moviendo ejecuta estas lineas...
        {
            if (rigidbody2D_.velocity.x >= 0)                           //si la velocidad en "x" es mayor que cero ejecuta la siguiente linea....
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);            //No rotes
            }
            else                                                                            //de otro modo ejecuta las siguientes lineas.....
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);              //rota en "y" 180�
            }
        }
    }

    void HandleJump()           //M�todo para agregarle fuerza la RigidBody del Hero
     {
         
         if (jumpPressed)        //si la barra espaciadora es apretada.....
            
         {
             this.rigidbody2D_.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);//agrega impulso de fuerza instantanea hacia arriba           
         }
     }
}
