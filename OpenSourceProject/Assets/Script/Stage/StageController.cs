using UnityEngine;

public class StageController : MonoBehaviour
{

    [SerializeField] private Tilemap2D tilemap2D;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CameraController_Player cameraController;
    [SerializeField] private StageUI stageUI;


    
    private void Awake()
    {
        MapDataLoader_Stage mapDataLoader = new MapDataLoader_Stage();

        // 해당하는 스테이지를 불러오기
        string curStage = FindObjectOfType<StageData>().selectStageName;
        MapData mapData = mapDataLoader.Load(curStage);

        tilemap2D.GenerateTilemap(mapData);

        playerController.Setup(mapData.playerPosition, mapData.mapSize.y);

        cameraController.Setup(mapData.mapSize.x, mapData.mapSize.y);

        stageUI.UpdateTextStage(curStage);
    }
    /*
    public void GameClear()
    {
        //Debug.Log("Game Clear");

        int index = PlayerPrefs.GetInt("StageIndex");

        if(index < TotalStageNum - 1)
        {
            index++;
            PlayerPrefs.SetInt("StageIndex", index);

            SceneLoader.LoadScene();
        }
        else
        {
            SceneLoader.LoadScene("Intro");
        }
    }
    */
    
}