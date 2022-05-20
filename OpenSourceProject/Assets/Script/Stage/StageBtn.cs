using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    private Button btn;
    private StageInfo stage;
    private void Awake()
    {
        btn = GetComponentInChildren<Button>();
        stage = GetComponent<StageInfo>();
    }
    private void Start()
    {
        if (stage.stageCondition == StageCondition.Inactive) return;
        btn.onClick.AddListener(LoadStage);
    }
    private void LoadStage()
    {
        FindObjectOfType<StageDataController>().selectStageName = stage.name;
        StageDataController.StageIdx = stage.name[stage.name.Length - 1]-'0' - 1;
        Debug.Log(StageDataController.StageIdx);
        LoadingSceneController.Instance.LoadSceneWithFade("Stage");
    }
}
