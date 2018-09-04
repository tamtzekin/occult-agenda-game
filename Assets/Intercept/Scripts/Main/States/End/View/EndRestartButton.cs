using UnityEngine;
using System.Collections;

public class EndRestartButton : MonoBehaviour {

	public void OnClickRestartButton () {
		Application.LoadLevel(0);
		//Main.Instance.endState.Complete();
	}
}
