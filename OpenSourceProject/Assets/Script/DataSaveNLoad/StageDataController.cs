using UnityEngine;
using System.IO;

public class StageDataController : MonoBehaviour
{
    public static int TotalStageNum { private set; get; } = 7;
    public static StageInfo[] stageArr = new StageInfo[TotalStageNum];

    // ������ �������� �̸�
    public string selectStageName = null;

    // ���� �������� Index
    private static int stageIdx = 0;
    public static int StageIdx
    {
        set
        {
            stageIdx = value;
        }
        get => stageIdx;
    }

    StageData stageData;
    private void Awake()
    {
        stageData = new StageData();
        try
        {
            LoadStageDataToJson();
            Load();
            Debug.Log("�ε� ����");
        }
        catch
        {
            Init();
            Debug.Log("�����Ͱ� ���� �ʱ�ȭ");
        }

        // Set Stage Idx
        for (int i = stageArr.Length - 1; i >= 0; i--)
        {
            if (stageArr[i].numOfStar > 0)
            {
                stageIdx = i + 1;
                break;
            }
        }
    }
    #region [Save&Load Stage Data]
    public void SaveStageData()
    {
        Save();
        SaveStageDataToJson();
        Debug.Log("���� ����");
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
        string path = Application.dataPath + "/StageData_1.json";

        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("From Json Data")]
    void LoadStageDataToJson()
    {
        string path = Application.dataPath + "/StageData_1.json";
        string jsonData = File.ReadAllText(path);
        stageData = JsonUtility.FromJson<StageData>(jsonData);
    }
    #endregion
}
