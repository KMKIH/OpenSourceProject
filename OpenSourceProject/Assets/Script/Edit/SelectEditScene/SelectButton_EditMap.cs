using UnityEngine;
using UnityEngine.UI;

public class SelectButton_EditMap : MonoBehaviour
{
    InputController_SelectEditMap inputController;
    Data data;
    [SerializeField] Color selectedColor;
    [SerializeField] Color normalColor;
    private void Awake()
    {
        inputController = FindObjectOfType<InputController_SelectEditMap>();
        data = GetComponent<Data>();
    }
    public void SetCurSelectEditMap()
    {
        inputController.curSelectMap = data.FileName;
        OnlyOneCanSelect();
    }
    public void OnlyOneCanSelect()
    {
        if (inputController.curSelectMap == data.FileName)
        {
            // »ö º¯°æ
            this.GetComponent<Image>().color = selectedColor;
            foreach(SelectButton_EditMap a in FindObjectsOfType<SelectButton_EditMap>())
            {
                if(!a.name.Equals(inputController.curSelectMap))
                    a.GetComponent<Image>().color = normalColor;
            }
        }
    }
}
