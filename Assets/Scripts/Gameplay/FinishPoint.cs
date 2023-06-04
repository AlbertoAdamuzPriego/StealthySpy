using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Representa el final del mapa y controla todo lo necesario para finalizar una partida completada
public class FinishPoint : MonoBehaviour
{
    //Animadores 
    [SerializeField] Animator screen;
    [SerializeField] Animator door;
    
    //Sonidos
    [SerializeField] AudioClip screenClip;
    [SerializeField] AudioClip doorClip;

    AudioSource audioSource;
    private int phase = 0; //Variable que controla el orden de acci�n
    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Si el jugador es detectado
        if(collision.CompareTag("Player"))
        {
            //Se congela al jugador
            GameObject player = FindFirstObjectByType<Player>().gameObject;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX; //Solo se congela el eje X para que el personaje caiga al suelo
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            //Si el jugador no est� saltando
            if (phase==0 && player.GetComponent<Player>().Grounded())
            {
                //Se desactiva el control del jugador
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; // Se congela ahora todos los ejes
                player.GetComponent<Player>().enabled = false;
                player.GetComponent<Animator>().SetBool("finish", true);

                //Animaci�n de la pantalla
                screen.SetBool("Finish", true);
                audioSource.clip = screenClip;
                audioSource.Play();
                phase = 1;
            }
           
            
        }
    }


    private void Update()
    {
        //Cuando se ha realizado la animaci�n de la pantalla
        if(phase==1 && screen.GetCurrentAnimatorStateInfo(0).IsName("screen_finish"))
        {
            //Animaci�n de la puerta
            audioSource.clip = doorClip;
            audioSource.Play();
            door.SetBool("Finish", true);
            phase = 2;
  
        }

        //Cuando se ha realizado la animaci�n de la puerta
        else if(phase==2 && door.GetCurrentAnimatorStateInfo(0).IsName("door_finish"))
        {
            //Se finaliza la partida
            GameplayManager.instance.Completed();
        }
        
    }
}
