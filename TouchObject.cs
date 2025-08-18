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
        SetCanvasEnabled(canvasCube, false);
        SetCanvasEnabled(canvasSphere, false);
        SetCanvasEnabled(canvasTriangle, false);
        SetCanvasEnabled(canvaBack, canvaBack != null && !canvaBack.enabled);
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

    /// <summary>
    /// Ativa ou desativa um <see cref="Canvas"/> de forma segura,
    /// verificando se o objeto não foi destruído antes de acessar a propriedade.
    /// </summary>
    /// <param name="canvas">O componente Canvas a ser modificado.</param>
    /// <param name="enabled">Define se o Canvas ficará habilitado (true) ou desabilitado (false).</param>
    private void SetCanvasEnabled(Canvas canvas, bool enabled)
    {
        if (canvas != null && canvas.gameObject != null)
        {
            // Checa se não foi destruído antes de acessar
            if (canvas)
                canvas.enabled = enabled;
        }
    }

}
