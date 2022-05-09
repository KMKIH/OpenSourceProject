using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    private Button btn;
    private Stage stage;
    private void Awake()
    {
        btn = GetComponentInChildren<Button>();
        stage = GetComponent<Stage>();
    }
    private void Start()
    {
        if (stage.stageCondition == StageCondition.Inactive) return;
        btn.onClick.AddListener(LoadStage);
    }
    private void LoadStage()
    {
        LoadingSceneController.Instance.LoadSceneWithFade(this.name);
    }
}
