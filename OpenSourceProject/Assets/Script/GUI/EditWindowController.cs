using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class EditWindowController : MonoBehaviour
{
    MapDataLoader_Edit mapDataLoader;
    private void Awake()
    {
        mapDataLoader = new MapDataLoader_Edit();
    }
    public void LoadMapData(MapData mapData)
    {
        
        // tilemap2D.GenerateTilemap(mapData);
    }

    // ���콺�� Ŭ���ϸ� �ش��ϴ� ������ �̸��� ������
    private string fileName = null;
    public void ClickModify()
    {
        if (fileName == null) return;

        LoadMapData(mapDataLoader.Load(fileName));
        
        // window off
    }
    public void ClickDelete()
    {
        if (fileName == null) return;
        
        // ���� ����
    }
    public void ClickCreate()
    {
        
    }
































    ///*
    // * viewport - ��ũ�� �� ������ �� �������� �κ�
    // *          - Mask => Ư�� ��� ��ŭ�� �������� �ϴ� ���
    // * contents - ��ũ�� �� ��ü ������ ����
    // *          - contents�� �ڽ����� ���� Image�� ������
    // * 
    // * contentPref      - contents�� �ڽ����� �� �������� Ʋ Prefab
    // * 
    // * (contents�� content���� ����)
    // * horizontalMargin - �¿� ������ n�� ������ ����
    // *                  - ��, 5 �Է½� ��,�� 5���ؼ� 10�� ������ ����
    // * verticalMargin   - ���� ����
    // * 
    // * gapBetweenContent- contents�� �ڽĵ�, content�� ���� ����
    // */
    //private List<Achievement> achievements;
    //private List<GameObject> achievementUIs;

    //GameObject viewport;
    //GameObject contents;

    //[SerializeField] GameObject contentFramePref;

    //[SerializeField] float horizontalMargin;
    //[SerializeField] float verticalMargin;
    //[SerializeField] float gapBetweenContent;

    //private void Start()
    //{
    //    CollocateUI();
    //}
    //void CollocateUI()
    //{
    //    contents.GetComponent<RectTransform>().sizeDelta
    //        = new Vector2(0, 100 * achievements.Count + gapBetweenContent * (achievements.Count - 1) + verticalMargin * 2);
    //    // ù ��ġ ���� (x���� stretch�� �س��� �Ű� �Ƚᵵ��)
    //    // y = - (verticalMargin + idx * (���� + gapBetweenContent))
    //    for (int i = 0; i < achievements.Count; i++)
    //    {
    //        achievementUIs.Add(Instantiate(contentFramePref, contents.transform));
    //        // ��ġ ����
    //        RectTransform ui = achievementUIs[i].GetComponent<RectTransform>();
    //        //Pos Y ����
    //        float posY = -(verticalMargin + i * (ui.sizeDelta.y + gapBetweenContent));
    //        ui.anchoredPosition = new Vector2(0, posY);
    //    }
    //}



}
