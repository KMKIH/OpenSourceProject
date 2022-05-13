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
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    public void LoadStageSelectScene()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("StageSelect");
    }
    [SerializeField] GameObject editWindow;
    public void OpenEditWindow()
    {
        editWindow.SetActive(true);
    }
}
