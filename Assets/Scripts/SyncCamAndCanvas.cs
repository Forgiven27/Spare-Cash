using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCamAndCanvas : MonoBehaviour
{
    public Canvas canvas;
    public Camera camera;

    void Awake()
    {
        if (canvas != null && camera != null)
        {
            SyncSizes();
        }
    }

    void SyncSizes()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        float canvasHeight = canvasRect.rect.height;
        float canvasWidth = canvasRect.rect.width;

        float aspectRatio = canvasWidth / canvasHeight;
        float cameraSize = canvasHeight / 2f;

        camera.orthographicSize = cameraSize;
        camera.aspect = aspectRatio;
    }
}