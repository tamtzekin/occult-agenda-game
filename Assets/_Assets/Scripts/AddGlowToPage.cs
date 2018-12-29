using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGlowToPage : MonoBehaviour
{
	[SerializeField] Material glowMaterial;

	public void AddGlow()
	{
		Main.Instance.gameState.contentManager.paperImage.material = glowMaterial;
	}

	public void RemoveGlow()
	{
		Main.Instance.gameState.contentManager.paperImage.material = null;
	}
}
