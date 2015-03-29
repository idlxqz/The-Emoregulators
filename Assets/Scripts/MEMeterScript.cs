using UnityEngine;
using System.Collections;

public class MEMeterScript : MonoBehaviour {

	public bool isActive;
	public bool isHidden;
	
	public bool requireAction;
	
	// Use this for initialization
	void Start () {
		isActive = false;
		requireAction = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown () {
		if (!isHidden) {
			requireAction = true;
		}
	}
}
