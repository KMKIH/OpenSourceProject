using UnityEngine;

[System.Serializable]
public class MapData
{
    public string fileName;
    public Vector2Int mapSize;
    public int[] mapData;

    public Vector2Int playerPosition;
}
