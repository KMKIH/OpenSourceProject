using UnityEngine;

public class Tilemap2D : MonoBehaviour
{

    [Header("Common")]
    [SerializeField]
    private StageController stageController;
    [SerializeField]
    private StageUI stageUI;

    [Header("Tile")]
    [SerializeField]
    // private GameObject tilePrefab;
    private GameObject[] tilePrefabs;
    [SerializeField]
    private Movement2D movement2D;


    [Header("Item")]
    [SerializeField]
    private GameObject itemPrefab;


    private int maxCoinCount = 0;//
    private int currentCoinCount = 0;//

    public void GenerateTilemap(MapData mapData)
    {
        int width = mapData.mapSize.x;
        int height = mapData.mapSize.y;

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                int index = y * width + x;

                if (mapData.mapData[index] == (int)TileType.Empty)
                {
                    continue;
                }

                Vector3 position = new Vector3(-(width * 0.5f - 0.5f) + x, (height * 0.5f - 0.5f) - y);

                if (mapData.mapData[index] > (int)TileType.Empty && mapData.mapData[index] < (int)TileType.LastIndex)
                {
                    SpawnTile((TileType)mapData.mapData[index], position);

                }

                else if(mapData.mapData[index] == (int)ItemType.Coin)
                {
                    SpawnItem(position);
                }

            }
        }
        currentCoinCount = maxCoinCount;
    }

    public void SpawnTile(TileType tileType, Vector3 position)
    {
        GameObject clone = Instantiate(tilePrefabs[(int)tileType- 1], position, Quaternion.identity);

        clone.name = "Tile";
        clone.transform.SetParent(transform);

        Tile tile = clone.GetComponent<Tile>();
        tile.Setup(movement2D);

    }

    public void SpawnItem(Vector3 position)
    {
        GameObject clone = Instantiate(itemPrefab, position, Quaternion.identity);

        clone.name = "Item";
        clone.transform.SetParent(transform);

        maxCoinCount++;
        stageUI.UpdateCoinCount(currentCoinCount, maxCoinCount);
    }


    public void GetCoin(GameObject coin)
    {
        currentCoinCount--;

        stageUI.UpdateCoinCount(currentCoinCount, maxCoinCount);

        coin.GetComponent<Item>().Exit();//¿Ã∆—∆Æ
    }  
 

}//
