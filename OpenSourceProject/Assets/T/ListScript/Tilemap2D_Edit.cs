using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tilemap2D_Edit : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;          // �ʿ� ��ġ�Ǵ� Ÿ�� ������

    [SerializeField]
    private TMP_InputField inputWidth;      // ���� width ũ�⸦ ������ inputfield
    [SerializeField]
    private TMP_InputField inputHeight;     // ���� height ũ�⸦ ������ inputfield
    [SerializeField]
    private TMP_InputField inputName;     // ���� �̸��� ������ inputName

    private MapData mapData;                // �� ������ ���忡 ���Ǵ� ������ ��� Ŭ����

    // �� x, y ũ�� ������Ƽ
    public int Width    { private set; get; } = 10;
    public int Height   { private set; get; } = 10;

    // �ʿ� ��ġ�Ǵ� Ÿ�� ���� ������ ���� List ������Ƽ
    public List<Tile_Edit> TileList { private set;get;}

    private void Awake()
    {
        // InputField�� ǥ�õǴ� �⺻�� ����
        inputWidth.text = Width.ToString();
        inputHeight.text = Height.ToString();
        inputName.text = "NoName.json";

        mapData = new MapData();
        TileList = new List<Tile_Edit>();
    }

    public void GenerateTilemap()
    {
        // out �Ǵ� ref Ű���带 ����� �� ������Ƽ ����� �Ұ����ϱ� ������ �������� ����
        int width, height;

        // InputField�� �ִ� width, height ���ڿ��� width, height ������ ������ ����
        int.TryParse(inputWidth.text, out width);
        int.TryParse(inputHeight.text, out height);

        // ������Ƽ Width, Height �� ����
        Width = width;
        Height = height;

        for( int y = 0; y < Height; y++ )
        {
            for(int x = 0; x < Width; x++ )
            {
                // �����Ǵ� Ÿ�ϸ��� �߾��� (0, 0, 0)�� ��ġ
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

        clone.name = "Tile";                        // Tile ������Ʈ�� �̸��� "Tile"�� ����
        clone.transform.SetParent(transform);       // Tilemap2D ������Ʈ�� Tile ������Ʈ�� �θ�� ����

        Tile_Edit tile = clone.GetComponent<Tile_Edit>();     // ��� ������ Ÿ��(Clone) ������Ʈ�� Tile.Setup() �޼ҵ� ȣ��
        tile.Setup(tiletype);

        TileList.Add(tile);
    }

    public MapData GetMapData()
    {
        // �ʿ� ��ġ�� ��� Ÿ���� ������ mapData.mapData �迭�� ����
        for (int i = 0; i < TileList.Count; i++)
        {
            if(TileList[i].TileType_Edit != TileType_Edit.Player)
            {
                mapData.mapData[i] = (int)TileList[i].TileType_Edit; 
            }
            // ���� ��ġ�� Ÿ���� �÷��̾���
            else
            {
                // ���� ��ġ�� Ÿ���� �� Ÿ��(Empty)�� ����
                mapData.mapData[i] = (int)TileType.Empty;

                // ���� ��ġ ������ mapData.playerPosition�� ����
                int x = (int)TileList[i].transform.position.x;
                int y = (int)TileList[i].transform.position.y;
                mapData.playerPositionl = new Vector2Int(x, y);
            }
        }

        return mapData;
    }

    public void findMapdata()
    {
        // inputFirld UI�� �Էµ� �ؽ�Ʈ ������ �ҷ��ͼ� fileName�� ����
        string fileName = inputName.text;
        // fileName�� ".json" ������ ������ �Է����ش�
        // ex) "Stage01" => "Stage01.json"
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }

        // MapDataLoader Ŭ���� �ν��Ͻ� ���� �� �޸� �Ҵ�
        MapDataLoader mapDataLoader = new MapDataLoader();

        //MapData mapData = mapDataLoader.Load(fileName);
        mapData = mapDataLoader.Load(fileName);

        LoadTilemap(mapData);
    }

    public void LoadTilemap(MapData mapdata)
    {
        // mapData ������ �������� Ÿ�� ������ �� ����
        int width = mapData.mapSize.x;
        int height = mapData.mapSize.y;

        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // ���� ���·� ��ġ�� Ÿ�ϵ��� ���� ��ܺ��� ���������� ��ȣ�� �ο�
                // 0, 1, 2, 3, 4, 5,
                //6, 7, ...
                int index = y * width + x;
               
                // �����Ǵ� Ÿ�ϸ��� �߾��� (0, 0, 0)�� ��ġ
                Vector3 position = new Vector3(-(width * 0.5f - 0.5f) + x, (height * 0.5f - 0.5f) - y);

                // Ÿ�� ����
                SpawnTile((TileType_Edit)mapData.mapData[index], position);

            }
        }
    }
}
