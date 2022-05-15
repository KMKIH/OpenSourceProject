using UnityEngine;

public class CameraController_Player: MonoBehaviour
{

    private float wDelta = 0.35f;
    private float hDelta = 0.6f;

    public void Setup(int width, int height)
    {
        float size = (width > height) ? width*wDelta : height*hDelta;

        GetComponent<Camera>().orthographicSize = size;
    }
    
}
