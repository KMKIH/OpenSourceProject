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
            return instance;
        }
    }
    private static ClearWindowController Create()
    {
        return Instantiate(Resources.Load<ClearWindowController>("ClearWindow"));
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
        stageName.text = SceneLoader.GetSceneName();
    }

    [SerializeField] Image star0;
    [SerializeField] Image star1;
    [SerializeField] Image star2;
    public void Clear(int cur, int goal)
    {
        if (cur == goal)            star2.color = new Color(1, 1, 1);
        if (cur >= goal / 3 * 2)    star1.color = new Color(1, 1, 1);
        if (cur >= goal / 3)        star0.color = new Color(1, 1, 1);
    }
    #region Scene 이동
    public void GoTitle()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("Title");
    }
    public void Restart()
    {
        LoadingSceneController.Instance.LoadSceneWithFade(SceneLoader.GetSceneName());
    }
    public void NextStage()
    {
        string nextSceneName = SceneLoader.GetSceneName();
        int stageNum = nextSceneName[nextSceneName.Length - 1] - '0';
        nextSceneName = nextSceneName.Substring(0, nextSceneName.Length - 1) + (stageNum + 1).ToString();

        LoadingSceneController.Instance.LoadSceneWithFade(nextSceneName);
    }
    #endregion
}
