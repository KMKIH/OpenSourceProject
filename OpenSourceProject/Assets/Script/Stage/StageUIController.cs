using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageUIController : MonoBehaviour
{
    // �� �������� ���� static���� or const��
    // �ش� ��ŭ �迭�� ���� (�� ���Ҵ� ���� ���� (0~3)�� ��, ���������� ���¸� ����)
    // ���������� ���´� active, unactive

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
        // ����� �����Ϳ� ���� Stage����
        for (int idx = 0; idx < StageData.TotalStageNum; idx++)
        {
            StageInfo stage = StageData.stageArr[idx].GetComponent<StageInfo>();
            /*
             * �ؽ�Ʈ ��� ���� & StageCondition ����
             */

            // �ڽ� ã��
            Image nameBack = null;
            Image noEntry = null;
            foreach (var a in StageData.stageArr[idx].GetComponentsInChildren<Image>())
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
            // �⺻������ inactive ����
            // ���� �׸��� ���� 1�� �̻��� ��� active 
            // ���� 1�� �̻��� ��� clear����
            if (idx == 0)
            {
                stage.stageCondition = StageCondition.Active;
                nameBack.sprite = stageNameBackSprites[0]; // active
                if (stage.numOfStar > 0)
                    nameBack.sprite = stageNameBackSprites[1]; // clear
            }
            else
            {
                stage.stageCondition = StageCondition.Inactive;
                nameBack.sprite = stageNameBackSprites[2]; // inactive
                noEntry.enabled = true;
                if (StageData.stageArr[idx - 1].numOfStar > 0)
                {
                    stage.stageCondition = StageCondition.Active;
                    nameBack.sprite = stageNameBackSprites[0]; // active
                }
                if (stage.numOfStar > 0)
                {
                    stage.stageCondition = StageCondition.Active;
                    nameBack.sprite = stageNameBackSprites[1]; // clear
                }
            }
            // ������Ʈ star ����
            for (int i = 0; i < stage.numOfStar; i++)
            {
                StageData.stageArr[idx].GetComponentsInChildren<Image>()[i].enabled = true;
            }
        }
    }
    private void GenerateStageSelection()
    {
        for (int i = 0; i < StageData.TotalStageNum; i++)
        {
            SpawnStageSelection(i + 1);
        }
    }
    private void SpawnStageSelection(int stageNum)
    {
        // ������Ʈ ����
        GameObject clone = Instantiate(stageSelectionPrefab);

        clone.name = "Stage" + stageNum;
        clone.transform.SetParent(transform);

        StageInfo stage = clone.GetComponent<StageInfo>();
        // ������Ʈ �ؽ�Ʈ ����
        clone.GetComponentInChildren<TMP_Text>().text = clone.name;

        // �迭�� �ֱ�
        StageData.stageArr[stageNum - 1] = stage;
    }
}
