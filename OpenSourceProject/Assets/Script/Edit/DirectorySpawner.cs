using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DirectorySpawner : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textDirectoryName;      // 현재 경로 이름을 나타내는 텍스트
    [SerializeField]
    private Scrollbar verticalScrollbar;            // 파일들이 배치되는 Scrollview의 스크롤바

    [SerializeField]
    private GameObject panelDataPrefab;             // 파일을 알려주는 프리팹

    [SerializeField]
    private Transform parentContent;                //생성되는 TextUI가 저장되는 부모 오브젝트

    public List<Data> fileList;                    // 파일 리스트

    public void Setup()
    {
        fileList = new List<Data>();
    }

    public void UpdateDirectory(DirectoryInfo directory)
    {
       // 기존에 생성되어 있는 데이터 정보 삭제
       
       for(int i = 0; i < fileList.Count; i++)
        {
            Destroy(fileList[i].gameObject);
        }
       fileList.Clear();
       

        // 파일 UI 생성
        foreach (FileInfo file in directory.GetFiles())
        {
            if (file.Name.Contains(".meta") == false)
            {
                SpawnData(file.Name);
            }
        }

        // 정렬
        fileList.Sort((a,b) => a.FileName.CompareTo(b.FileName));

        for(int i = 0; i < fileList.Count; i++)
        {
            fileList[i].transform.SetSiblingIndex(i);
        }
    }

    public void SpawnData(string fileName)
    {
        GameObject clone = Instantiate(panelDataPrefab);

        // 생성한 Panel UI를 Content 자식으로 배치하고, 크기를 1로 설정
        clone.transform.SetParent(parentContent);   
        clone.transform.localPosition = Vector3.one;
        clone.name = fileName;

        Data data = clone.GetComponent<Data>();
        data.Setup(fileName);

        fileList.Add(data);
    }

}
