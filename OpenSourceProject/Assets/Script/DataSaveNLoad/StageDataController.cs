using UnityEngine;
using System.IO;

public class StageDataController : MonoBehaviour
{
    public static int TotalStageNum { private set; get; } = 7;
    public static StageInfo[] stageArr = new StageInfo[TotalStageNum];
    // 선택한 스테이지 이름
    public string selectStageName = null;
    // 현재 스테이지 Index
    private static int stageIdx = 0;
    public static int StageIdx
    {
        set
        {
            stageIdx = value;
        }
        get => stageIdx;
    }
    // Stage Data
    StageData stageData = new StageData();

    private void Awake()
    {
        LoadStageData();
    }

    #region [Save&Load Stage Data]
    public void SaveStageData()
    {
        Save();
        SaveStageDataToJson();
        Debug.Log("저장 성공");
    }
    public void LoadStageData()
    {
        try
        {
            LoadStageDataFromJson();
            Load();
            Debug.Log("로드 성공");
        }
        catch
        {
            Init();
            Debug.Log("데이터가 없어 초기화");
        }
    }
    void Save()
    {
        for (int i = 0; i < TotalStageNum; i++)
        {
            stageData.numOfStar[i] = stageArr[i].numOfStar;
        }
    }
    void Load()
    {
        for (int i = 0; i < TotalStageNum; i++)
        {
            stageArr[i] = new StageInfo();
            if (i == 0) stageArr[i].stageCondition = StageCondition.Active;
            else
            {
                stageArr[i].stageCondition 
                    = stageData.numOfStar[i-1] > 0 ? 
                    StageCondition.Active : StageCondition.Inactive;
            }
            stageArr[i].numOfStar = stageData.numOfStar[i];
        }
    }
    void Init()
    {
        for (int i = 0; i < TotalStageNum; i++)
        {
            stageArr[i] = new StageInfo();
            if (i == 0)
            {
                stageArr[i].stageCondition = StageCondition.Active;
            }
            else
            {
                stageArr[i].stageCondition = StageCondition.Inactive;
            }
            stageArr[i].numOfStar = 0;
        }
    }
    [ContextMenu("To Json Data")]
    void SaveStageDataToJson()
    {
        string jsonData = JsonUtility.ToJson(stageData, true);
        string path = Application.dataPath + "/StageData.json";

        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("From Json Data")]
    void LoadStageDataFromJson()
    {
        string path = Application.dataPath + "/StageData.json";
        string jsonData = File.ReadAllText(path);

        stageData = JsonUtility.FromJson<StageData>(jsonData);
    }
    #endregion
}
