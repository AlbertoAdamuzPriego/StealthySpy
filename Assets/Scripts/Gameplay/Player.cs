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
    private AudioSource audioSource;
    [SerializeField] private AudioClip punchAudio;
    [SerializeField] private AudioClip walkAudio;


    [Header("Movement")]
    [SerializeField] Joystick joystick;
    [SerializeField] float moveSpeed; //Velocidad del jugador
    [SerializeField] float jumpForce; //Fuerza de salto
    [SerializeField] Transform groundCheckPoint; //Punto de detección del suelo
    [SerializeField] LayerMask whatIsGround; //Capa del suelo
    [SerializeField] LayerMask whatIsTransporter; //Capa del suelo
    private bool isGrounded; //Indica si el jugador está en el suelo
    private float offset=0;
    private bool transport = false;


    [SerializeField] Button jumpButton;
    [SerializeField] Button incapacitedButton;
    private BoxCollider2D incapacitedArea;
    private GameObject enemy;
    public bool visible;
    private bool pause=false;
    private Vector2 originalVelocity;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        whatIsGround = LayerMask.GetMask("Ground");
        whatIsTransporter = LayerMask.GetMask("Transport");
        incapacitedArea = GetComponentInChildren<BoxCollider2D>();
        visible = true;

        GameplayManager.instance.OnPause += Pause;
        GameplayManager.instance.OnGameplay += Play;
        GameplayManager.instance.OnGameOver += StopSound;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pause)
        {
            Collider2D collider = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsTransporter);

            if (collider != null)
            {
                jumpButton.interactable = true;
                isGrounded = true;
                transport = true;

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

                if(isGrounded)
                {
                    jumpButton.interactable = true;
                }

                else
                {
                    jumpButton.interactable = false;
                }
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


            animator.SetFloat("moveSpeed", Mathf.Abs(RB.velocity.x));
            animator.SetBool("isTransporter", transport);
            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("verticalSpeed", RB.velocity.y);
            offset = 0;

            if (!visible)
            {
                SR.color = new Color(0.77f, 0.77f, 0.77f, 1);
            }

            else
            {
                SR.color = new Color(1, 1, 1, 1);
            }
        }

        else
        {
            transform.position = transform.position;
        }
        
      
    }

    public void Jump()
    {
        if(isGrounded)
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.CompareTag("Obstacles"))
        {
            enemy = collision.gameObject;
            incapacitedButton.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacles"))
        {
            enemy = null;
            incapacitedButton.interactable = false;
        }
    }

    public void Incapacitate()
    {
        animator.Play("player_punch", 0);
        if (enemy != null)
        {
            
            audioSource.clip = punchAudio;
            audioSource.pitch = 1f;
            audioSource.loop = false;
            audioSource.Play();
            enemy.GetComponent<EnemyController>().Incapacite();
           
        }

    }

    private void Pause()
    {
        pause = true;
        animator.speed = 0f;
        originalVelocity = RB.velocity;
        RB.velocity = Vector3.zero;
        RB.isKinematic = true;
    }

    private void Play()
    {
        pause = false;
        RB.isKinematic = false;
        animator.speed = 1f;
        RB.velocity = originalVelocity;
    }

    private void StopSound()
    {
        audioSource.Stop();
    }

    public bool Grounded()
    {
        return isGrounded;
    }
}
