using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Controlador de las cámaras de seguridad
public class SecurityCamera : MonoBehaviour
{
    [SerializeField] Quaternion finalRotation; //Rotación final de la cabeza
    [SerializeField] float rotationSpeed; //Velocidad de rotación
    [SerializeField] VisionTriangleController fov; //Área de visión

    private Quaternion startRotation; //Rotación inicial (por defecto la que tiene en la escena)
    private Quaternion targetRotation; //Rotación objetivo (por defecto finalRotation)
    private float progress = 0f; //Indica el progreso de rotación

    [SerializeField] private float waitTime; //Tiempo de espera

    private float timer; //Contador para el tiempo de espera
   

    void Start()
    {
        startRotation = transform.rotation; // Rotación inicial del objeto
        targetRotation = finalRotation; // Rotación objetivo del objeto
        timer = 0;
        
    }

    private void Update()
    { 
        // Incrementar el progreso de la rotación
        progress += rotationSpeed * Time.deltaTime;

        // Interpolar la rotación actual entre la rotación inicial y la rotación objetivo
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);

        // Si se ha alcanzado la rotación objetivo, reiniciar la rotación
        if (progress >= 1f)
        {
            UpdateTimer();
      
            if (timer>waitTime)
            {
                progress = 0f;

                //Ahora la rotación inicial y objetivo se intercambian para revertir el movimiento
                Quaternion tempRotation = startRotation;
                startRotation = targetRotation;
                targetRotation = tempRotation;
                timer = 0;
            }
    
        }

        //Se redibuja el mesh con la rotación actual 
        else
           fov.DrawMesh(transform.rotation.z*100);

    
    }

    //Actualiza el contador
    private void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

}
