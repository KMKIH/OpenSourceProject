using UnityEngine;
using System.IO;
using System;

public class DirectoryController : MonoBehaviour
{
    private DirectoryInfo m_Directory;
    private DirectorySpawner directorySpawner;

    private void Awake()
    {
        // ���α׷��� �ֻ�ܿ� Ȱ��ȭ ���°� �ƴϾ �÷���
        Application.runInBackground = true;

        directorySpawner = GetComponent<DirectorySpawner>();
        directorySpawner.Setup();

        //���� ��θ� ������Ʈ�� MapData�� ����
        string MapDataFolder = "Assets/Mapdata/";
        m_Directory = new DirectoryInfo(MapDataFolder);

        // ������ �����ϴ� ���丮 ����
        UpdateDirectory(m_Directory);
    }

    public void UpdateDirectory(DirectoryInfo directory)
    {
        // ���� ��� ����
        m_Directory = directory;
      
        // ���� ������ �����ϴ� ��� ������ �̸� ���
        directorySpawner.UpdateDirectory(directory);

    }
    private void Update()
    {
        if(SceneLoader.GetSceneName().Equals("Editor"))
            UpdateDirectory(m_Directory);
    }
}
