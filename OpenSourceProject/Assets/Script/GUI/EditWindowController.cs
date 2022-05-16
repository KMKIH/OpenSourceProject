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

    // 마우스로 클릭하면 해당하는 파일의 이름을 가져옴
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
        
        // 파일 삭제
    }
    public void ClickCreate()
    {
        
    }
































    ///*
    // * viewport - 스크롤 할 컨텐츠 중 보여지는 부분
    // *          - Mask => 특정 모양 만큼만 보여지게 하는 기능
    // * contents - 스크롤 할 전체 컨텐츠 모음
    // *          - contents의 자식으로 여러 Image가 들어갈예정
    // * 
    // * contentPref      - contents의 자식으로 들어갈 컨텐츠의 틀 Prefab
    // * 
    // * (contents와 content간의 간격)
    // * horizontalMargin - 좌우 각각의 n의 여백을 만듦
    // *                  - 즉, 5 입력시 좌,우 5씩해서 10의 여백이 생김
    // * verticalMargin   - 위와 동일
    // * 
    // * gapBetweenContent- contents의 자식들, content들 간의 간격
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
    //    // 첫 위치 설정 (x축은 stretch를 해놔서 신경 안써도됨)
    //    // y = - (verticalMargin + idx * (높이 + gapBetweenContent))
    //    for (int i = 0; i < achievements.Count; i++)
    //    {
    //        achievementUIs.Add(Instantiate(contentFramePref, contents.transform));
    //        // 위치 설정
    //        RectTransform ui = achievementUIs[i].GetComponent<RectTransform>();
    //        //Pos Y 설정
    //        float posY = -(verticalMargin + i * (ui.sizeDelta.y + gapBetweenContent));
    //        ui.anchoredPosition = new Vector2(0, posY);
    //    }
    //}



}
