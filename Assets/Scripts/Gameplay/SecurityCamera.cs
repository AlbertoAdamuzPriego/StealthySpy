using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] Quaternion finalRotation;
    [SerializeField] float rotationSpeed;
    [SerializeField] VisionTriangleController fov;

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float progress = 0f;
    private bool playerIsDetected;

    [SerializeField] private float waitTime;

    private float timer;

    void Start()
    {
        startRotation = transform.rotation; // Rotaci�n inicial del objeto
        targetRotation = finalRotation; // Rotaci�n objetivo del objeto
        timer = 0;
        playerIsDetected = false;
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
                Quaternion tempRotation = startRotation;
                startRotation = targetRotation;
                targetRotation = tempRotation;
                timer = 0;
            }
    
        }

        else
           playerIsDetected = fov.DrawMesh(transform.rotation.z*100);
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
    }
}
