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
        // Remove o evento quando o objeto for destruído
        RotateObject.OnAnyRotateObjectDisabled -= HandleRotateObjectDisabled;
    }

    void Update()
    {
        // Verifica se o botão esquerdo do mouse foi clicado ou se há um toque na tela
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Desativa todos as rotações dos objetos
                StopAllRotations();

                // Esconde todos os canvases
                HideAllCanvases();

                // Ativar o canvas e a rotação correspondente ao objeto tocado
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
                        // Se o objeto tocado não for reconhecido, mantém todos os canvases escondidos
                        break;
                }

            }
        }
    }

    /// <summary>
    /// Responsável por esconder todos os canvases de objetos 3D
    /// e ativar o canvas do botão "Back".
    /// </summary>
    private void HideAllCanvases()
    {
        canvasCube.enabled = false;
        canvasSphere.enabled = false;
        canvasTriangle.enabled = false;
        canvaBack.enabled = true;
    }

    /// <summary>
    /// Responsável por parar a rotação de todos os objetos na cena.
    /// </summary>
    private void StopAllRotations()
    {
        foreach (var rotObj in FindObjectsByType<RotateObject>(FindObjectsSortMode.None))
            rotObj.StopRotation();
    }

    /// <summary>
    /// Responsável por esconder todos os canvases quando um objeto
    /// Verifica se o objeto que foi desativado é um RotateObject através do evento
    /// </summary>
    /// <param name="obj">É o objeto RotateObject que foi desativado. </param>
    private void HandleRotateObjectDisabled(RotateObject obj)
    {
        HideAllCanvases();
    }

}
