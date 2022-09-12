//Script para controlar el orden de las animaciones
//Usando el M�todo "Play", Se env�a en la variable "AnimationId" el clip de animaci�n
//"Run" - "Jump" o "Idle"
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationId                 //Definimos un enumerado
{
    Idle = 0,
    Run = 1,
    Jump = 2,
    PrepararBrinco = 3,
    Brincar = 4
 }

public class AnimatorController_1 : MonoBehaviour
{
    Animator animator;                              //Variable tipo Animator
    private void Awake ()
    {
        animator = GetComponent<Animator>();        //Instanciamiento  variable "animator"
    }

    public void Play(AnimationId animationId)       //M�todo para ejecutar la animaci�n solicitada enviada dentro de
                                                    //"AnimationId" ("Idle" o "Run")
    {
        animator.Play(animationId.ToString());      //Ejecuta la animacion gurdada en "animationID"
        
        
    }
}
