using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectSceneController : MonoBehaviour
{
    public void GoTitle()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("Title");
    }
}
