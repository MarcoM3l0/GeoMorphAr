using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    ARTrackedImageManager trackedImageManager;
    public GameObject[] ArPrefabs;

    // Dicion�rio para mapear nomes de imagens rastreadas aos seus respectivos objetos AR
    readonly Dictionary<string, GameObject> ArObjects = new Dictionary<string, GameObject>();

    void Awake() => trackedImageManager = GetComponent<ARTrackedImageManager>();

    void OnEnable() => trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);

    void OnDisable() => trackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);


    /// <summary>
    /// OnTrackedImagesChanged � chamado quando h� mudan�as nas imagens rastreadas.
    /// Quando uma nova imagem � detectada ou quando uma imagem existente � atualizada,
    /// garante que o objeto AR correspondente seja instanciado ou atualizado conforme necess�rio.
    /// </summary>
    /// <param name="eventArgs">Cont�m informa��es sobre as imagens rastreadas.</param>
    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {

        // Cria objeto com base na imagem rastreada
        foreach (var trackedImage in eventArgs.added)
        {
            string imageName = trackedImage.referenceImage.name;

            foreach (var arPrefab in ArPrefabs)
            {
                if (imageName == arPrefab.name && !ArObjects.ContainsKey(imageName))
                {
                    // Cria o GameObject vazio como �ncora
                    GameObject anchorObject = new GameObject("Anchor_" + imageName);
                    anchorObject.transform.SetParent(trackedImage.transform);
                    anchorObject.transform.localPosition = Vector3.zero;
                    anchorObject.transform.localRotation = Quaternion.identity;

                    // Instancia o prefab como filho do objeto �ncora
                    GameObject arObject = Instantiate(arPrefab, anchorObject.transform);
                    arObject.name = imageName;

                    arObject.transform.localRotation = Quaternion.identity;

                    ArObjects.Add(imageName, arObject);
                }
            }
        }

        // Atualiza o objeto com base na imagem rastreada
        foreach (var trackedImage in eventArgs.updated)
        {
            string imageName = trackedImage.referenceImage.name;

            if (ArObjects.TryGetValue(imageName, out GameObject arObject))
            {
                arObject.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }
        }

        // Remove o objeto AR quando a imagem n�o � mais rastreada
        foreach (var trackedImagePair in eventArgs.removed)
        {
            ARTrackedImage trackedImage = trackedImagePair.Value;
            string imageName = trackedImage.referenceImage.name;

            if (ArObjects.TryGetValue(imageName, out GameObject arObject))
            {
                Destroy(arObject);
                ArObjects.Remove(imageName);
            }
        }

    }

}
