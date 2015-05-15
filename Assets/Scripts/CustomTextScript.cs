using UnityEngine;

public class CustomTextScript : MonoBehaviour {

    //logic control
    public bool finished;
    protected float timeStart;
    public float MinimumWaitTime;

    //memeter and instructions text areas definition
    public Rect instructionsArea;

    //instruction control
    public System.Action setupNextPhase;
    protected System.Action ExecuteOnFinish;
    public string[] instructions;
    public int delayBetweenInstructions;
    protected bool moreInstructions;
    protected int instructionsPointer;
    protected string currentInstructions;

    //instructions format
    public GUIStyle instructionsFormat;

    //positioning
    public int lateralOffset;

	// Use this for initialization
	public virtual void Start ()
	{
	    this.MinimumWaitTime = 5;

        instructionsArea.x = lateralOffset;
        instructionsArea.y = Screen.height / 5;
        instructionsArea.width = Screen.width - 2 * lateralOffset ;
        instructionsArea.height = Screen.height - (Screen.height / 5) - (Screen.height / 6);
	}
	
	// Update is called once per frame
	public virtual void Update () {
        //check if the waiting time is elapsed
        if (this.moreInstructions)
        {
            if ((Time.time - this.timeStart) >= this.delayBetweenInstructions)
            {
                this.timeStart = Time.time;
                instructionsPointer++;
                currentInstructions += "\n\n" + instructions[instructionsPointer];
                //check if there are more to show
                if (instructions.Length == instructionsPointer + 1)
                {
                    moreInstructions = false;
                    UIManagerScript.EnableSkipping();
                    this.OnFinish();
                }
            }
        }
        else if ((Time.time - this.timeStart) >= this.MinimumWaitTime)
        {
            UIManagerScript.EnableSkipping();
            this.OnFinish();
        }
	}

    protected void OnFinish()
    {
        if (this.ExecuteOnFinish != null)
        {
            this.ExecuteOnFinish();
            this.ExecuteOnFinish = null;
        }
    }

    public virtual void OnGUI()
    {
        //draw the instructions text
        GUI.Label(this.instructionsArea, this.currentInstructions, this.instructionsFormat);
    }

    public virtual void Setup(System.Action nextPhaseSetup,  string[] newInstructions)
    {
        finished = false;
        setupNextPhase = nextPhaseSetup;
        instructions = newInstructions;
        if (instructions.Length > 1)
        {
            moreInstructions = true;
        }
        else
        {
            moreInstructions = false;
        }
        currentInstructions = instructions[0];
        instructionsPointer = 0;
        timeStart = Time.time;
    }

    public void Setup(System.Action nextPhaseSetup, System.Action executeOnFinish, string[] newInstructions)
    {
        this.ExecuteOnFinish = executeOnFinish;
        this.Setup(nextPhaseSetup, newInstructions);
    }

    public void Setup(System.Action nextPhaseSetup, string newInstructions)
    {
        Setup(nextPhaseSetup, new string[] { newInstructions });
    }

    public void Setup(System.Action nextPhaseSetup, System.Action executeOnFinish, string newInstructions)
    {
        this.ExecuteOnFinish = executeOnFinish;
        this.Setup(nextPhaseSetup, newInstructions);
    }

}
