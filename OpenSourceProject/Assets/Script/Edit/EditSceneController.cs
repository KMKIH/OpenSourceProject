using UnityEngine;

public class EditSceneController : MonoBehaviour
{
    public static string curSelectMap = null;
    public void sceneReset()
    {
        LoadingSceneController.Instance.LoadSceneWithFade(SceneLoader.GetSceneName());
    }
    public void goTitle()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("Title");
    }
    public void Play()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("Stage_Edit");
    }
}
