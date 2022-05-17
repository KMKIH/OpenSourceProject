using UnityEngine;

public class EditSceneController : MonoBehaviour
{
    public void sceneReset()
    {
        SceneLoader.LoadScene();
    }
    public void goTitle()
    {
        SceneLoader.LoadScene("Title");
    }
}
