using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D RB; //Cuerpo del jugador

    [Header("Animator")]
    private Animator animator; //Controlador de las animaciones del jugador
    private SpriteRenderer SR; //Sprite del jugador
    private AudioSource audioSource; //Controlador de audio
    [SerializeField] private AudioClip punchAudio; //Audio puñetazo
    [SerializeField] private AudioClip walkAudio; //Audio caminar


    [Header("Movement")]
    [SerializeField] Joystick joystick;
    [SerializeField] float moveSpeed; //Velocidad del jugador
    [SerializeField] float jumpForce; //Fuerza de salto
    [SerializeField] Transform groundCheckPoint; //Punto de detección del suelo
    [SerializeField] LayerMask whatIsGround; //Capa del suelo
    [SerializeField] LayerMask whatIsTransporter; //Capa del suelo
    private bool isGrounded; //Indica si el jugador está en el suelo
    private float offset=0; //Offset de velocidad
    private bool transport = false; //Indica si el personaje está sobre una cinta
    [SerializeField] Button jumpButton; //Botón de salto
    private bool isJumping = false; //Indica si el jugador está pulsando el botón de salto
    [SerializeField] private float jumpTime = 0.2f; //Tiempo de espera entre salto
    private float jumpTimer = 0f; //Contador de tiempo de salto

    [Header ("Incapacite")]
    [SerializeField] Button incapacitedButton; //Botón de incapacitar
    private BoxCollider2D incapacitedArea; //Rango para incapacitar
    private GameObject enemy; //Enemigo al que incapacita

    [Header ("Others")]
    public bool visible; //Indica si el jugador es visible para los ene migos
    private bool pause=false; 
    private Vector2 originalVelocity; //Velocidad antes de pausar
    

    void Start()
    {
        //Inicialización de componentes
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        incapacitedArea = GetComponentInChildren<BoxCollider2D>();

        //Se asignan las capas
        whatIsGround = LayerMask.GetMask("Ground");
        whatIsTransporter = LayerMask.GetMask("Transport");
        
        visible = true;

        GameplayManager.instance.OnPause += Pause;
        GameplayManager.instance.OnGameplay += Play;
        GameplayManager.instance.OnGameOver += StopSound;
    }

    void Update()
    {
        if(!pause)
        {
            //Comprobamos si el jugador está sobre una cinta de transporte
            Collider2D collider = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsTransporter);

            //Detecta que sí
            if (collider != null)
            {
                //Se permite el salto
                jumpButton.interactable = true;
                isGrounded = true;
                transport = true;

                //Se añade un offset a la velocidad dependiendo de hacia donde se mueve la cinta
                if (collider.transform.localScale.x > 0)
                {
                    offset = 0.2f;
                }

                else
                {
                    offset = -0.2f;
                }
            }

            else
            {
                transport = false;

                //Comprobamos si el jugador está en el suelo
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

                //Detecta que sí
                if(isGrounded)
                {
                    //Se permite el salto
                    jumpButton.interactable = true;
                }

                else
                {
                    //No se permite el salto
                    jumpButton.interactable = false;
                }
            }

            //Cuando el personaje aterriza se actualiza el tiempo de espera para el siguiente salto
            if(isGrounded)
            {
                UpdateJumpTimer();
            }


            //Obtenemos el input horizontal del controlador
            float horizontalInput = joystick.Horizontal;

            //Movemos al jugador en el eje X
            RB.velocity = new Vector2(moveSpeed * (horizontalInput + offset), RB.velocity.y);

            
            //Cambiamos el sprite para que concuerde la dirección de movimiento con la dirección del sprite
            if (RB.velocity.x < 0 && horizontalInput < 0)
            {
                SR.flipX = true;
                incapacitedArea.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }

            else if (RB.velocity.x > 0)
            {
                SR.flipX = false;
                incapacitedArea.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

            //Se actualiza el Animator
            animator.SetFloat("moveSpeed", Mathf.Abs(RB.velocity.x));
            animator.SetBool("isTransporter", transport);
            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("verticalSpeed", RB.velocity.y);


            //Se reinicia el offset para el siguiente frame
            offset = 0;

            //Se oscurece al personaje si está oculto
            if (!visible)
            {
                SR.color = new Color(0.77f, 0.77f, 0.77f, 1);
            }

            else
            {
                SR.color = new Color(1, 1, 1, 1);
            }

            //Se comprueba que el botón de salto esté pulsado
            if (isJumping)
            {
                //Se comprueba que paso el tiempo de espera
                if(jumpTimer<=0)
                    Jump();
            }
        }

        //Si esta pausado, se mantiene la posición actual (para evitar que caiga por gravedad)
        else
        {
            transform.position = transform.position;
        }
      
      
    }

    //Detecta si el jugador está pulsando el botón de salto
    public void OnPointerDown()
    {
        isJumping = true;
    }

    //Detecta si el jugador suelta el botón de salto
    public void OnPointerUp()
    {
        isJumping = false;

    }

    //El personaje salta
    private void Jump()
    {
        //Solo puede saltar desde el suelo
        if(isGrounded)
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce);
            jumpTimer = jumpTime;
        }
    }

    //Detecta si un objeto colisiona
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es un obstaculo activa el botón de incapacitar
        if (collision.CompareTag("Obstacles"))
        {
            enemy = collision.gameObject;
            incapacitedButton.interactable = true;
        }
    }

    //Detecta cuando un objeto sale de la colisión
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Si es un obstaculo desactiva el botón de incapacitar
        if (collision.CompareTag("Obstacles"))
        {
            enemy = null;
            incapacitedButton.interactable = false;
        }
    }

    //Incapacita a un enemigo cercano
    public void Incapacitate()
    {
        
        if (enemy != null)
        {
            //Animación de puñetazo
            animator.Play("player_punch", 0);

            //Sonido de puñetazo
            audioSource.clip = punchAudio;
            audioSource.pitch = 1f;
            audioSource.loop = false;
            audioSource.Play();

            //Incapacita al enemigo
            enemy.GetComponent<EnemyController>().Incapacite();
           
        }

    }

    //Pausa el personaje
    private void Pause()
    {
        pause = true;
        animator.speed = 0f; //Pausa la animación
        originalVelocity = RB.velocity; //Se almacena la velocidad
        RB.velocity = Vector3.zero;
        RB.isKinematic = true; //Evita bugs
    }

    //Reanuda el control del personaje
    private void Play()
    {
        pause = false;
        RB.isKinematic = false;
        animator.speed = 1f; //Se restaura la animación
        RB.velocity = originalVelocity; //Se restaura la velocidad
    }

    //Para el sonido en ejecución
    private void StopSound()
    {
        audioSource.Stop();
    }

    //Devuelve si el personaje está en el suelo
    public bool Grounded()
    {
        return isGrounded;
    }

    //Actualiza el temporizador del tiempo de espera de salto
    private void UpdateJumpTimer()
    {
        jumpTimer -= Time.deltaTime;
    }
}
