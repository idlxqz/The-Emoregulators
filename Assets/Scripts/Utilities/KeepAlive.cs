using UnityEngine;
using System.Collections;

public class KeepAlive : MonoBehaviour {

	void Start() {
	}

	void Awake() {
		//Keep the object alive between scenes
		DontDestroyOnLoad(transform.gameObject);
	}

}