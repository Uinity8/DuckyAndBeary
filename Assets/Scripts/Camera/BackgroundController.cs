using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponentInParent<Camera>();
        transform.localScale = Vector3.one * (mainCamera.orthographicSize / 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * (mainCamera.orthographicSize / 3f);
    }
}
