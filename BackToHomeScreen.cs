using UnityEngine;

public class BackToHomeScreen : MonoBehaviour
{
    public Canvas canvaHomeScreen, canvaBack;

    public GameObject[] objectsToUnload;

    /// <summary>
    /// Função chamada ao clicar no botão "Voltar" para retornar à tela inicial.
    /// Gerencia a visibilidade dos canvases e descarrega os objetos desnecessários para liberar memória.
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
        System.GC.Collect(); // Força a coleta de lixo para liberar memória
    }
}
