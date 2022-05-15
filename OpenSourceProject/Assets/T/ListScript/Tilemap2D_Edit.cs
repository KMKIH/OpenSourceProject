using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tilemap2D_Edit : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;          // 맵에 배치되는 타일 프리팹

    [SerializeField]
    private TMP_InputField inputWidth;      // 맵의 width 크기를 얻어오는 inputfield
    [SerializeField]
    private TMP_InputField inputHeight;     // 맵의 height 크기를 얻어오는 inputfield
    [SerializeField]
    private TMP_InputField inputName;     // 맵의 이름을 얻어오는 inputName

    private MapData mapData;                // 맵 데이터 저장에 사용되는 데이터 양식 클래스

    // 맵 x, y 크기 프로퍼티
    public int Width    { private set; get; } = 10;
    public int Height   { private set; get; } = 10;

    // 맵에 배치되는 타일 정보 저장을 위한 List 프로퍼티
    public List<Tile_Edit> TileList { private set;get;}

    private void Awake()
    {
        // InputField에 표시되는 기본값 설정
        inputWidth.text = Width.ToString();
        inputHeight.text = Height.ToString();
        inputName.text = "NoName.json";

        mapData = new MapData();
        TileList = new List<Tile_Edit>();
    }

    public void GenerateTilemap()
    {
        // out 또는 ref 키워드를 사용할 때 프로퍼티 사용이 불가능하기 때문에 지역변수 선언
        int width, height;

        // InputField에 있는 width, height 문자열을 width, height 변수에 정수로 저장
        int.TryParse(inputWidth.text, out width);
        int.TryParse(inputHeight.text, out height);

        // 프로퍼티 Width, Height 값 설정
        Width = width;
        Height = height;

        for( int y = 0; y < Height; y++ )
        {
            for(int x = 0; x < Width; x++ )
            {
                // 생성되는 타일맵의 중앙이 (0, 0, 0)인 위치
                Vector3 postion = new Vector3((-Width * 0.5f + 0.5f) + x, (Height * 0.5f - 0.5f) - y, 0);

                SpawnTile(TileType_Edit.Empty, postion);
            }
        }

        mapData.mapSize.x = Width;
        mapData.mapSize.y = Height;
        mapData.mapData = new int[TileList.Count];
    }

    public void SpawnTile(TileType_Edit tiletype, Vector3 postion)
    {
        GameObject clone = Instantiate(tilePrefab, postion, Quaternion.identity);

        clone.name = "Tile";                        // Tile 오브젝트의 이름을 "Tile"로 설정
        clone.transform.SetParent(transform);       // Tilemap2D 오브젝트를 Tile 오브젝트의 부모로 설정

        Tile_Edit tile = clone.GetComponent<Tile_Edit>();     // 방금 생성한 타일(Clone) 오브젝트의 Tile.Setup() 메소드 호출
        tile.Setup(tiletype);

        TileList.Add(tile);
    }

    public MapData GetMapData()
    {
        // 맵에 배치된 모든 타일의 정보를 mapData.mapData 배열에 저장
        for (int i = 0; i < TileList.Count; i++)
        {
            if(TileList[i].TileType_Edit != TileType_Edit.Player)
            {
                mapData.mapData[i] = (int)TileList[i].TileType_Edit; 
            }
            // 현재 위치의 타일이 플레이어라면
            else
            {
                // 현재 위치의 타일은 빈 타일(Empty)로 설정
                mapData.mapData[i] = (int)TileType.Empty;

                // 현재 위치 정보를 mapData.playerPosition에 저장
                int x = (int)TileList[i].transform.position.x;
                int y = (int)TileList[i].transform.position.y;
                mapData.playerPositionl = new Vector2Int(x, y);
            }
        }

        return mapData;
    }

    public void findMapdata()
    {
        // inputFirld UI에 입력된 텍스트 정보를 불러와서 fileName에 저장
        string fileName = inputName.text;
        // fileName에 ".json" 문장이 없으면 입력해준다
        // ex) "Stage01" => "Stage01.json"
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }

        // MapDataLoader 클래스 인스턴스 생성 및 메모리 할당
        MapDataLoader mapDataLoader = new MapDataLoader();

        //MapData mapData = mapDataLoader.Load(fileName);
        mapData = mapDataLoader.Load(fileName);

        LoadTilemap(mapData);
    }

    public void LoadTilemap(MapData mapdata)
    {
        // mapData 정보를 바탕으로 타일 형태의 맵 생성
        int width = mapData.mapSize.x;
        int height = mapData.mapSize.y;

        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // 격자 형태로 배치된 타일들을 왼쪽 상단부터 순차적으로 번호를 부여
                // 0, 1, 2, 3, 4, 5,
                //6, 7, ...
                int index = y * width + x;
               
                // 생성되는 타일맵의 중앙이 (0, 0, 0)인 위치
                Vector3 position = new Vector3(-(width * 0.5f - 0.5f) + x, (height * 0.5f - 0.5f) - y);

                // 타일 생성
                SpawnTile((TileType_Edit)mapData.mapData[index], position);

            }
        }
    }
}
