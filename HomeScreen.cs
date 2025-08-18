using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public Canvas canvaHomeScreen;
    public Canvas canvaBack;

    /// <summary>
    /// Fun��o chamada ao clicar no bot�o para alternar entre a tela inicial e a tela anterior.
    /// Inverte a visibilidade dos canvases e inicia a transi��o entre as telas.
    /// </summary>
    public void ClickButton()
    {
        // Desativa o canvas da tela inicial ao clicar no bot�o
        canvaHomeScreen.enabled = false;
        // Ativa o canvas de volta
        canvaBack.enabled = true;
    }
}
