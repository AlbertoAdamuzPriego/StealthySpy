using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int currentStep = 0;
    int steps;
    Scrollbar scrollBar;
    void Start()
    {
        scrollBar = GetComponent<Scrollbar>();
        steps=FindObjectOfType<MapManager>().GetNumberOfMaps();

        scrollBar.numberOfSteps = steps-1;
        scrollBar.onValueChanged.AddListener((value) =>
        {
            currentStep = Mathf.RoundToInt(value / (1f / (float)scrollBar.numberOfSteps));
           
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



}
