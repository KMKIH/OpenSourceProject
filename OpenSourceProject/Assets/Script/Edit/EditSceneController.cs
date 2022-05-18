using UnityEngine;

public class EditSceneController : MonoBehaviour
{
    public void sceneReset()
    {
        LoadingSceneController.Instance.LoadSceneWithFade(SceneLoader.GetSceneName());
    }
    public void goTitle()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("Title");
    }
}
