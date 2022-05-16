using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    [SerializeField] RectTransform content;
    [SerializeField] float interval;
    private float curPosx;
    private float goalPosx;
    [SerializeField] float movespeed = 0.00000001f;

    private void Start()
    {
        curPosx = content.anchoredPosition.x;
    }
    public void Right()
    {
        curPosx = content.anchoredPosition.x;

        
        if (curPosx <= -(((int)(StageDataController.TotalStageNum/4)+1)*1300)) return;
        // Set goal
        goalPosx = ((int)(curPosx / 1300) - 1) * 1300;
        if (goalPosx <= -(((int)(StageDataController.TotalStageNum / 4) + 1) * 1300)) goalPosx = -(((int)(StageDataController.TotalStageNum / 4) + 1) * 1300);
        StartCoroutine(ScrollContent(true));
    }
    public void Left()
    {
        curPosx = content.anchoredPosition.x;
        if (curPosx >= 0) return;
        // Set goal
        goalPosx = ((int)(curPosx / 1300) + 1) * 1300;
        if (goalPosx >= 0) goalPosx = 0;
        StartCoroutine(ScrollContent(false));
    }
    IEnumerator ScrollContent(bool goRight)
    {
        if (goRight)
        {
            while (content.anchoredPosition.x <= goalPosx - 0.1f)
            {
                content.localPosition = Vector2.Lerp(content.anchoredPosition, Vector2.right * goalPosx, Time.deltaTime * movespeed);
                if (Input.GetMouseButton(0)) yield break;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        else
        {
            while (content.anchoredPosition.x >= goalPosx - 0.1f)
            {
                content.localPosition = Vector2.Lerp(content.anchoredPosition, Vector2.right * goalPosx, Time.deltaTime * movespeed);
                if (Input.GetMouseButton(0)) yield break;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        content.anchoredPosition = Vector2.right * goalPosx;
    }
}
