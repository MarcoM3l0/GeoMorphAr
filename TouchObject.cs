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
        SetCanvasEnabled(canvasCube, false);
        SetCanvasEnabled(canvasSphere, false);
        SetCanvasEnabled(canvasTriangle, false);
        SetCanvasEnabled(canvaBack, canvaBack != null && !canvaBack.enabled);
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

    /// <summary>
    /// Ativa ou desativa um <see cref="Canvas"/> de forma segura,
    /// verificando se o objeto n�o foi destru�do antes de acessar a propriedade.
    /// </summary>
    /// <param name="canvas">O componente Canvas a ser modificado.</param>
    /// <param name="enabled">Define se o Canvas ficar� habilitado (true) ou desabilitado (false).</param>
    private void SetCanvasEnabled(Canvas canvas, bool enabled)
    {
        if (canvas != null && canvas.gameObject != null)
        {
            // Checa se n�o foi destru�do antes de acessar
            if (canvas)
                canvas.enabled = enabled;
        }
    }

}
