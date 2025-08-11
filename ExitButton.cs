using UnityEngine;

public class ExitButton : MonoBehaviour
{
     public void ExitApplication()
    {
        // Encerra o aplicativo
        Application.Quit();

        // Se estiver rodando no editor, para a simulação
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
