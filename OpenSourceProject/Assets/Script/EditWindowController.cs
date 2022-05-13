using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditWindowController : MonoBehaviour
{
    /*
     * viewport - ��ũ�� �� ������ �� �������� �κ�
     *          - Mask => Ư�� ��� ��ŭ�� �������� �ϴ� ���
     * contents - ��ũ�� �� ��ü ������ ����
     *          - contents�� �ڽ����� ���� Image�� ������
     * 
     * contentPref      - contents�� �ڽ����� �� �������� Ʋ Prefab
     * 
     * (contents�� content���� ����)
     * horizontalMargin - �¿� ������ n�� ������ ����
     *                  - ��, 5 �Է½� ��,�� 5���ؼ� 10�� ������ ����
     * verticalMargin   - ���� ����
     * 
     * gapBetweenContent- contents�� �ڽĵ�, content�� ���� ����
     */
    private AchievementManager achievementManager;
    private List<Achievement> achievements;
    private List<GameObject> achievementUIs;

    GameObject viewport;
    GameObject contents;

    [SerializeField] GameObject contentFramePref;

    [SerializeField] float horizontalMargin;
    [SerializeField] float verticalMargin;
    [SerializeField] float gapBetweenContent;


    /*
     * GameManeger Obj�� ���� AchievementManager��ũ��Ʈ�� ����
     */
    private void Awake()
    {
        achievementManager = FindObjectOfType<AchievementManager>();
        achievements = achievementManager.achievements;
        achievementUIs = new List<GameObject>();

        viewport = transform.GetChild(2).gameObject;
        if (viewport.name != "Viewport")
        {
            viewport = GameObject.Find("Viewport");
        }
        contents = viewport.transform.GetChild(0).gameObject;
    }
    private void Start()
    {
        CollocateUI();
    }
    void CollocateUI()
    {
        contents.GetComponent<RectTransform>().sizeDelta
            = new Vector2(0, 100 * achievements.Count + gapBetweenContent * (achievements.Count - 1) + verticalMargin * 2);
        // ù ��ġ ���� (x���� stretch�� �س��� �Ű� �Ƚᵵ��)
        // y = - (verticalMargin + idx * (���� + gapBetweenContent))
        for (int i = 0; i < achievements.Count; i++)
        {
            achievementUIs.Add(Instantiate(contentFramePref, contents.transform));
            // ��ġ ����
            RectTransform ui = achievementUIs[i].GetComponent<RectTransform>();
            //Pos Y ����
            float posY = -(verticalMargin + i * (ui.sizeDelta.y + gapBetweenContent));
            ui.anchoredPosition = new Vector2(0, posY);

            achievementUIs[i].transform.GetChild(0).GetComponent<Text>().text = achievements[i].getName();
            achievementUIs[i].transform.GetChild(1).GetComponent<Text>().text = achievements[i].getExplain();
        }
    }
}
