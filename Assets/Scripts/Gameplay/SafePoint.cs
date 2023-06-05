using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Representa los objetos que sirven al jugador para esconderse
public class SafePoint : MonoBehaviour
{
    private UIGameplayManager UIgameManager; //Controlador de la interfaz
    
    void Start()
    {
        UIgameManager=FindAnyObjectByType<UIGameplayManager>();
    }

    // Evento que se activa al detectar una colisión
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si la colisión es el jugador, se da el efecto de estar oculto y el personaje no es visible a los enemigos
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().visible = false;
            UIgameManager.ActivatePanelSafePoint(true);
        }
    }

    // Evento que se activa al finalizar una colisión
    private void OnTriggerExit2D(Collider2D other)
    {
        //Si la colisión es el jugador, se elimina el efecto de estar oculto y el personaje es visible a los enemigos
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().visible = true;
            UIgameManager.ActivatePanelSafePoint(false);
        }
    }
}
