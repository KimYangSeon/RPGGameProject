using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Vector2 mapSize;
    [SerializeField]
    // RectTransform map;

    Vector3 cameraPos = new Vector3(0, 0, -10);

    float height;
    float width;
    float minX;
    float minY;

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;

        minX = mapSize.x - width;
        minY = mapSize.y - height;
    }
    void FixedUpdate()
    {
        transform.position = playerTransform.position + cameraPos;

        //float clampX = Mathf.Clamp(transform.position.x, -minX, minX);
        //float clampY = Mathf.Clamp(transform.position.y, -minY, minY);

        //transform.position = new Vector3(clampX, clampY, -10f);
    }
}
