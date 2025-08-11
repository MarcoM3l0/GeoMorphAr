using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCanvasController : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public Canvas canvaCube, canvaSphere, canvaTriangle, canvaBack;

    void OnEnable() => trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);

    void OnDisable() => trackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);

    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.updated)
        {
            if(trackedImage.trackingState != TrackingState.Tracking)
            {
                HideCanvasByName();
            }
        }
    }

    public void HideCanvasByName()
    {
        canvaCube.enabled = false;
        canvaSphere.enabled = false;
        canvaTriangle.enabled = false;

        canvaBack.enabled = true;
    }
}
