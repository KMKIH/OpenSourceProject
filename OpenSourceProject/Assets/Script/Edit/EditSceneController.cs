using UnityEngine;

public class EditSceneController : MonoBehaviour
{
    public static string curSelectMap = null;

    [SerializeField] Tilemap2D_Edit tilemap;
    [SerializeField] CameraController_Edit cameraController;
    [SerializeField] MapDataSave mapDataSave;
    [SerializeField] GameObject panelMapSize;
    [SerializeField] GameObject editWindow;

    private void OnEnable()
    {
        curSelectMap = null;
    }
    public void sceneReset()
    {
        LoadingSceneController.Instance.LoadSceneWithFade(SceneLoader.GetSceneName());
    }
    public void goTitle()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("Title");
    }
    public void Edit()
    {
        if (curSelectMap != null)
        {
            tilemap.findMapdata();
            cameraController.SetupCamera();
            panelMapSize.SetActive(false);
            mapDataSave.SetInputFileName();
            editWindow.SetActive(false);
        }
    }
    public void Create()
    {
        cameraController.SetupCamera();
        mapDataSave.SetInputFileName();
        editWindow.SetActive(false);
    }
    public void Play()
    {
        if (curSelectMap != null)
            LoadingSceneController.Instance.LoadSceneWithFade("Stage_Edit");
    }
}
