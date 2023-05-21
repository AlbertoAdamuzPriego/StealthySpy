using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    private int currentStep;
    private int maxSteps;
    [SerializeField] RectTransform content;
    Scrollbar scroll;
    [SerializeField] int offset;
    // Start is called before the first frame update
    void Start()
    {
        currentStep = 1;
        maxSteps = FindAnyObjectByType<MapManager>().GetNumberOfMaps();
        scroll = GetComponentInChildren<Scrollbar>();
        
        scroll.onValueChanged.AddListener((value) =>
        {
            float aux = 0; 
            int i = 1;

            while(aux>content.anchoredPosition.x)
            {
                
                aux -= offset/2;
                i++;
            }

            currentStep = Mathf.Clamp(i-2,1,maxSteps);
        });
    }
    // Update is called once per frame
    void Update()
    {
    
    }

    public void Next()
    {
        content.anchoredPosition = (currentStep-1) * new Vector2(-offset, 0);
        Debug.Log(maxSteps);
        if(currentStep < maxSteps) 
        {
            currentStep++;
            content.anchoredPosition += new Vector2(-offset, 0);
        }
       
    }

    public void Previous()
    {
        content.anchoredPosition = (currentStep - 1) * new Vector2(-offset, 0);
        if (currentStep > 1)
        {
            currentStep--;
            content.anchoredPosition += new Vector2(offset, 0);

        }
    }
}
