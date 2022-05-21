using UnityEngine;
using TMPro;

public class StageUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textStage;
    [SerializeField]
    private TextMeshProUGUI textCoinCount;

    public void UpdateTextStage(string stageName)
    {
        if (stageName.Contains(".json"))
        {
            stageName = stageName.Substring(0, stageName.Length - 5);
        }
        textStage.text = stageName;
    }

    public void UpdateCoinCount(int current, int max)
    {
        textCoinCount.text = $"Coin{current}/{max}";
    }

}
