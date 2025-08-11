using UnityEngine;

public class ButtonBack : MonoBehaviour
{
    public Canvas canvaCube;
    public Canvas canvaSphere;
    public Canvas canvaTriangle;
    public Canvas canvaBack;

    /// <summary>
    /// Função chamada ao clicar no botão "Back".
    /// Garante que todos os canvases de objetos 3D sejam desativados
    /// </summary>
    public void ClickButton()
    {
        // Desativa os canvases
        canvaCube.enabled = false;
        canvaSphere.enabled = false;
        canvaTriangle.enabled = false;
        canvaBack.enabled = true;

        // Desativa a rotação dos objetos
        StopAllRotations();
    }

    /// <summary>
    /// Responsável por parar a rotação de todos os objetos na cena.
    /// </summary>
    private void StopAllRotations()
    {
        foreach (var rotObj in FindObjectsByType<RotateObject>(FindObjectsSortMode.None))
            rotObj.StopRotation();
    }
}
