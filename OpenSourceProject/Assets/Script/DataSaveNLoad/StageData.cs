using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    public bool[] isClear;
    public int[] numOfStar;

    public StageData()
    {
        isClear = new bool[StageDataController.TotalStageNum];
        numOfStar = new int[StageDataController.TotalStageNum];
    }
}
