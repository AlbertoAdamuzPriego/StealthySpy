using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timeout:MonoBehaviour
{
    [SerializeField] private float time=0;
    private bool enable = false;
    [SerializeField] private TMP_Text text;
    private string timeText;
    private float maxTime;
    private void Start()
    {
        GameplayManager.instance.OnGameplay += ReanudeCount;
        GameplayManager.instance.OnPause += PauseCount;
        GameplayManager.instance.OnFinished += PauseCount;
    }
    public void StartCount(float MaxTime)
    {
        time = MaxTime;
        maxTime = MaxTime;
        enable = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(enable) 
        {
            time -= Time.deltaTime;

          
            if(time <= 0 )
            {
                enable = false;
                GameplayManager.instance.GameOver();
            }
        }

        timeText = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");

        text.text = timeText;


    }


    public void PauseCount() 
    { 
        enable = false;
    }

    public void ReanudeCount()
    {
        enable = true;
    }

    public float GetTime()
    {
        return maxTime-time;
    }

}
