using UnityEngine;
using UnityEngine.UI;

public class SelectButton_EditMap : MonoBehaviour
{
    Data data;
    [SerializeField] Color selectedColor;
    [SerializeField] Color normalColor;
    private void Awake()
    {
        data = GetComponent<Data>();
    }
    public void SetCurSelectEditMap()
    {
        EditSceneController.curSelectMap = data.FileName;
        OnlyOneCanSelect();
    }
    public void OnlyOneCanSelect()
    {
        if (EditSceneController.curSelectMap == data.FileName)
        {
            // »ö º¯°æ
            this.GetComponent<Image>().color = selectedColor;
            foreach(SelectButton_EditMap a in FindObjectsOfType<SelectButton_EditMap>())
            {
                if(!a.name.Equals(EditSceneController.curSelectMap))
                    a.GetComponent<Image>().color = normalColor;
            }
        }
    }
}
