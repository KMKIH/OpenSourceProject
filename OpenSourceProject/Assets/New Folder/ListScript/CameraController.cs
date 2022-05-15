using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Tilemap2D   tilemap2D;          // �� ũ�� ������ ������ ���� Tilemap2D
    private Camera      mainCamera;         // ī�޶� �þ߼����� ���� Camera

    [SerializeField]
    private float moveSpeed;                // ī�޶� �̵� �ӵ�
    [SerializeField]
    private float zoomSpeed;                // ī�޶� �� �ӵ�
    [SerializeField]
    private float minViewSize = 2;          // ī�޶� �þ� �ּ� ũ��
    private float maxViewSize;              // ī�޶� �þ� �ִ� ũ��

    private float       wDelta = 0.4f;      // width �þ� delta��
    private float       hDelta = 0.6f;      // height �þ� delta��

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    public void SetupCamera()
    {
        // ���� ũ�� ����
        int width = tilemap2D.Width;
        int height = tilemap2D.Height;

        // ī�޶� �þ� ���� (��ü ���� ȭ�� �ȿ� �������� ����)
        float size = (width > height) ? width*wDelta : height*hDelta;
        mainCamera.orthographicSize = size; 

        // ī�޶� ��ġ ���� (y�� ��ǥ)
        if (height > width)
        {
            // ���� ����(height)�� �� Ŭ ��� ī�޶��� y�� ��ġ�� �Ϻ� ����
            Vector3 position = new Vector3(0, 0.05f, -10);
            position.y *= height;

            transform.position = position;
        }

        maxViewSize = mainCamera.orthographicSize;
    }

    public void SetPosition(float x, float y)
    {
        transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
    }

    public void SetOrthographicSize(float size)
    {
        if (size == 0) return;

        // ī�޶� �þ� ����
        mainCamera.orthographicSize += size * zoomSpeed * Time.deltaTime;
        // ī�޶� �þ� ������ (minViewSize <= orthographicSize <= maxViewSize)
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minViewSize, maxViewSize);
    }
}
