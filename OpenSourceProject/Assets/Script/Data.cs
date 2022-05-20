using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Data : MonoBehaviour
{
    private TextMeshProUGUI textDataName;

    private string fileName;
    public string FileName => fileName;

    private int maxFileNameLength = 25;

    public void Setup(string fileName)
    {
        textDataName = GetComponentInChildren<TextMeshProUGUI>();

        this.fileName = fileName;
        
        textDataName.text = this.fileName;

        if(fileName.Length >= maxFileNameLength)
        {
            textDataName.text = fileName.Substring(0, maxFileNameLength);
            textDataName.text += "..";
        }
    }
}
