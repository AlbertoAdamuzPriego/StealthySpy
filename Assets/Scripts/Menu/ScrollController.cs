using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controla el funcionamiento del scrollbar
public class ScrollController : MonoBehaviour
{
    private int currentStep; //Paso actual
    private int maxSteps; //Máximo de pasos
    [SerializeField] RectTransform content; //Contenedor del scrollbar
    Scrollbar scroll; 
    [SerializeField] int offset; //Distancia entre botones
 
    void Start()
    {
        currentStep = 1;
        maxSteps = FindAnyObjectByType<MapManager>().GetNumberOfMaps(); 
        scroll = GetComponentInChildren<Scrollbar>();
        
        //Calcula el paso actual segun la posición del scroll
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
 
    //Mueve el scroll al siguiente paso, si existe
    public void Next()
    {
        //Centra el scrollbar (para evitar que los botones queden desplazados)
        content.anchoredPosition = (currentStep-1) * new Vector2(-offset, 0);

        //Actualiza la posición del scrollbar
        if(currentStep < maxSteps) 
        {
            currentStep++;
            content.anchoredPosition += new Vector2(-offset, 0);
        }
       
    }

    //Mueve el scroll al anterior paso, si existe
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
