using UnityEngine;
using System.Collections;

public class BreathingRegulationScript : MonoBehaviour {

    public bool finished;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    //TODO: play breathing regulation animations and finish
        
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Click to proceed"))
            finished = true;
    }
}
