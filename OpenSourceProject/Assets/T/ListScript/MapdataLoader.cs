using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class MapDataLoader
{
    public MapData Load(string fileName)
    {
        // fileName�� ".json" ������ ������ �Է����ش�.
        // ex) "Stage01" => "Stage01.json"
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }

        // ������ ���, ���ϸ��� �ϳ��� ��ĥ �� ���
        // Application.streamingAssetsPath : ���� ����Ƽ ������Ʈ - Assets - MapData ���� ���
        fileName = Path.Combine("Assets/Mapdata/", fileName);

        // "fileName" ���Ͽ� �ִ� ������ "dataAsJson" ������ ���ڿ� ���·� ����
        string dataAsJson = File.ReadAllText(fileName);

        // ������ȭ�� �̿��� dataAsJson ������ �ִ� ���ڿ� �����͸� MapData Ŭ���� �ν��Ͻ��� ����
        MapData mapData = new MapData();
        mapData = JsonConvert.DeserializeObject<MapData>(dataAsJson);

        return mapData;
    }
}
