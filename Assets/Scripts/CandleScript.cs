using UnityEngine;
using System.Collections;

public class CandleScript : MonoBehaviour {
	
	public Sprite onCandleSprite;
	public Sprite offCandleSprite;

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

	void OnMouseOver () {
		if (!isHidden) {
			requireAction = true;
		}
	}
}
