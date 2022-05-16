using UnityEngine;
using System.IO;

public class StageData : MonoBehaviour
{
    // 저장용
    public bool[] isClear;
    public int[] numOfStar;
    
    // 실 사용
    public static int TotalStageNum { private set; get; } = 10;
    public static StageInfo[] stageArr = new StageInfo[TotalStageNum];

    public string selectStageName = null;

    StageData stageData;
    private void Awake()
    {
        stageData = new StageData();
        try
        {
            LoadStageDataToJson();
            Load();
        }
        catch
        {
            Init();
        }
    }
    #region [Save&Load Stage Data]
    public void SaveStageData()
    {
        Save();
        SaveStageDataToJson();
    }

    void Save()
    {
        for (int i = 0; i < TotalStageNum; i++)
        {
            stageData.isClear[i] = !(stageArr[i].numOfStar == 0);
            stageData.numOfStar[i] = stageArr[i].numOfStar;
        }
    }
    void Load()
    {
        for (int i = 0; i < TotalStageNum; i++)
        {
            stageArr[i].stageCondition = stageData.isClear[i] ? StageCondition.Active : StageCondition.Inactive;
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
    void LoadStageDataToJson()
    {
        string path = Application.dataPath + "/StageData.json";
        string jsonData = File.ReadAllText(path);
        stageData = JsonUtility.FromJson<StageData>(jsonData);
    }
    #endregion
}
