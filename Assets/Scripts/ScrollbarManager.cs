using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int currentStep;
    int steps;
    Scrollbar scrollBar;
    void Start()
    {
      
        scrollBar = GetComponent<Scrollbar>();

        steps =FindObjectOfType<MapManager>().GetNumberOfMaps();

        scrollBar.numberOfSteps = 1;
        scrollBar.onValueChanged.AddListener((value) =>
        {
            /*
               1 steps -> 0
               2 steps -> 0 1
               3 steps -> 0  0.5  1
               4 steps -> 0 0.33  0.66 1
               5 steps-> 0 0.25 0.5 0.75 1
             */
            value=Mathf.Clamp(value, 0, 1);
            currentStep = Mathf.RoundToInt(value / (1f / ((float)steps - 1)));
           
        });
        
    }

    public void IncrementStep()
    {
        if(currentStep < steps) 
            currentStep++;

        scrollBar.value = currentStep;
    }

    public void DecrementStep()
    {
        if (currentStep > 0)
               currentStep--;

        scrollBar.value = currentStep;
    }

    private void Update()
    {
        Debug.Log(scrollBar.value);
    }

}
