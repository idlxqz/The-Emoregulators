using UnityEngine;
using System.Collections;

public class IBoxScript : MonoBehaviour {

	public Sprite onBoxSprite;
	public Sprite offBoxSprite;

	public bool isActive;
	public bool isHidden;

	public bool requireAction;

	// Use this for initialization
	void Start () {
		isActive = false;
		isHidden = true;
		requireAction = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
