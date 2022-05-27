using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeController : MonoBehaviour
{
	CanvasGroup canvasGroup;
    private void OnEnable()
    {
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1.0f;
		FadeOut(2);
    }
    public void FadeIn(float fadeOutTime, System.Action nextEvent = null)
	{
		StartCoroutine(CoFadeIn(fadeOutTime, nextEvent));
	}

	public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
	{
		StartCoroutine(CoFadeOut(fadeOutTime, nextEvent));
	}

	// 투명 -> 불투명
	IEnumerator CoFadeIn(float fadeOutTime, System.Action nextEvent = null)
	{
		if (this.gameObject.activeSelf == false)
			gameObject.SetActive(true);
		while (canvasGroup.alpha < 1f)
		{
			canvasGroup.alpha += Time.deltaTime / fadeOutTime;

			if (canvasGroup.alpha >= 1f) canvasGroup.alpha = 1f;

			yield return null;
		}
		if (nextEvent != null) nextEvent();
	}

	// 불투명 -> 투명
	IEnumerator CoFadeOut(float fadeOutTime, System.Action nextEvent = null)
	{
		while (canvasGroup.alpha > 0f)
		{
			canvasGroup.alpha -= Time.deltaTime / fadeOutTime;

			if (canvasGroup.alpha <= 0f) canvasGroup.alpha = 0f;

			yield return null;
		}
		this.gameObject.SetActive(false);
		if (nextEvent != null) nextEvent();
	}
}