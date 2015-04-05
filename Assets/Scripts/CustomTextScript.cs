using UnityEngine;
using System.Collections;

public class CustomTextScript : MonoBehaviour {

    //logic control
    public bool finished;
    private float finalWaitStart;
    public float secondsToCloseSession;

    //memeter and instructions text areas definition
    public Rect instructionsArea;

    //centralized logging
    public Logger log;

    //instruction control
    public string instructions;
    public System.Action setupNextPhase;

    //instructions format
    public GUIStyle instructionsFormat;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //check if the waiting time is elapsed
        if ((Time.time - finalWaitStart) >= secondsToCloseSession)
            finished = true;
	}

    void OnGUI()
    {
        //draw the instructions text
        GUI.Label(instructionsArea, instructions, instructionsFormat);
    }

    public void Setup(System.Action nextPhaseSetup, float timeToDisplay,  string newInstructions)
    {   
        finished = false;
        secondsToCloseSession = timeToDisplay;
        setupNextPhase = nextPhaseSetup;
        instructions = newInstructions;
        finalWaitStart = Time.time;
    }
}
