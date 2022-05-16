using UnityEngine;

public class StageSelectSceneController : MonoBehaviour
{
    public void GoTitle()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("Title");
    }
}
