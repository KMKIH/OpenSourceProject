using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    private TileType_Edit currentType = TileType_Edit.Empty;  // ���콺 Ŭ���� ��ġ�� Ÿ���� currentType �Ӽ����� ����
    private Tile_Edit playerTile = null;                 // �÷��̾� Ÿ�� ����
    private Tile_Edit blinkTile = null;                 // �÷��̾� Ÿ�� ����

    [SerializeField]
    private CameraController_Edit cameraController;      // ī�޶� ��ġ, �� ��� ���� CameraController
    private Vector2 previousMousePosition;          // ���� �������� ���콺 ��ġ
    private Vector2 currentMousePosition;           // ���� �������� ���콺 ��ġ

    private void Update()
    {
        // ���� ���콺�� UI ���� ���� �� IsPositionOverGameObject()�� true ��ȯ
        // ��, ���콺�� UI ���� ���� ���� Update() ������ ������� �ʴ´�.
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

        // ī�޶� �̵�, Zoom In/Out
        UpdateCamera();

        RaycastHit hit;
        // ���콺 ���� ��ư�� ������ ���� ��
        if (Input.GetMouseButton(0))
        {
            // ī�޶�κ��� ���� ���콺 ��ġ�� ������� ���� ����
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // ������ �ε��� ������Ʈ�� �����ϸ� hit�� ����
            if( Physics.Raycast(ray, out hit, Mathf.Infinity) )
            {
                // hit ������Ʈ�� Tile ������Ʈ ������ �ҷ��� tile ������ ����
                // �� �� hit ������Ʈ�� Tile ������Ʈ�� ������ null ��ȯ
                Tile_Edit tile = hit.transform.GetComponent<Tile_Edit>();
                if( tile != null )
                {   
                    // �÷��̾� Ÿ���� �ʿ� 1���� ��ġ�� �� �ֱ� ������
                    // ������ ��ġ�� �÷��̾� Ÿ���� ������ Empty �Ӽ����� ����
                    if(currentType == TileType_Edit.Player)
                    {
                        if(playerTile != null)
                        {
                            playerTile.TileType_Edit = TileType_Edit.Empty;
                        }
                        playerTile = tile;
                    }
                    if (currentType == TileType_Edit.Blink)
                    {
                        if (blinkTile != null)
                        {
                            blinkTile.TileType_Edit = TileType_Edit.Empty;
                        }
                        blinkTile = tile;
                    }
                    // �ε��� ������Ʈ�� tileType �Ӽ����� ���� (Ÿ��, ������, �÷��̾� ĳ����)
                    tile.TileType_Edit = currentType;
                }
            }
        }
    }

    /// <summary>
    /// Ÿ��, ������, �÷��̾� ĳ���� ��ư�� ���� tileType�� ����
    /// </summary>
    public void SetTileType(int tileType)
    {
        currentType = (TileType_Edit)tileType;
    }

    public void UpdateCamera()
    {
        // Ű���带 �̿��� ī�޶� �̵�
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        cameraController.SetPosition(x,y);

        // ���콺 �� ��ư�� �̿��� ī�޶� �̵�
        if (Input.GetMouseButtonDown(2))
        {
            currentMousePosition = previousMousePosition = Input.mousePosition;
        }
        else if(Input.GetMouseButton(2))
        {
            currentMousePosition = Input.mousePosition;
            if(previousMousePosition != currentMousePosition)
            {
                Vector2 move = (previousMousePosition - currentMousePosition) * 0.5f;
                cameraController.SetPosition(move.x, move.y);   
            }
        }
        previousMousePosition = currentMousePosition;

        // ���콺 �� ��ư�� �̿��� ī�޶� Zoom In/Out
        float distance = Input.GetAxisRaw("Mouse ScrollWheel");
        cameraController.SetOrthographicSize(-distance);
    }
}
