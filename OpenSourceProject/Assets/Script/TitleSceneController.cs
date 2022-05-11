using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneController : MonoBehaviour
{
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }
    public void LoadStageSelectScene()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("StageSelect");
    }
    public void LoadEditScene()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("Edit");
    }
}
