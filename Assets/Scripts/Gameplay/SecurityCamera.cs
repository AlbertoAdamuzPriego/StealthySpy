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
        startRotation = transform.rotation; // Rotación inicial del objeto
        targetRotation = finalRotation; // Rotación objetivo del objeto
    }

    private void Update()
    {
        float z = transform.rotation.z; 
        // Incrementar el progreso de la rotación
        progress += rotationSpeed * Time.deltaTime;

        // Interpolar la rotación actual entre la rotación inicial y la rotación objetivo
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);

        // Si se ha alcanzado la rotación objetivo, reiniciar la rotación
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
