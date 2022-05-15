using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;

public class MapDataSave : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputFileName;
    [SerializeField]
    private TMP_InputField inputFileName2;
    [SerializeField]
    private Tilemap2D tilemap2D;

    void Awake()
    {
        inputFileName.text = "Name.json";
    }

    public void Save()
    {
        // tilemap2D�� ����Ǿ� �ִ� MapData ������ �ҷ��´�
        // �� ũ��, �÷��̾� ĳ���� ��ġ, �ʿ� �����ϴ� Ÿ�ϵ��� ����
        MapData mapData = tilemap2D.GetMapData();

        // inputFirld UI�� �Էµ� �ؽ�Ʈ ������ �ҷ��ͼ� fileName�� ����
        string fileName = inputFileName.text;
        // fileName�� ".json" ������ ������ �Է����ش�
        // ex) "Stage01" => "Stage01.json"
        if(fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }
        // ������ ���, ���ϸ��� �ϳ��� ��ĥ �� ���
        // ���� ������Ʈ ��ġ �������� "MapData" ����
        fileName = Path.Combine("Assets/Mapdata/", fileName);

        // mapData �ν��Ͻ��� �ִ� ������ ����ȭ�ؼ� toJson ������ ���ڿ� ���·� ����
        string toJson = JsonConvert.SerializeObject(mapData, Formatting.Indented);
        // "fileName" ���Ͽ� "toJson" ������ ����
        //File.Delete(fileName);
        File.WriteAllText(fileName, toJson);
    }
    public void Delete()
    {
        // inputFirld UI�� �Էµ� �ؽ�Ʈ ������ �ҷ��ͼ� fileName�� ����
        string fileName = inputFileName2.text;
        // fileName�� ".json" ������ ������ �Է����ش�
        // ex) "Stage01" => "Stage01.json"
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }
        // ������ ���, ���ϸ��� �ϳ��� ��ĥ �� ���
        // ���� ������Ʈ ��ġ �������� "MapData" ����
        fileName = Path.Combine("Assets/Mapdata/", fileName);

        // "fileName" ���Ͽ� "toJson" ������ ����
        //File.WriteAllText(fileName, toJson);
        File.Delete(fileName);
    }
}
