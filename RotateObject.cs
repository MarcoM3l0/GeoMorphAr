using UnityEngine;
using System;

public class RotateObject : MonoBehaviour
{
    public static event Action<RotateObject> OnAnyRotateObjectDisabled;

    public bool isRotating = false;
    public float rotationSpeed = 150f;

    void Awake()
    {
        // Certifica-se de que o objeto n�o est� rotacionando inicialmente
        isRotating = false;

    }

    // Atualiza a rota��o do objeto a cada frame
    void Update()
    {
        // Rotaciona o objeto em seu pr�prio eixo
        if (isRotating)
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// Funciona para iniciar a rota��o do objeto
    /// garantindo que a rota��o comece do estado parado.
    /// </summary>
    public void StartRotation()
    {
        isRotating = true;
    }

    /// <summary>
    /// Funciona para parar a rota��o do objeto
    /// garantindo que a rota��o pare imediatamente.
    /// </summary>
    public void StopRotation()
    {
        isRotating = false;
        transform.localRotation = Quaternion.identity; // Reseta a rota��o local do objeto
    }

    void OnDisable()
    {
        // Garante que a rota��o esteja desativada quando o objeto for desativado
        StopRotation();
        OnAnyRotateObjectDisabled?.Invoke(this);
    }
}
