using UnityEngine;
using System.Collections;

public class KeepAlive : MonoBehaviour {

	//public bool KeepActive;

	void Start() {
	}

	void Awake() {
		//Keep the object alive between scenes
		DontDestroyOnLoad (transform.gameObject);
	}

}