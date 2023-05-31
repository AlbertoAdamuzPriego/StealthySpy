using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] Animator screen;
    [SerializeField] Animator door;
    [SerializeField] AudioClip screenClip;
    [SerializeField] AudioClip doorClip;

    AudioSource audioSource;
    private int phase = 0;
    // Start is called before the first frame update
    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject player = FindFirstObjectByType<Player>().gameObject;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            if (phase==0 && player.GetComponent<Player>().Grounded())
            {
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                player.GetComponent<Player>().enabled = false;
                player.GetComponent<Animator>().SetBool("finish", true);

                screen.SetBool("Finish", true);
                audioSource.clip = screenClip;
                audioSource.Play();
                phase = 1;
            }
           
            
        }
    }


    private void Update()
    {
        if(phase==1 && screen.GetCurrentAnimatorStateInfo(0).IsName("screen_finish"))
        {
            audioSource.clip = doorClip;
            audioSource.Play();
            door.SetBool("Finish", true);
            phase = 2;
  
        }

        else if(phase==2 && door.GetCurrentAnimatorStateInfo(0).IsName("door_finish"))
        {
            Debug.Log("COMPLETE");
            GameplayManager.instance.Completed();
        }
        
    }
}
