using UnityEngine;

public class BackToHomeScreen : MonoBehaviour
{
    public Canvas canvaHomeScreen, canvaBack;

    public GameObject[] objectsToUnload;

    /// <summary>
    /// Fun��o chamada ao clicar no bot�o "Voltar" para retornar � tela inicial.
    /// Gerencia a visibilidade dos canvases e descarrega os objetos desnecess�rios para liberar mem�ria.
    /// </summary>
    public void ClickButton()
    {
        // Altera a visibilidade dos canvases
        canvaHomeScreen.enabled = !canvaHomeScreen.enabled;
        canvaBack.enabled = !canvaBack.enabled;

        // Desativa os objetos que devem ser descarregados
        foreach (GameObject obj in objectsToUnload)
        {
            if (obj == null) continue;

            obj.SetActive(false);
            Destroy(obj);
        }

        Resources.UnloadUnusedAssets();
        System.GC.Collect(); // For�a a coleta de lixo para liberar mem�ria
    }
}
