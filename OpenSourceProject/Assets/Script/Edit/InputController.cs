using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    private TileType_Edit currentType = TileType_Edit.Empty;  // 마우스 클릭된 위치의 타일을 currentType 속성으로 변경
    private Tile_Edit playerTile = null;                 // 플레이어 타일 정보
    private Tile_Edit blinkTile = null;                 // 플레이어 타일 정보

    [SerializeField]
    private CameraController_Edit cameraController;      // 카메라 위치, 줌 제어를 위한 CameraController
    private Vector2 previousMousePosition;          // 직전 프레임의 마우스 위치
    private Vector2 currentMousePosition;           // 현전 프레임의 마우스 위치

    private void Update()
    {
        // 현재 마우스가 UI 위에 있을 때 IsPositionOverGameObject()가 true 반환
        // 즉, 마우스가 UI 위에 있을 때는 Update() 내용이 실행되지 않는다.
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

        // 카메라 이동, Zoom In/Out
        UpdateCamera();

        RaycastHit hit;
        // 마우스 왼쪽 버튼을 누르고 있을 때
        if (Input.GetMouseButton(0))
        {
            // 카메라로부터 현재 마우스 위치로 뻗어나가는 광선 생성
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // 광선에 부딪힌 오브젝트가 존재하면 hit에 저장
            if( Physics.Raycast(ray, out hit, Mathf.Infinity) )
            {
                // hit 오브젝트의 Tile 컴포넌트 정보를 불러와 tile 변수에 저장
                // 이 때 hit 오브젝트에 Tile 컴포넌트가 없으면 null 반환
                Tile_Edit tile = hit.transform.GetComponent<Tile_Edit>();
                if( tile != null )
                {   
                    // 플레이어 타일은 맵에 1개만 배치할 수 있기 때문에
                    // 이전에 배치된 플레이어 타일이 있으면 Empty 속성으로 설정
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
                    // 부딪힌 오브젝트를 tileType 속성으로 변경 (타일, 아이템, 플레이어 캐릭터)
                    tile.TileType_Edit = currentType;
                }
            }
        }
    }

    /// <summary>
    /// 타일, 아이템, 플레이어 캐릭터 버튼을 눌러 tileType을 변경
    /// </summary>
    public void SetTileType(int tileType)
    {
        currentType = (TileType_Edit)tileType;
    }

    public void UpdateCamera()
    {
        // 키보드를 이용한 카메라 이동
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        cameraController.SetPosition(x,y);

        // 마우스 휠 버튼을 이용한 카메라 이동
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

        // 마우스 휠 버튼을 이용한 카메라 Zoom In/Out
        float distance = Input.GetAxisRaw("Mouse ScrollWheel");
        cameraController.SetOrthographicSize(-distance);
    }
}
