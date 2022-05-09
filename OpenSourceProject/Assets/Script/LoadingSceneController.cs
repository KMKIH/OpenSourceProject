using System.Collections;
using UnityEngine;

public class LoadingSceneController : MonoBehaviour
{
    private static LoadingSceneController instance;
    public static LoadingSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<LoadingSceneController>();
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
    private static LoadingSceneController Create()
    {
        return Instantiate(Resources.Load<LoadingSceneController>("LoadingImage"));
    }
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    [SerializeField] private CanvasGroup canvasGroup;
    public void LoadSceneWithFade(string sceneName)
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadSceneProcess(sceneName));
    }
    public void FadeOut()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        StartCoroutine(Fade(false));
    }
    private IEnumerator LoadSceneProcess(string destination)
    {
        yield return StartCoroutine(Fade(true));
        SceneLoader.LoadScene(destination);
        FadeOut();
    }
    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 3f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0, 1, timer) : Mathf.Lerp(1, 0, timer);

        }
        if (!isFadeIn)
        {
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
