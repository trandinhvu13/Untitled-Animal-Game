using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private float _9By16CameraSize;
    [SerializeField] private float _9By195CameraSize;

    private const float RATIO_9_16 = 9f / 16f;
    private const float RATIO_9_195 = 9f / 19.5f;

    private float _aspectRatio;
    private Camera _camera;

    // Use this for initialization
    void Start()
    {
        _camera = GetComponent<Camera>();
        _aspectRatio = (float)Screen.width / Screen.height;
        var t = (_aspectRatio - RATIO_9_195) / (RATIO_9_16 - RATIO_9_195);
        var size = Mathf.Lerp(_9By195CameraSize, _9By16CameraSize, t);
        _camera.orthographicSize = size;
    }
}
