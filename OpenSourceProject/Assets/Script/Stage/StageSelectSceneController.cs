using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageSelectSceneController : MonoBehaviour
{
    // 총 스테이지 갯수 static으로 or const로
    // 해당 만큼 배열을 생성 (각 원소는 별의 갯수 (0~3)의 값, 스테이지의 상태를 가짐)
    // 스테이지의 상태는 active, unactive

    [SerializeField]
    private GameObject stageSelectionPrefab;

    [SerializeField]
    private Sprite[] stageNameBackSprites; // 0 - active, 1 - clear, 2 - inactive


    private void Awake()
    {
        GenerateStageSelection();
        SetStage();
    }
    private void SetStage()
    {
        // 저장된 데이터에 따라 Stage관리
        for (int idx = 0; idx < StageController.TotalStageNum; idx++)
        {
            Stage stage = StageController.stageArr[idx].GetComponent<Stage>();
            /*
             * 텍스트 배경 설정 & StageCondition 설정
             */

            // 자식 찾기
            Image nameBack = null;
            Image noEntry = null;
            foreach (var a in StageController.stageArr[idx].GetComponentsInChildren<Image>())
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
            // 기본적으로 inactive 설정
            // 이전 항목의 별이 1개 이상인 경우 active 
            // 별이 1개 이상인 경우 clear설정
            if (idx == 0)
            {
                stage.stageCondition = StageCondition.Active;
                nameBack.sprite = stageNameBackSprites[0]; // active
                if (stage.numOfStart > 0)
                    nameBack.sprite = stageNameBackSprites[1]; // clear
            }
            else
            {
                stage.stageCondition = StageCondition.Inactive;
                nameBack.sprite = stageNameBackSprites[2]; // inactive
                noEntry.enabled = true;
                if (StageController.stageArr[idx - 1].numOfStart > 0)
                {
                    stage.stageCondition = StageCondition.Active;
                    nameBack.sprite = stageNameBackSprites[0]; // active
                }
                if (stage.numOfStart > 0)
                {
                    stage.stageCondition = StageCondition.Active;
                    nameBack.sprite = stageNameBackSprites[1]; // clear
                }
            }
            // 오브젝트 star 설정
            for (int i = 0; i < stage.numOfStart; i++)
            {
                StageController.stageArr[idx].GetComponentsInChildren<Image>()[i].enabled = true;
            }
        }
    }
    private void GenerateStageSelection()
    {
        for (int i = 0; i < StageController.TotalStageNum; i++)
        {
            SpawnStageSelection(i + 1);
        }
    }
    private void SpawnStageSelection(int stageNum)
    {
        // 오브젝트 생성
        GameObject clone = Instantiate(stageSelectionPrefab);

        clone.name = "Stage" + stageNum;
        clone.transform.SetParent(transform);

        Stage stage = clone.GetComponent<Stage>();
        // 오브젝트 텍스트 설정
        clone.GetComponentInChildren<TMP_Text>().text = clone.name;

        // 배열에 넣기
        StageController.stageArr[stageNum - 1] = stage;
    }
    public void GoTitle()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("Title");
    }
}
