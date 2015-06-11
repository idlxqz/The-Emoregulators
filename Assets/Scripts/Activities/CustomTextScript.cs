using UnityEngine;

public class CustomTextScript : Activity {

    //logic control
    protected float timeStart;
    public float MinimumWaitTime;


    //instruction control
    public System.Action setupNextPhase;
    protected System.Action ExecuteOnFinish;
    public string[] instructions;
    public int firstDelayBetweenInstructions;
    public int delayBetweenInstructions;
    protected bool moreInstructions;
    protected int instructionsPointer;
    protected string currentInstructions;

    protected override void Awake()
    {
        base.Awake();
        this.MinimumWaitTime = 5;
    }

	
	// Update is called once per frame
	public virtual void Update ()
	{
        //check if the waiting time is elapsed
        if (this.moreInstructions)
        {
            var delay = (instructionsPointer == 0 ? this.firstDelayBetweenInstructions : this.delayBetweenInstructions);
           
            if ((Time.time - this.timeStart) >= delay)
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
        this.CanContinue = true;
        if (this.ExecuteOnFinish != null)
        {
            this.ExecuteOnFinish();
            this.ExecuteOnFinish = null;
        }
    }

    public virtual void OnGUI()
    {
        //draw the instructions text
        GUI.Label(this.Configurations.FullTextArea, this.currentInstructions, this.Configurations.BoxFormat);
    }

    public virtual void Setup(string description, System.Action nextPhaseSetup,  string[] newInstructions)
    {
        this.Description = description;
        this.SensorManager.StartNewActivity();

        this.CanContinue = false;
        setupNextPhase = nextPhaseSetup;
        instructions = newInstructions;
        if (instructions.Length > 1)
        {
            moreInstructions = true;
            this.Configurations.BoxFormat.alignment = TextAnchor.UpperLeft;
        }
        else
        {
            if (this.Configurations.BoxFormat != null)
            {
                this.Configurations.BoxFormat.alignment = TextAnchor.MiddleLeft;
            }
            moreInstructions = false;
        }
        currentInstructions = instructions[0];
        instructionsPointer = 0;
        timeStart = Time.time;
    }

    public void Setup(string description, System.Action nextPhaseSetup, System.Action executeOnFinish, string[] newInstructions)
    {
        this.ExecuteOnFinish = executeOnFinish;
        this.Setup(description, nextPhaseSetup, newInstructions);
    }

    public void Setup(string description, System.Action nextPhaseSetup, string newInstructions)
    {
        Setup(description, nextPhaseSetup, new string[] { newInstructions });
    }

    public void Setup(string description, System.Action nextPhaseSetup, System.Action executeOnFinish, string newInstructions)
    {
        this.ExecuteOnFinish = executeOnFinish;
        this.Setup(description,nextPhaseSetup, newInstructions);
    }

   
}
