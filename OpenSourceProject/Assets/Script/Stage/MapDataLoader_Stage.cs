using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class MapDataLoader_Stage: MonoBehaviour
{
    public MapData Load(string fileName)
    {
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }

        fileName = Path.Combine("Assets/StageData/", fileName);

        string dataAsJson = File.ReadAllText(fileName);

        MapData mapData = new MapData();

        mapData = JsonConvert.DeserializeObject<MapData>(dataAsJson);

        return mapData;
    }
    public MapData Load_Edit(string fileName)
    {
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }

        fileName = Path.Combine("Assets/MapData/", fileName);

        string dataAsJson = File.ReadAllText(fileName);

        MapData mapData = new MapData();

        mapData = JsonConvert.DeserializeObject<MapData>(dataAsJson);

        return mapData;
    }
}
