using UnityEngine;
public enum StageCondition
{
    Active, Inactive
}
public struct StageInfo
{
    StageCondition stageCondition;
    int numOfStar; // 0-3ÀÇ °ªÀ» Áö´Ô
}
public class Stage : MonoBehaviour
{
    public StageCondition stageCondition;
    public int numOfStart = 0;
}
