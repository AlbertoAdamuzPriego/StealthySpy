using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//Controlador de los enemigos
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speedMovement; //Velocidad de movimiento

    [SerializeField] private Vector2[] pointsMovement; //Vector de posiciones de patrulla

    [SerializeField] private float waitTime; //Tiempo de espera en cada posici�n de patrulla

    [SerializeField] private float startVisionAngle; //�ngulo inicial del �rea de visi�n

    private float waitTimer; //Temporizador de espera
    
    private float incapacitedTimer; //Temporizador de tiempo incapacitado

    private int currentTarget=0; //Posici�n objetivo

    private SpriteRenderer sprite;
    
    private Animator animator;
    
    private bool incapacitated = false;

    [SerializeField] private float incapacitedTime; //Tiempo que permanece incapacitado

    [SerializeField] VisionTriangleController fov; //�rea de visi�n

    private bool playerIsDetected;

    void Start()
    {

        sprite = GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator>();
        Turn();

        fov=GetComponentInChildren<VisionTriangleController>();
        waitTimer = 0;
        incapacitedTimer = 0;
    }

   
    void Update()
    {
        if (!incapacitated)
        {
            if (!playerIsDetected)
            {
                if (MoveToTarget()) 
                {
                    //Se desplaza hacia la posici�n correspondiente
                    transform.position = Vector2.MoveTowards(transform.position, pointsMovement[currentTarget], speedMovement * Time.deltaTime);
                    animator.SetBool("isWalking", true);
                }

                else
                {
                    //Espera un tiempo en la posici�n actual
                    UpdateWaitTimer();
                    animator.SetBool("isWalking", false);

                    //Cuando finaliza el tiempo de espera se avanza a la siguiente posici�n
                    if (waitTimer >= waitTime)
                    {
                        currentTarget = (currentTarget + 1) % pointsMovement.Length;
                        Turn();
                        waitTimer = 0;
                    }
                }

            }

            //Dibuja el �rea de visi�n
            fov.DrawMesh();
        }

        else
        {
            //Actualiza el temporizador
            UpdateIncapacitedTimer();

            //Se despierta al alcanzar el tiempo m�ximo incapacitado
            if(incapacitedTimer >= incapacitedTime)
            {
                WakeUp();
            }
        }

    }

    //Comprueba si tiene que desplazarse
    private bool MoveToTarget()
    {
        //Si no ha alcanzado la posici�n objetivo y no est� incapacitado, tiene que continuar moviendose
        if(Vector2.Distance(pointsMovement[currentTarget], transform.position)>0.1 && !incapacitated)
        {
            return true;
        }

        return false;
    }

    //Ajusta el sentido del sprite y el �rea de visi�n
    private void Turn()
    {
        //El personaje se mueve hacia la derecha
        if (transform.position.x < pointsMovement[currentTarget].x)
        {
            //Se actualiza el �ngulo inicial
            fov.setInitialAngle(startVisionAngle);

            //Se ajusta el punto de origen del fov
            transform.GetChild(0).GetComponent<Transform>().position = transform.position + new Vector3(0.3f, -0.28f,0);
            
            //Se ajusta el sprite
            sprite.flipX = false;
        }

        //El personaje se mueve hacia la izquierda
        else
        {
            fov.setInitialAngle(startVisionAngle+180);
            transform.GetChild(0).GetComponent<Transform>().position = transform.position + new Vector3(-0.3f, -0.28f, 0);
            sprite.flipX = true;
        }
    }
    //Actualiza el temporizador del tiempo de espera
    private void UpdateWaitTimer()
    {
        waitTimer += Time.deltaTime;
    }

    //Actualiza el temporizador del tiempo incapacitado
    private void UpdateIncapacitedTimer()
    {
        incapacitedTimer += Time.deltaTime;
    }

    //Incapacita al personaje
    public void Incapacite()
    {
        
        //Evita que el jugador pueda incapacitar infinitamente 
        if (!incapacitated)
        {
            animator.SetBool("Incapacited", true);
            incapacitated = true;
            incapacitedTimer = 0;
            fov.gameObject.SetActive(false);
     
        }
    }

    //El personaje despierta despu�s de haber sido incapacitado
    private void WakeUp()
    {
        animator.SetBool("Incapacited", false);

        //Espera a que finalice la animaci�n de levantarse
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("enemy_idle"))
        {
            incapacitated = false;
            fov.gameObject.SetActive(true);
        }
      
    }
}
