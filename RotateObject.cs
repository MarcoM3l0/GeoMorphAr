using UnityEngine;
using System;

public class RotateObject : MonoBehaviour
{
    public static event Action<RotateObject> OnAnyRotateObjectDisabled;

    public bool isRotating = false;
    public float rotationSpeed = 150f;

    void Awake()
    {
        // Certifica-se de que o objeto não está rotacionando inicialmente
        isRotating = false;

    }

    // Atualiza a rotação do objeto a cada frame
    void Update()
    {
        // Rotaciona o objeto em seu próprio eixo
        if (isRotating)
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// Funciona para iniciar a rotação do objeto
    /// garantindo que a rotação comece do estado parado.
    /// </summary>
    public void StartRotation()
    {
        isRotating = true;
    }

    /// <summary>
    /// Funciona para parar a rotação do objeto
    /// garantindo que a rotação pare imediatamente.
    /// </summary>
    public void StopRotation()
    {
        isRotating = false;
        transform.localRotation = Quaternion.identity; // Reseta a rotação local do objeto
    }

    void OnDisable()
    {
        // Garante que a rotação esteja desativada quando o objeto for desativado
        StopRotation();
        OnAnyRotateObjectDisabled?.Invoke(this);
    }
}
