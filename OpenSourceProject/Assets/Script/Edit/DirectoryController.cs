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
        string MapDataFolder = Application.persistentDataPath;
        m_Directory = new DirectoryInfo(MapDataFolder);

        // ������ �����ϴ� ���丮 ����
        UpdateDirectory(m_Directory);
    }

    public void UpdateDirectoryAuto()
    {
        UpdateDirectory(m_Directory);
    }
    public void UpdateDirectory(DirectoryInfo directory)
    {
        // ���� ��� ����
        m_Directory = directory;

        // ���� ������ �����ϴ� ��� ������ �̸� ���
        directorySpawner.UpdateDirectory(directory);
    }
    public DirectoryInfo GetDirectory()
    {
        return m_Directory;
    }
}
