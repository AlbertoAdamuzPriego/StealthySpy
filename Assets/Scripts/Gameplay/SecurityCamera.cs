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

    void Start()
    {
        startRotation = transform.rotation; // Rotaci�n inicial del objeto
        targetRotation = finalRotation; // Rotaci�n objetivo del objeto
    }

    private void Update()
    {
        float z = transform.rotation.z; 
        // Incrementar el progreso de la rotaci�n
        progress += rotationSpeed * Time.deltaTime;

        // Interpolar la rotaci�n actual entre la rotaci�n inicial y la rotaci�n objetivo
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);

        // Si se ha alcanzado la rotaci�n objetivo, reiniciar la rotaci�n
        if (progress >= 1f)
        {
            progress = 0f;
            Quaternion tempRotation = startRotation;
            startRotation = targetRotation;
            targetRotation = tempRotation;
        }

        bool player=fov.DrawMesh(transform.rotation.z*100);
    }


}
