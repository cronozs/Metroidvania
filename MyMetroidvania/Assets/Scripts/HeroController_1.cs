using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class HeroController_1 : MonoBehaviour
{
    [Header("Animation Variable")]
    [SerializeField] AnimatorController_1 animatorController;
    [Header("Checker Variables")]                                //Cabecera del ComboBox "Variables"  //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.
    [SerializeField] LayerChecker_1 footA;                  //Instanciamento a la Clase "LayerChecker_1" = footA
    [SerializeField] LayerChecker_1 footB;                  //Instanciamento a la Clase "LayerChecker_1" = footB
    [Header("Boolean Variables")]                           //"Pesta a" con t tulo ??en el Inspector
    public bool canDoubleJump;                              //variable boleana(verdadero/falso) para ejecutar el salto doble
    public bool playerIsAttacking;                          //Esta atacando el héroe??
    [Header("Interruption Variables")]                      //"Pesta a" con t tulo ?? en el Inspector
    public bool canCheckGround;                             //Variable booleana, usada para detectar si tocas el piso
    public bool canMove;                                    //Usamos la variable para anular el movimiento "Horizontal" "Run" y "Idle"
    [Header("Rigid Variables")]
    [SerializeField] private float doubleJumpForce;         //Agregamos una variable flotante para agrear furza al DobleSalto
    [SerializeField] private float jumpForce;               //Agregamos una variable flotante para agrear furza al salto
    [SerializeField] private float speed_;                  //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.
    [Header("Attack Variables")]
    [SerializeField] SwordController_1 swordController;

    [SerializeField] private Vector2 movementDirection;     //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.
    private Rigidbody2D rigidbody2D_;                       //Variable de instanciamiento
    private bool jumpPressed = false;                       //variable usadas para saber si se apret  la barra espaciadora?
                                                            //y es personaje salt .?
    private bool attackPressed = false;                     //activamos Input del LeftClick del Mouse
    private bool playerIsOnGround;                          //Variable privada tipoBool, el Heroe esta tocando el piso?
    void Start()
    {
        canCheckGround = true;                              //inicializamos la variable "canCheckGround" como verdadera
        rigidbody2D_ = GetComponent<Rigidbody2D>();         //Instanciando la variable.
        animatorController.Play(AnimationId.Idle);
        //jumpPressed = Input.GetButtonDown("Jump");
        canMove = true; //Al iniciar el juego el personaje se mueve "Run" y "Idle"
    }
    // Update is called once per frame
    void Update()
    {
        HandleIsGrounding();                                 //Invoca al m todo ?"HandleIsGrounding"(El h roe est  tocando el piso ?). ??
        HandleControls();                                    //invocando el m todo ?"HandleControls"(abre el puerto de entrada del teclado)
        HandleMovement();                                    //invocando el m todo ?"HandleMovement"(multiplica el valor de "x" por "speed".
        HandleFlip();
        HandleJump();                                         //invocando el m todo?"HandleFlip"(rota el personaje a la izquierda o a la derecha)
        HandleAttack();                                       //invocando el método "HandleAttack" (agregamos clip de animación Attack)

    }
    void HandleIsGrounding()
    {
        if (!canCheckGround) return;   //Si NO esta tocando el piso NO se ejecuta nada de este m todo?
                                       //(esta variable se apaga en la corrutina)
        //Debug.Log(!canCheckGround);
        playerIsOnGround = footA.isTouching || footB.isTouching;  //Falta comentar..........Debug.Log("Tocando el pioo");
    }
    void HandleControls()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"),
        Input.GetAxis("Vertical"));
        jumpPressed = Input.GetButtonDown("Jump");
        attackPressed = Input.GetButtonDown("Attack"); //linkeamos el RMB a variable "attackPressed"
    }
    void HandleMovement()
    {
        if (!canMove) return; //Si está volando no hagas nada.....
        rigidbody2D_.velocity = new Vector2(movementDirection.x * speed_, rigidbody2D_.velocity.y);
        if (playerIsOnGround)
        {

            if (Mathf.Abs(rigidbody2D_.velocity.x) > 0)
            //comprobamos si se esta moviendo en el eje "X"
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
        if (rigidbody2D_.velocity.magnitude > 0)                //S lo si el ?personaje se est moviendo ejecuta estas lineas...?
        {
            if (rigidbody2D_.velocity.x >= 0)                           //si la velocidad en "x" es mayor que cero ejecuta la siguiente linea....
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                //No rotes
            }
            else
            //de otro modo ejecuta las siguientes lineas.....
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
                //rota en "y" 180?
            }
        }
    }
    void HandleJump()           //M todo para agregarle fuerza la RigidBody del ?Hero
    {
        if (canDoubleJump && jumpPressed && !playerIsOnGround)  //"!playerIsOnGround" esta variable nos indica que NO esta tocando el piso
        {
            this.rigidbody2D_.velocity = Vector2.zero;
            this.rigidbody2D_.AddForce(Vector2.up * doubleJumpForce,ForceMode2D.Impulse);//agrega impulso de fuerza instant nea hacia arriba al doble ?salto
            canDoubleJump = false;
            //apagamos la variable "canDoubleJump  para que no brinque infinitamente?
        }
        if (jumpPressed && playerIsOnGround)

        {
            this.rigidbody2D_.AddForce(Vector2.up * jumpForce,
            ForceMode2D.Impulse);
            animatorController.Play(AnimationId.Jump);
            StartCoroutine(HandleJumpAnimation());
            canDoubleJump = true;
            //prendemos la variable "canDoubleJump" para que brinque

            //de nuevo si apretamos la barra espaciadora 
        }
    }
    IEnumerator HandleJumpAnimation()                       //Corrutina que ejecutados "clips" desfasados en tiempo 0.4f unidades de tiempo
    {
        canCheckGround = false;
        playerIsOnGround = false;
        yield return new WaitForSeconds(0.1f);                  //Ejecutar el Clip "Brinco" durante 0.1f uniades de tiempo
        animatorController.Play(AnimationId.PrepararBrinco);   //Ejecuta el clip "PrepararBrinco"
        yield return new WaitForSeconds(0.2f);                  //Ejecutar el Clip "PrepararBrinco" durante 0.2f uniades de tiempo
        animatorController.Play(AnimationId.Brincar);           //Ejecuta el clip "Brinco"
        //yield return new WaitForSeconds(1);                     //Ejecutar el Clip "Brinco" durante 1f uniades de tiempo
        canCheckGround = true;
    }

    void HandleAttack()                         //Método de animación Attack (puede atacar en el piso y en el aire)
    {
        if (attackPressed && !playerIsAttacking)          //Si apretamos RMB y NO está  atacando..
        {
            animatorController.Play(AnimationId.Attack);  //ejecutamos Clip "Atack"
            playerIsAttacking = true;                     //Prendemos la variable como verdadera (el héroe está atacando)
            swordController.Attack(0.4f);
            StartCoroutine(RestoreAttack());              //Inicia corrutina "RestoreAttack" (reinicia
        }
    }

    IEnumerator RestoreAttack()                         //Corrutina "RestoreAttack"
    {
        if (playerIsOnGround)                          //Si el héroe esta en el suelo?
        
            canMove = false;                            //apaga la variable "canMove"
            yield return new WaitForSeconds(0.4f);          //espera 0.4f 
            playerIsAttacking = false;
        
                              //Apaga variable "heroe esta tacando"
        if (!playerIsOnGround)                          //Si el héroe NO está en el piso.....
        
            animatorController.Play(AnimationId.PrepararBrinco);  //Inicia clip "preparaBrinco"
            canMove = true;                                    //prende la variable "canMove"
        
            
    }
}