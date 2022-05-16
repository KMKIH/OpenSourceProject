using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSizeBackground : MonoBehaviour
{
    float scaleX;
    float scaleY;
    private void Awake()
    {
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        transform.localScale = new Vector2(scaleX * Camera.main.orthographicSize, scaleY * Camera.main.orthographicSize);
    }
    private void Update()
    {
        transform.localScale = new Vector2(scaleX * Camera.main.orthographicSize, scaleY * Camera.main.orthographicSize);
    }
}
