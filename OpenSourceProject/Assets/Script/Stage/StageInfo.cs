using UnityEngine;
public enum StageCondition
{
    Active, Inactive
}
public class StageInfo : MonoBehaviour
{
    public StageCondition stageCondition;
    public int numOfStar = 0;
}
