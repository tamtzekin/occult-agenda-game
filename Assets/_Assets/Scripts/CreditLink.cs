using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditLink : MonoBehaviour
{
	[SerializeField]
	private string websiteLink;

	public void OpenLinkInBrowser()
	{
		Application.OpenURL(websiteLink);
	}
}
