using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearWindowController : MonoBehaviour
{
    private static ClearWindowController instance;
    public static ClearWindowController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<ClearWindowController>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            Time.timeScale = 0f;
            return instance;
        }
    }
    private static ClearWindowController Create()
    {
        return Instantiate(Resources.Load<ClearWindowController>("Clear Window"));
    }
    [SerializeField] TMP_Text stageName;
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // stage 이름 설정
        stageName.text = SceneLoader.GetSceneName() + (StageDataController.StageIdx + 1);
    }

    [SerializeField] Image star0;
    [SerializeField] Image star1;
    [SerializeField] Image star2;
    private int numOfStar = 0;
    public void Clear(int cur, int goal)
    {
        if (cur == goal)
        {
            star2.color = new Color(1, 1, 1);
            numOfStar++;
        }
        if (cur >= goal / 2)
        {
            star1.color = new Color(1, 1, 1);
            numOfStar++;
        }
        star0.color = new Color(1, 1, 1);
        numOfStar++;
    }
    #region Scene 이동
    public void GoStageSelect()
    {
        Time.timeScale = 1f;
        SaveData();
        LoadingSceneController.Instance.LoadSceneWithFade("StageSelect");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SaveData();
        LoadingSceneController.Instance.LoadSceneWithFade(SceneLoader.GetSceneName());
    }
    public void NextStage()
    {
        Time.timeScale = 1f;
        SaveData();

        StageDataController.StageIdx += 1;
        int stageIdx = StageDataController.StageIdx;
        FindObjectOfType<StageDataController>().selectStageName = "Stage" + (stageIdx + 1);
        LoadingSceneController.Instance.LoadSceneWithFade(SceneLoader.GetSceneName());
    }
    #endregion
    private void SaveData()
    {

        int stageIdx = StageDataController.StageIdx;
        StageDataController.stageArr[stageIdx].numOfStar = numOfStar;
        if (stageIdx < StageDataController.TotalStageNum - 1)
        {
            StageDataController.stageArr[stageIdx + 1].stageCondition = StageCondition.Active;
        }
        FindObjectOfType<StageDataController>().SaveStageData();
    }
}
