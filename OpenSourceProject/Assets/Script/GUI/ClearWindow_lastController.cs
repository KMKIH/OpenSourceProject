using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearWindow_lastController : MonoBehaviour
{

    private static ClearWindow_lastController instance;
    public static ClearWindow_lastController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<ClearWindow_lastController>();
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
    private static ClearWindow_lastController Create()
    {
        return Instantiate(Resources.Load<ClearWindow_lastController>("Clear Window_last"));
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
        GameController.SaveData(numOfStar);
        LoadingSceneController.Instance.LoadSceneWithFade("StageSelect");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        GameController.SaveData(numOfStar);
        LoadingSceneController.Instance.LoadSceneWithFade(SceneLoader.GetSceneName());
    }
    #endregion
}

