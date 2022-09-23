// dnSpy decompiler from Assembly-CSharp.dll class: ZoomButtonCtrl
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ZoomButtonCtrl : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public void SetEnableClick(bool value)
	{
		this.enableClick = value;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!this.isAnimating && this.enableClick)
		{
			base.StartCoroutine(this.AnimateClick());
		}
	}

	private IEnumerator AnimateClick()
	{
		/*
		if (base.GetComponent<Scaler>() != null)
		{
			base.GetComponent<Scaler>().SetEnableAnimate(false);
		}
		AudioCtrl.Instance.PlayEffect(AudioCtrl.Instance.button, -1f);*/
		this.isAnimating = true;
		float t = 0f;
		Vector3 fromScale = base.transform.localScale;
		Vector3 toScale = fromScale + this.DeltaScale;
		while (t < 0.15f)
		{
			t += Time.unscaledDeltaTime;
			base.transform.localScale = Vector3.Lerp(fromScale, toScale, t / 0.15f);
			yield return null;
		}
		t = 0f;
		while (t < 0.15f)
		{
			t += Time.unscaledDeltaTime;
			base.transform.localScale = Vector3.Lerp(toScale, fromScale, t / 0.15f);
			yield return null;
		}
		if (this.onClick != null)
		{
			this.onClick.Invoke();
		}
		this.isAnimating = false;
		yield break;
	}

	private const float ZoomInDuration = 0.15f;

	private const float ZoomOutDuration = 0.15f;

	public UnityEvent onClick;

	private Vector3 DeltaScale = Vector3.one * 0.3f;

	private bool isAnimating;

	private bool enableClick = true;
}
