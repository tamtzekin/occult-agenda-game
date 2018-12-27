using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SanityEvent : SanityEffect
{
	public UnityEvent addEvent;

	public override void ApplyEffect()
	{
		Debug.Log("Sanity Effect " + name);
		addEvent.Invoke();
	}
}
