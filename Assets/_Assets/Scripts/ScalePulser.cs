using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePulser : MonoBehaviour
{
	public float timer = 0;
	public float speed = 1;
	public AnimationCurve pulseCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(0.5f, 1.0f), new Keyframe(1f, 0.0f));

	void Update()
	{
		timer += speed * Time.deltaTime;
		float newScale = Mathf.Lerp(1.0f, 1.05f, pulseCurve.Evaluate(timer % 1));
		transform.localScale = new Vector3(newScale, newScale, 1f);
	}
}
