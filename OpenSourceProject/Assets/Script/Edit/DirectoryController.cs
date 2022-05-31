using UnityEngine;
using System.IO;
using System;

public class DirectoryController : MonoBehaviour
{
    private DirectoryInfo m_Directory;
    private DirectorySpawner directorySpawner;

    private void Awake()
    {
        // 프로그램이 최상단에 활성화 상태가 아니어도 플레이
        Application.runInBackground = true;

        directorySpawner = GetComponent<DirectorySpawner>();
        directorySpawner.Setup();

        //최초 경로를 프로젝트의 MapData로 설정
        string MapDataFolder = Application.persistentDataPath;
        m_Directory = new DirectoryInfo(MapDataFolder);

        // 폴더에 존재하는 디렉토리 생성
        UpdateDirectory(m_Directory);
    }

    public void UpdateDirectoryAuto()
    {
        UpdateDirectory(m_Directory);
    }
    public void UpdateDirectory(DirectoryInfo directory)
    {
        // 현재 경로 설정
        m_Directory = directory;

        // 현재 폴더에 존재하는 모든 폴더의 이름 출력
        directorySpawner.UpdateDirectory(directory);
    }
    public DirectoryInfo GetDirectory()
    {
        return m_Directory;
    }
}
