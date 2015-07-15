using UnityEngine;
using System.Collections;

public class DebugScript : MonoBehaviour 
{
	//Debug variables
	public bool ActivateForwardDebug;
	public GameObject ForwardButton;
	public GameSession GameSession;

	// Use this for initialization
	void Start () 
	{
		//Change ForwardButton to an instance of the actual button (so that it doesn't die between scenes
		ForwardButton = Instantiate (ForwardButton, ForwardButton.transform.localPosition, ForwardButton.transform.rotation) as GameObject;
		ForwardButton.transform.parent = null;
		ForwardButton.AddComponent<KeepAlive> ();
		ForwardButton.SetActive (false);

	}

	void OnLevelWasLoaded(int level) {
		//Enable or disable forward debug
		if(ActivateForwardDebug)
		{
			GameObject forwardButtonClone = Instantiate (ForwardButton, ForwardButton.transform.position, ForwardButton.transform.rotation) as GameObject;
			forwardButtonClone.transform.parent = GameObject.Find ("Canvas").transform;
			forwardButtonClone.SetActive(true);
			forwardButtonClone.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		}
	}

}
