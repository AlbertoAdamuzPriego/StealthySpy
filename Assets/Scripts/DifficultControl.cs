using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Map;
using TMPro;
using UnityEngine.UI;

public class DifficultControl : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    private void Start()
    {
        int difficulty=PlayerPrefs.GetInt("difficulty");

        switch (difficulty)
        {
            case 0: label.SetText("F�cil"); break;

            case 1: label.SetText("Normal"); break;

            case 2: label.SetText("Dificil"); break;
        }


      
    }
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

    }
}
