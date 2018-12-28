using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityAddToPaper : SanityEffect
{
	[SerializeField]
	GameObject prefabObject;

	GameObject addedObject;

	public override void ApplyEffect()
	{
		RectTransform rectTransform = Main.Instance.gameState.contentParent.transform.GetChild(Main.Instance.gameState.contentParent.transform.childCount - 1) as RectTransform;
		Debug.Log(rectTransform.anchoredPosition);
		Vector3 objectPosition = rectTransform.anchoredPosition;
		Debug.Log(objectPosition);
		objectPosition.x = 0;
		objectPosition.y = objectPosition.y - 200;
		Debug.Log(objectPosition);

		addedObject = Instantiate<GameObject>(prefabObject, Main.Instance.gameState.contentManager.paperImage.transform, false);
		addedObject.GetComponent<RectTransform>().anchoredPosition = objectPosition;
		//Debug.Log(addedObject.transform.position);
		//addedObject.transform.position = objectPosition;
		//Debug.Log(addedObject.transform.position);
	}

	public override void RemoveEffect()
	{
		addedObject.SetActive(false);
	}
}
 