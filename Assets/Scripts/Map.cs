using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu]
public class Map : ScriptableObject
{
    public string mapName="";
    public string sceneName;
    public float[] score0 = new float[10];
    public float[] score1 = new float[10];
    public float[] score2 = new float[10];
    public float averageScore0;
    public float averageScore1;
    public float averageScore2;

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
  

    public void SetScoreList(float[] newScore, int difficulty)
    {
        Assert.AreEqual(10, newScore.Length);
        float i=0;
        switch(difficulty)
        {
            case 0:
                score0 = newScore;
                averageScore0 = 0;
                Debug.Log("Average");
                foreach (float num in score0)
                {
                    if(num!=0)
                    {
                        i++;
                        averageScore0 += num;
                    }
                   
                }

                averageScore0 /= i;
                break;

            case 1:
                score1 = newScore;
                averageScore1 = 0;
                foreach (float num in score1)
                {
                    if(num!=0)
                    {
                        i++;
                        averageScore1 += num;
                    }
                    
                }

                averageScore1 /= i;
                break;

            case 2:
                score2 = newScore;
                averageScore2 = 0;
                foreach (float num in score2)
                {
                    if(num!=0)
                    {
                        i++;
                        averageScore2 += num;
                    }
                    
                }

                averageScore2 /= i;
                break;
        }
       
    }


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

    public void SaveScore()
    {
        for(int i=0;i<score0.Length; i++)
        {
            Debug.Log(score0[i]);
            PlayerPrefs.SetFloat("Score0" + mapName + i, score0[i]);
            PlayerPrefs.SetFloat("Score1" + mapName + i, score1[i]);
            PlayerPrefs.SetFloat("Score2" + mapName + i, score2[i]);
        }

        PlayerPrefs.SetFloat("Average0" + mapName, averageScore0);
        PlayerPrefs.SetFloat("Average1" + mapName, averageScore1);
        PlayerPrefs.SetFloat("Average2" + mapName, averageScore2);

        PlayerPrefs.Save();

    }

    public void LoadScore()
    {

        for (int i = 0; i < score0.Length; i++)
        {
            score0[i]=PlayerPrefs.GetFloat("Score0" + mapName + i, 0);
            score1[i] = PlayerPrefs.GetFloat("Score1" + mapName + i, 0);
            score2[i] = PlayerPrefs.GetFloat("Score2" + mapName + i, 0);
        }

        PlayerPrefs.GetFloat("Average0" + mapName, 0);
        PlayerPrefs.GetFloat("Average1" + mapName, 0);
        PlayerPrefs.GetFloat("Average2" + mapName, 0);
    }

    public float[] GetBestTime()
    {
        float[] times = new float[3];
        times[0] = score0[0];
        times[1] = score1[0];
        times[2]= score2[0];


        return times;
    }

    public float[] GetAverageTime()
    {
        float[] times = new float[3];
        times[0] = averageScore0;
        times[1] = averageScore1;
        times[2] = averageScore2;


        return times;
    }
}
