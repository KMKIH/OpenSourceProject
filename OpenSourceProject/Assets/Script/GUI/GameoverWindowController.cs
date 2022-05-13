using UnityEngine;
using TMPro;
public class GameoverWindowController : MonoBehaviour
{
    private static GameoverWindowController instance;
    public static GameoverWindowController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<GameoverWindowController>();
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
    private static GameoverWindowController Create()
    {
        return Instantiate(Resources.Load<GameoverWindowController>("GameoverWindow"));
    }

    [SerializeField] TMP_Text stageName;
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // stage �̸� ����
        stageName.text = SceneLoader.GetSceneName();
    }

    #region Scene �̵�
    public void GoTitle()
    {
        LoadingSceneController.Instance.LoadSceneWithFade("Title");
    }
    public void Restart()
    {
        LoadingSceneController.Instance.LoadSceneWithFade(SceneLoader.GetSceneName());
    }
    #endregion
}
