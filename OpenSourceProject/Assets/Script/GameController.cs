using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public static void SaveData(int curStar)
    {
        int stageIdx = StageDataController.StageIdx;
        StageDataController.stageArr[stageIdx].numOfStar = curStar;
        if (stageIdx < StageDataController.TotalStageNum - 1)
        {
            StageDataController.stageArr[stageIdx + 1].stageCondition = StageCondition.Active;
        }
        FindObjectOfType<StageDataController>().SaveStageData();
    }
}
