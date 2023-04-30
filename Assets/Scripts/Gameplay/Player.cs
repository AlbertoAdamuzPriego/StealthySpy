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

    [Header("Movement")]
    [SerializeField] Joystick joystick;
    [SerializeField] float moveSpeed; //Velocidad del jugador
    [SerializeField] float jumpForce; //Fuerza de salto
    [SerializeField] Transform groundCheckPoint; //Punto de detección del suelo
    [SerializeField] LayerMask whatIsGround; //Capa del suelo
    private bool isGrounded; //Indica si el jugador está en el suelo


    [SerializeField] GameObject incapacitedButton;
    private BoxCollider2D incapacitedArea;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        whatIsGround = LayerMask.GetMask("Ground");
        incapacitedArea = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Obtenemos el input horizontal del controlador
        float horizontalInput = joystick.Horizontal;

        //Movemos al jugador en el eje X
        RB.velocity = new Vector2(moveSpeed * horizontalInput, RB.velocity.y);

        //Cambiamos el sprite para que concuerde la dirección de movimiento con la dirección del sprite
        if (RB.velocity.x < 0)
        {
            SR.flipX = true;
        }

        else if (RB.velocity.x > 0)
        {
            SR.flipX = false;
        }


        //Comprobamos si el jugador está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

        /*//Comprobamos si el jugador puede saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Realizamos el salto
            RB.velocity = new Vector2(RB.velocity.x, jumpForce);
        }
        */
        animator.SetFloat("moveSpeed", Mathf.Abs(RB.velocity.x));

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
        if(enemy != null)
        {
            enemy.GetComponent<EnemyController>().Incapacite();
        }
    }

}
