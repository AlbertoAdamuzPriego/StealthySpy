using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

//Representa los botones del menú de puntuaciones
public class ScoreButton : MonoBehaviour
{
    private Map map; //Mapa asociado al botón
    private GameObject scoresContainer;  //Contenedor del texto de puntuaciones
    public Map Map
    {
        set { map = value; }
    }

    private void Start()
    {
        transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = map.mapName; 
        scoresContainer = GameObject.FindGameObjectWithTag("Scores");

        var button = GetComponentInChildren<Button>();
        button.onClick.AddListener(ShowScores);
    }

    //Muestra todas las puntuaciones registradas en el mapa
    private void ShowScores()
    {
        //Strings para construir el panel de información
        string text0="";
        string text1="";
        string text2="";

        //Leemos la lista de puntuaciones
        float[] score0 = map.GetScoreList(0);
        float[] score1=map.GetScoreList(1);
        float[] score2=map.GetScoreList(2);

        //Añadimos iteradamente las puntuaciones
        for(int i=0;i<score0.Length; i++)  
        {
            if (score0[i] != 0)
                text0 += TimeSpan.FromSeconds(score0[i]).ToString(@"mm\:ss") + "\n";

            else
                text0 += "--:--\n";

            if (score1[i]!=0)
                text1 += TimeSpan.FromSeconds(score1[i]).ToString(@"mm\:ss")+"\n";
            
            else
                text1 += "--:--\n";

            if (score2[i]!=0)
                text2 += TimeSpan.FromSeconds(score2[i]).ToString(@"mm\:ss")+ "\n";
            else
                text2 += "--:--\n";
        }

        //Si la media no es 0 la añadimos
        if(map.GetAverageScore(0)!=0)
            text0 += TimeSpan.FromSeconds(map.GetAverageScore(0)).ToString(@"mm\:ss");
        
        else
            text0 += "--:--\n";

        if (map.GetAverageScore(1) != 0)
            text1 += TimeSpan.FromSeconds(map.GetAverageScore(1)).ToString(@"mm\:ss");

        else
            text1 += "--:--\n";

        if (map.GetAverageScore(2) != 0)
            text2 += TimeSpan.FromSeconds(map.GetAverageScore(2)).ToString(@"mm\:ss");

        else
            text2 += "--:--\n";

        scoresContainer.transform.GetChild(0).GetComponent<TMP_Text>().text=text0;
        scoresContainer.transform.GetChild(1).GetComponent<TMP_Text>().text = text1;
        scoresContainer.transform.GetChild(2).GetComponent<TMP_Text>().text = text2;
    }
}
