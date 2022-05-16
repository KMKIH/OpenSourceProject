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
        FindObjectOfType<StageDataController>().selectStageName = this.name;
        LoadingSceneController.Instance.LoadSceneWithFade("Stage");
    }
}
