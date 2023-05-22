using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Controla los obstáculos que deben aparecer según la dificultad*/
public class Obstacle : MonoBehaviour
{
    [SerializeField] private int dificulty; //Dificultad del mapa
    
    /*
     * @brief Función que se ejecuta al instanciarse el objeto
     * 
     */
    void Start()
    {
        GameplayManager.instance.OnGameplay += EnableComponent; //Suscribe la funcion EnableComponent al evento OnGameplay
        GameplayManager.instance.OnPause += DisableComponent; //Suscribe la funcion DisableComponent al evento OnPause
        GameplayManager.instance.OnGameOver += DisableComponent; //Suscribe la funcion DisableComponent al evento OnGameOver
    }


    public int Dificulty()
    {
        return dificulty;
    }

    private void SetComponentActive(bool enable)
    {
        if (GetComponent<EnemyController>() != null)
        {
            GetComponent<EnemyController>().enabled = enable;
        }

        else if (GetComponentInChildren<SecurityCamera>() != null)
        {
            GetComponentInChildren<SecurityCamera>().enabled = enable;
        }
    }

    private void EnableComponent()
    {
        SetComponentActive(true);
    }

    private void DisableComponent()
    {
        SetComponentActive(false);
    }
}
