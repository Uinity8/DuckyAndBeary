using UnityEngine;

public class UICanvasManager
{
    private Canvas mainCanvas;

    public Canvas GetOrCreateCanvas()
    {
        if (mainCanvas == null)
        {
            mainCanvas = Object.FindObjectOfType<Canvas>();
            if (mainCanvas == null)
            {
                GameObject canvasObj = new GameObject("[UI]");
                mainCanvas = canvasObj.AddComponent<Canvas>();
                mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                Debug.Log("Main UI Canvas created.");
            }
        }
        return mainCanvas;
    }
}