using UnityEngine;

public class StageController_Edit : MonoBehaviour
{

    [SerializeField] private Tilemap2D tilemap2D;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CameraController_Player cameraController;
    [SerializeField] private StageUI stageUI;
    private void Awake()
    {
        MapDataLoader_Stage mapDataLoader = new MapDataLoader_Stage();

        // �ش��ϴ� ���������� �ҷ�����
        string curStage = EditSceneController.curSelectMap;
        MapData mapData = mapDataLoader.Load_Edit(curStage);

        tilemap2D.GenerateTilemap(mapData);

        playerController.Setup(mapData.playerPosition, mapData.mapSize.x, mapData.mapSize.y);

        cameraController.Setup(mapData.mapSize.x, mapData.mapSize.y);
        
        stageUI.UpdateTextStage(curStage);
    }

    public void GoEdit()
    {
        Time.timeScale = 1f;
        LoadingSceneController.Instance.LoadSceneWithFade("Editor");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        LoadingSceneController.Instance.LoadSceneWithFade(SceneLoader.GetSceneName());
    }

}