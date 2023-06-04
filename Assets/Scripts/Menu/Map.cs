using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu] //Pemrite crear objetos en el editor

//Contiene toda la información de un mapa
public class Map : ScriptableObject
{
    public string mapName="";
    public string sceneName; //Escena de Unity con el mapa
    public float[] score0 = new float[10]; //Puntuaciones dificultad fácil
    public float[] score1 = new float[10]; //Puntuaciones dificultad normal
    public float[] score2 = new float[10]; //Puntuaciones dificultad dificil
    public float averageScore0; //Puntuación media dificultad fácil
    public float averageScore1; //Puntuación media dificultad normal
    public float averageScore2; //Puntuación media dificultad dificil

    //Devuelve la lista de puntuaciones de una dificultad
    public float[] GetScoreList(int difficulty)
    {
        switch(difficulty)
        {
            case 0: return score0;
            case 1: return score1;
            case 2: return score2;  
        }

        return score0;
    }
  
    //Asigna una lista de puntuaciones a una dificultad

    public void SetScoreList(float[] newScore, int difficulty)
    {
        Assert.AreEqual(10, newScore.Length);
       
        switch(difficulty)
        {
            case 0:
                score0 = newScore;
                break;

            case 1:
                score1 = newScore;
                break;

            case 2:
                score2 = newScore;
                break;
        }

        CalculateAverages();
       
    }

    //Devuelve la media de las puntuaciones de una dificultad
    public float GetAverageScore(int difficulty)
    {
        switch(difficulty)
        {
            case 0: return averageScore0;
            case 1: return averageScore1;
            case 2: return averageScore2;
        }
        return averageScore0;
    }

    //Guarda las puntuaciones diferenciandolas con el nombre del mapa
    public void SaveScore()
    {
        for(int i=0;i<score0.Length; i++)
        {
 
            PlayerPrefs.SetFloat("Score0" + mapName + i, score0[i]);
            PlayerPrefs.SetFloat("Score1" + mapName + i, score1[i]);
            PlayerPrefs.SetFloat("Score2" + mapName + i, score2[i]);
        }

        PlayerPrefs.DeleteKey("Average0" + mapName);
        PlayerPrefs.DeleteKey("Average1" + mapName);
        PlayerPrefs.DeleteKey("Average2" + mapName);

        PlayerPrefs.Save();

    }

    //Carga las puntuaciones
    public void LoadScore()
    {

        for (int i = 0; i < score0.Length; i++)
        {
            score0[i]=PlayerPrefs.GetFloat("Score0" + mapName + i, 0);
            score1[i] = PlayerPrefs.GetFloat("Score1" + mapName + i, 0);
            score2[i] = PlayerPrefs.GetFloat("Score2" + mapName + i, 0);
        }

        CalculateAverages();

    }

    //Devuelve el mejor tiempo de todas las dificultades
    public float[] GetBestTime()
    {
        float[] times = new float[3];
        times[0] = score0[0];
        times[1] = score1[0];
        times[2]= score2[0];


        return times;
    }

    //Devuelve la media las puntuaciones de todas las dificultades
    public float[] GetAverageTime()
    {
        float[] times = new float[3];
        times[0] = averageScore0;
        times[1] = averageScore1;
        times[2] = averageScore2;


        return times;
    }

    //Calcula la media obtenida de todas las dificultades
    private void CalculateAverages()
    {
        //Fácil
        int i=0;
       
        averageScore0 = 0;
        foreach (float num in score0)
        {
            if (num != 0)
            {
                i++;
                averageScore0 += num;
            }

        }

        if(i>0)
            averageScore0 /= i;
        else 
            averageScore0 = 0;

       //Normal
       i = 0;
       
       averageScore1 = 0;
       foreach (float num in score1)
       {
        if (num != 0)
        {
            i++;
            averageScore1 += num;
        }

       }

       if(i>0)
        averageScore1 /= i;
               
       else
        averageScore1 = 0;

       //Dificil
        i = 0;
        averageScore2 = 0;
        foreach (float num in score2)
        {
           if (num != 0)
            {
                 i++;
                 averageScore2 += num;
            }

        }

        if(i>0)
            averageScore2 /= i;
        else
            averageScore2 = 0;
        
    }
}
