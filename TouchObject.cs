using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchObject : MonoBehaviour
{
    public Canvas canvasCube,canvasSphere, canvasTriangle, canvaBack;

    void Start()
    {
        // Inicializa os canvases como desativados
        HideAllCanvases();
        RotateObject.OnAnyRotateObjectDisabled += HandleRotateObjectDisabled;
    }

    void OnDestroy()
    {
        // Remove o evento quando o objeto for destru�do
        RotateObject.OnAnyRotateObjectDisabled -= HandleRotateObjectDisabled;
    }

    void Update()
    {
        // Verifica se o bot�o esquerdo do mouse foi clicado ou se h� um toque na tela
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Desativa todos as rota��es dos objetos
                StopAllRotations();

                // Esconde todos os canvases
                HideAllCanvases();

                // Ativar o canvas e a rota��o correspondente ao objeto tocado
                switch (hit.transform.name)
                {
                    case "Cube":
                        canvasCube.enabled = true;
                        hit.transform.GetComponent<RotateObject>().StartRotation();
                        break;
                    case "Sphere":
                        canvasSphere.enabled = true;
                        hit.transform.GetComponent<RotateObject>().StartRotation();
                        break;
                    case "Triangle":
                        canvasTriangle.enabled = true;
                        hit.transform.GetComponent<RotateObject>().StartRotation();
                        break;
                    default:
                        // Se o objeto tocado n�o for reconhecido, mant�m todos os canvases escondidos
                        break;
                }

            }
        }
    }

    /// <summary>
    /// Respons�vel por esconder todos os canvases de objetos 3D
    /// e ativar o canvas do bot�o "Back".
    /// </summary>
    private void HideAllCanvases()
    {
        canvasCube.enabled = false;
        canvasSphere.enabled = false;
        canvasTriangle.enabled = false;
        canvaBack.enabled = true;
    }

    /// <summary>
    /// Respons�vel por parar a rota��o de todos os objetos na cena.
    /// </summary>
    private void StopAllRotations()
    {
        foreach (var rotObj in FindObjectsByType<RotateObject>(FindObjectsSortMode.None))
            rotObj.StopRotation();
    }

    /// <summary>
    /// Respons�vel por esconder todos os canvases quando um objeto
    /// Verifica se o objeto que foi desativado � um RotateObject atrav�s do evento
    /// </summary>
    /// <param name="obj">� o objeto RotateObject que foi desativado. </param>
    private void HandleRotateObjectDisabled(RotateObject obj)
    {
        HideAllCanvases();
    }

}
