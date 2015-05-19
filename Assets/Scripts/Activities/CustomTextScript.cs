using UnityEngine;

public class CustomTextScript : Activity {

    //logic control
    protected float timeStart;
    public float MinimumWaitTime;


    //instruction control
    public System.Action setupNextPhase;
    protected System.Action ExecuteOnFinish;
    public string[] instructions;
    public int delayBetweenInstructions;
    protected bool moreInstructions;
    protected int instructionsPointer;
    protected string currentInstructions;

	// Use this for initialization
	public override void Start ()
	{
        base.Start();
	    this.MinimumWaitTime = 5;
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{
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
        GUI.Label(this.Configurations.FullTextArea, this.currentInstructions, this.Configurations.InstructionsFormat);
    }

    public virtual void Setup(System.Action nextPhaseSetup,  string[] newInstructions)
    {
        this.SensorManager.StartNewActivity();

        this.CanContinue = false;
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
