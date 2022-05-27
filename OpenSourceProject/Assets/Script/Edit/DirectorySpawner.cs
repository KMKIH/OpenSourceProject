using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DirectorySpawner : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textDirectoryName;      // ���� ��� �̸��� ��Ÿ���� �ؽ�Ʈ
    [SerializeField]
    private Scrollbar verticalScrollbar;            // ���ϵ��� ��ġ�Ǵ� Scrollview�� ��ũ�ѹ�

    [SerializeField]
    private GameObject panelDataPrefab;             // ������ �˷��ִ� ������

    [SerializeField]
    private Transform parentContent;                //�����Ǵ� TextUI�� ����Ǵ� �θ� ������Ʈ

    public List<Data> fileList;                    // ���� ����Ʈ

    public void Setup()
    {
        fileList = new List<Data>();
    }

    public void UpdateDirectory(DirectoryInfo directory)
    {
       // ������ �����Ǿ� �ִ� ������ ���� ����
       
       for(int i = 0; i < fileList.Count; i++)
        {
            Destroy(fileList[i].gameObject);
        }
       fileList.Clear();
       

        // ���� UI ����
        foreach (FileInfo file in directory.GetFiles())
        {
            if (file.Name.Contains(".meta") == false)
            {
                SpawnData(file.Name);
            }
        }

        // ����
        fileList.Sort((a,b) => a.FileName.CompareTo(b.FileName));

        for(int i = 0; i < fileList.Count; i++)
        {
            fileList[i].transform.SetSiblingIndex(i);
        }
    }

    public void SpawnData(string fileName)
    {
        GameObject clone = Instantiate(panelDataPrefab);

        // ������ Panel UI�� Content �ڽ����� ��ġ�ϰ�, ũ�⸦ 1�� ����
        clone.transform.SetParent(parentContent);   
        clone.transform.localPosition = Vector3.one;
        clone.name = fileName;

        Data data = clone.GetComponent<Data>();
        data.Setup(fileName);

        fileList.Add(data);
    }

}
