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

    [SerializeField] GameObject incapacitedButton;
    private BoxCollider2D incapacitedArea;
    private GameObject enemy;
    public bool visible;
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
       

        if (Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsTransporter))
        {
            isGrounded = true;
            offset = 0.2f;
            transport = true;
        }

        else
        {
            transport = false;
            //Comprobamos si el jugador está en el suelo
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
        }


        //Obtenemos el input horizontal del controlador
        float horizontalInput = joystick.Horizontal;

        //Movemos al jugador en el eje X
        RB.velocity = new Vector2(moveSpeed * (horizontalInput+offset), RB.velocity.y);

      /*  if(horizontalInput+offset !=0)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.clip = walkAudio;
                audioSource.pitch = 2f;
                audioSource.loop = true;
                audioSource.Play();
               
            }
        }

        else
        {
            if(audioSource.clip != null && audioSource.clip.Equals(walkAudio)) 
                audioSource.Stop();
        }
      */
        //Cambiamos el sprite para que concuerde la dirección de movimiento con la dirección del sprite
        if (RB.velocity.x < 0 && horizontalInput<0)
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
        animator.SetBool("isGrounded",isGrounded);
        animator.SetFloat("verticalSpeed", RB.velocity.y);
        offset = 0;

        if(!visible)
        {
            SR.color = new Color(0.77f, 0.77f, 0.77f, 1);
        }

        else
        {
            SR.color = new Color(1, 1, 1, 1);
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
            incapacitedButton.GetComponent<Image>().color = Color.white;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacles"))
        {
            enemy = null;
            incapacitedButton.GetComponent<Image>().color = Color.gray;
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
        RB.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    private void Play()
    {
        RB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void StopSound()
    {
        audioSource.Stop();
    }
}
