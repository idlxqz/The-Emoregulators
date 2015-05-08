using UnityEngine;
using System.Collections;

public class CustomTextScript : MonoBehaviour {

    //logic control
    public bool finished;
    private float timeStart;
    private float finalWaitStart;
    public float secondsToCloseSession;

    //memeter and instructions text areas definition
    Rect instructionsArea;

    //centralized logging
    public Logger log;

    //instruction control
    public System.Action setupNextPhase;
    public string[] instructions;
    public int delayBetweenInstructions;
    private bool moreInstructions;
    private int instructionsPointer;
    private string currentInstructions;

    //instructions format
    public GUIStyle instructionsFormat;

    //positioning
    public int lateralOffset;

	// Use this for initialization
	void Start () {
        instructionsArea.x = lateralOffset;
        instructionsArea.y = Screen.height / 5;
        instructionsArea.width = Screen.width - 2 * lateralOffset ;
        instructionsArea.height = Screen.height - (Screen.height / 5) - (Screen.height / 6);
	}
	
	// Update is called once per frame
	void Update () {
        //check if the waiting time is elapsed
        if (moreInstructions)
        {
            if ((Time.time - timeStart) >= delayBetweenInstructions)
            {
                timeStart = Time.time;
                instructionsPointer++;
                currentInstructions += instructions[instructionsPointer];
                //check if there are more to show
                if (instructions.Length == instructionsPointer + 1)
                {
                    finalWaitStart = Time.time;
                    moreInstructions = false;
                    //let the user skip from now on
                    UIManagerScript.EnableSkipping();
                }
            }
        }
        else if ((Time.time - finalWaitStart) >= secondsToCloseSession)
            finished = true;
	}

    void OnGUI()
    {
        //draw the instructions text
        GUI.Label(instructionsArea, currentInstructions, instructionsFormat);
    }

    public void Setup(System.Action nextPhaseSetup, float timeToDisplay,  string[] newInstructions)
    {   
        finished = false;
        secondsToCloseSession = timeToDisplay;
        setupNextPhase = nextPhaseSetup;
        instructions = newInstructions;
        if (instructions.Length > 1)
        {
            moreInstructions = true;
        }
        else
        {
            moreInstructions = false;
            finalWaitStart = Time.time;
            //let the user skip from now on
            UIManagerScript.EnableSkipping();
        }
        currentInstructions = instructions[0];
        instructionsPointer = 0;
        timeStart = Time.time;
    }

    public void Setup(System.Action nextPhaseSetup, float timeToDisplay, string newInstructions)
    {
        Setup(nextPhaseSetup, timeToDisplay, new string[] { newInstructions });
    }

}
