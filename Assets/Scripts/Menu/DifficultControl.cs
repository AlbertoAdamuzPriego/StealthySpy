using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Map;
using TMPro;
using UnityEngine.UI;
using System;

//Controla la dificultad del juego
public class DifficultControl : MonoBehaviour
{
    [SerializeField] private TMP_Text label; //Texto de dificultad actual
    private void Start()
    {
        //Lee la �ltima dificultad guardada
        int difficulty=PlayerPrefs.GetInt("difficulty",0); 

        switch (difficulty)
        {
            case 0: label.SetText("F�cil"); break;

            case 1: label.SetText("Normal"); break;

            case 2: label.SetText("Dificil"); break;
        }


      
    }

    //Cambia la dificultad actual seg�n la seleccionada en el men� de selector de mapas
    public void ChangeDifficulty()
    {
        string difficulty = GetComponentInChildren<TMP_Text>().text;

        switch (difficulty)
        {
            case "F�cil":
                PlayerPrefs.SetInt("difficulty", 0);
                break;
            case "Normal":
                PlayerPrefs.SetInt("difficulty",1);
                break;
            case "Dificil":
                PlayerPrefs.SetInt("difficulty",2);
                break;
        }

        GameManager.instance.Difficulty();

    }
}
