using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Controlador de las c�maras de seguridad
public class SecurityCamera : MonoBehaviour
{
    [SerializeField] Quaternion finalRotation; //Rotaci�n final de la cabeza
    [SerializeField] float rotationSpeed; //Velocidad de rotaci�n
    [SerializeField] VisionTriangleController fov; //�rea de visi�n

    private Quaternion startRotation; //Rotaci�n inicial (por defecto la que tiene en la escena)
    private Quaternion targetRotation; //Rotaci�n objetivo (por defecto finalRotation)
    private float progress = 0f; //Indica el progreso de rotaci�n

    [SerializeField] private float waitTime; //Tiempo de espera

    private float timer; //Contador para el tiempo de espera
   

    void Start()
    {
        startRotation = transform.rotation; // Rotaci�n inicial del objeto
        targetRotation = finalRotation; // Rotaci�n objetivo del objeto
        timer = 0;
        
    }

    private void Update()
    { 
        // Incrementar el progreso de la rotaci�n
        progress += rotationSpeed * Time.deltaTime;

        // Interpolar la rotaci�n actual entre la rotaci�n inicial y la rotaci�n objetivo
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);

        // Si se ha alcanzado la rotaci�n objetivo, reiniciar la rotaci�n
        if (progress >= 1f)
        {
            UpdateTimer();
      
            if (timer>waitTime)
            {
                progress = 0f;

                //Ahora la rotaci�n inicial y objetivo se intercambian para revertir el movimiento
                Quaternion tempRotation = startRotation;
                startRotation = targetRotation;
                targetRotation = tempRotation;
                timer = 0;
            }
    
        }

        //Se redibuja el mesh con la rotaci�n actual 
        else
           fov.DrawMesh(transform.rotation.z*100);

    
    }

    //Actualiza el contador
    private void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

}
