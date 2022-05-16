using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageSelectUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject stageSelectionPrefab;

    [SerializeField]
    private Sprite[] stageNameBackSprites; // 0 - active, 1 - clear, 2 - inactive

    private StageInfo[] stages;
    private void Awake()
    {
        stages = new StageInfo[StageDataController.TotalStageNum];
        GenerateStageSelection();
        SetStage();
    }
    private void SetStage()
    {
        // 저장된 데이터에 따라 Stage관리
        for (int idx = 0; idx < StageDataController.TotalStageNum; idx++)
        {
            /*
             * 텍스트 배경 설정 & StageCondition 설정
             */

            // 자식 찾기
            Image nameBack = null;
            Image noEntry = null;
            foreach (var a in stages[idx].GetComponentsInChildren<Image>())
            {
                if (a.name.Equals("ImageStageName"))
                {
                    nameBack = a;
                }
                else if (a.name.Equals("ImageNoEntry"))
                {
                    noEntry = a;
                }
            }
            // 이미지 설정
            if (idx == 0)
            {
                nameBack.sprite = stageNameBackSprites[0]; // active
                if (stages[idx].numOfStar > 0)
                    nameBack.sprite = stageNameBackSprites[1]; // clear
            }
            else
            {
                nameBack.sprite = stageNameBackSprites[2]; // inactive
                noEntry.enabled = true;
                if (stages[idx - 1].numOfStar > 0)
                {
                    nameBack.sprite = stageNameBackSprites[0]; // active
                    noEntry.enabled = false;
                }
                if (stages[idx].numOfStar > 0)
                {
                    nameBack.sprite = stageNameBackSprites[1]; // clear
                    noEntry.enabled = false;
                }
            }

            // 오브젝트 star 설정
            for (int i = 0; i < stages[idx].numOfStar; i++)
            {
                stages[idx].GetComponentsInChildren<Image>()[i].enabled = true;
            }
        }
    }
    private void GenerateStageSelection()
    {
        for (int i = 0; i < StageDataController.TotalStageNum; i++)
        {
            SpawnStageSelection(i + 1);
        }
    }
    private void SpawnStageSelection(int stageNum)
    {
        // 오브젝트 생성
        GameObject clone = Instantiate(stageSelectionPrefab);

        clone.GetComponent<StageInfo>().stageCondition = StageDataController.stageArr[stageNum - 1].stageCondition;
        clone.GetComponent<StageInfo>().numOfStar = StageDataController.stageArr[stageNum-1].numOfStar;

        stages[stageNum - 1] = clone.GetComponent<StageInfo>();

        clone.name = "Stage" + stageNum;
        clone.transform.SetParent(transform);

        StageInfo stage = clone.GetComponent<StageInfo>();
        // 오브젝트 텍스트 설정
        clone.GetComponentInChildren<TMP_Text>().text = clone.name;
    }
}
