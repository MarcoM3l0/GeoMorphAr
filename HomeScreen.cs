using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public Canvas canvaHomeScreen;
    public Canvas canvaBack;

    /// <summary>
    /// Função chamada ao clicar no botão para alternar entre a tela inicial e a tela anterior.
    /// Inverte a visibilidade dos canvases e inicia a transição entre as telas.
    /// </summary>
    public void ClickButton()
    {
        // Desativa o canvas da tela inicial ao clicar no botão
        canvaHomeScreen.enabled = false;
        // Ativa o canvas de volta
        canvaBack.enabled = true;
    }
}
