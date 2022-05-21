using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearWindow_EditController : MonoBehaviour
{

    private static ClearWindow_EditController instance;
    public static ClearWindow_EditController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<ClearWindow_EditController>();
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
    private static ClearWindow_EditController Create()
    {
        return Instantiate(Resources.Load<ClearWindow_EditController>("Clear Window_Edit"));
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
        stageName.text = EditSceneController.curSelectMap;
        if (stageName.text.Contains(".json"))
        {
            stageName.text = stageName.text.Substring(0, stageName.text.Length - 5);
        }
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
    public void GoEditor()
    {
        Time.timeScale = 1f;
        GameController.SaveData(numOfStar);
        LoadingSceneController.Instance.LoadSceneWithFade("Editor");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        GameController.SaveData(numOfStar);
        LoadingSceneController.Instance.LoadSceneWithFade(SceneLoader.GetSceneName());
    }
    #endregion
}

