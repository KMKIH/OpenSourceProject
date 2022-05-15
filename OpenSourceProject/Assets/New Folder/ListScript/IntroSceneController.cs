using UnityEngine;
using System.IO;

public class IntroSceneController : MonoBehaviour
{
    public void Awake()
    {
        // ���� ���� �����찡 ��Ŀ�� �Ǿ� ���� �ʾƵ� ������ ���� �ǵ��� ����
        Application.runInBackground = true;

        // ������ ���� �������� �� �׻� ���������� 1���� �����ϵ��� ����
        PlayerPrefs.SetInt("StageIndex", 0);

        // StreamingAssets ���� ������ ���´�
        DirectoryInfo directory = new DirectoryInfo(Application.streamingAssetsPath);

        // GetFiles()�� ������ �ִ� ��� ���� ������ ���´�
        // �� ������ .json ���ϰ� .meta ������ �ֱ� ������ /2�� �� ���� �� ������ ����
        //StageController.maxStageCount = directory.GetFiles().Length / 2;
    }

    private void Update()
    {
        // �ƹ�Ű�� ������
        if (Input.anyKeyDown)
        {
            // "Stage" ������ �� ����
            //SceneLoader.LoadScene("Stage");
        }
    }
}
