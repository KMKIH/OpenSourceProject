using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;

public class MapDataSave : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputFileName;
    [SerializeField]
    private Tilemap2D_Edit tilemap2D;
    public void SetInputFileName()
    {
        if (EditSceneController.curSelectMap == null)
        {
            inputFileName.text = "NoName.json";
        }
        inputFileName.text = EditSceneController.curSelectMap;
    }

    [SerializeField]GameObject Image_saveComplete;
    public void Save()
    {
        // tilemap2D에 저장되어 있는 MapData 정보를 불러온다
        // 맵 크기, 플레이어 캐릭터 위치, 맵에 존재하는 타일들의 정보
        MapData mapData = tilemap2D.GetMapData();
        mapData.fileName = inputFileName.text;

        // inputFirld UI에 입력된 텍스트 정보를 불러와서 fileName에 저장
        string fileName = inputFileName.text;
        // fileName에 ".json" 문장이 없으면 입력해준다
        // ex) "Stage01" => "Stage01.json"
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }
        // 파일의 경로, 파일명을 하나로 합칠 때 사용
        // 현재 프로젝트 위치 기준으로 "MapData" 폴더
        fileName = Path.Combine(Application.persistentDataPath, fileName);

        // mapData 인스턴스에 있는 내용을 직렬화해서 toJson 변수에 문자열 형태로 저장
        string toJson = JsonConvert.SerializeObject(mapData, Formatting.Indented);
        // "fileName" 파일에 "toJson" 내용을 저장
        //File.Delete(fileName);
        File.WriteAllText(fileName, toJson);

        Image_saveComplete.SetActive(true);
    }
    public void Delete()
    {
        // inputFirld UI에 입력된 텍스트 정보를 불러와서 fileName에 저장
        string fileName = EditSceneController.curSelectMap;
        // fileName에 ".json" 문장이 없으면 입력해준다
        // ex) "Stage01" => "Stage01.json"
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }
        // 파일의 경로, 파일명을 하나로 합칠 때 사용
        // 현재 프로젝트 위치 기준으로 "MapData" 폴더
        fileName = Path.Combine(Application.persistentDataPath, fileName);

        // "fileName" 파일에 "toJson" 내용을 저장
        //File.WriteAllText(fileName, toJson);
        File.Delete(fileName);
    }
}
