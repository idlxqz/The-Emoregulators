using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class CustomTextScript : Activity {

    //logic control
    protected float timeStart;
    public float MinimumWaitTime;


    //instruction control
    public System.Action setupNextPhase;
    protected System.Action ExecuteOnFinish;
    public Instruction[] instructions;
    protected bool moreInstructions;
    protected int instructionsPointer;
    protected string currentInstructions;
    protected string fullInstructions;


	//Unity methods
	public override void Start()
	{
		base.Start();
	}


    protected override void Awake()
    {
        base.Awake();
        this.MinimumWaitTime = 5;

		this.IsMultiLineSet = false;
		this.IsMultiLine = false;
    }

	//Getters and Setters
	public bool IsMultiLineSet { get; set; }
	public bool IsMultiLine { get; set; }

	public void SetTimeStart(float newTimeStart)
	{
		this.timeStart = newTimeStart;
	}

	public string GetCurrentInstructions()
	{
		return currentInstructions;
	}

	public void SetCurrentInstructions(string newCurrentInstructions)
	{
		currentInstructions = newCurrentInstructions;
	}

	public string GetFullInstructions()
	{
		return fullInstructions;
	}
	
	public void SetFullInstructions(string newFullInstructions)
	{
		fullInstructions = newFullInstructions;
	}

	public void SetMoreInstructions(bool isMoreInstructions)
	{
		this.moreInstructions = isMoreInstructions;
	}


	// Update is called once per frame
	public virtual void Update ()
	{
        //check if the waiting time is elapsed
       	if (this.moreInstructions)
        {
			//TheEmoregulatorsAssistant - there are always more instructions unless the agent reports otherwise
			if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				return;

            var delay = instructions[instructionsPointer + 1].DelayTime;
           
            if ((Time.time - this.timeStart) >= delay)
            {
                this.timeStart = Time.time;
                instructionsPointer++;
                currentInstructions += "\n\n" + instructions[instructionsPointer].Text;
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
        
        //var rect = GUILayoutUtility.GetRect(new GUIContent(this.fullInstructions), this.Configurations.BoxFormat);
        //this.Configurations.FullTextArea.height = rect.height + 30;    
       
        
        //draw the instructions text
        GUI.Label(this.Configurations.FullTextArea, this.currentInstructions, this.Configurations.BoxFormat);
    }

    public virtual void Setup(string description, System.Action nextPhaseSetup,  Instruction[] newInstructions)
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
            fullInstructions = newInstructions[0].Text;
            foreach (var instruction in newInstructions.Skip(1))
            {
                fullInstructions += "\n\n" + instruction.Text;
            }
        }
        else
        {
            if (this.Configurations.BoxFormat != null)
            {
                this.Configurations.BoxFormat.alignment = TextAnchor.MiddleLeft;
            }
            moreInstructions = false;
            fullInstructions = instructions[0].Text;
        }
        currentInstructions = instructions[0].Text;
        instructionsPointer = 0;
        timeStart = Time.time;
    }

    public void Setup(string description, System.Action nextPhaseSetup, System.Action executeOnFinish, Instruction[] newInstructions)
    {
        this.ExecuteOnFinish = executeOnFinish;
        this.Setup(description, nextPhaseSetup, newInstructions);
    }

    public void Setup(string description, System.Action nextPhaseSetup, string newInstructions)
    {
        Setup(description, nextPhaseSetup, new Instruction[] { new Instruction(newInstructions)});
    }

    public void Setup(string description, System.Action nextPhaseSetup, System.Action executeOnFinish, string newInstructions)
    {
        this.ExecuteOnFinish = executeOnFinish;
        this.Setup(description,nextPhaseSetup, newInstructions);
    }

	//TheEmoregulatorsAssistant CustomText Setup
	public void Setup(string description, System.Action nextPhaseSetup, System.Action executeOnFinish)
	{
		this.ExecuteOnFinish = executeOnFinish;
		this.Setup(description, nextPhaseSetup);
	}

	public void Setup(string description, System.Action nextPhaseSetup)
	{

		this.Description = description;
		this.SensorManager.StartNewActivity();
		
		this.CanContinue = false;
		setupNextPhase = nextPhaseSetup;

		this.currentInstructions = "";
		this.fullInstructions = "";
		this.moreInstructions = true;

		timeStart = Time.time;

	}

	// Define CustomText Configurations
	public void DefineConfigurationAlignment(TextAnchor alignmentType)
	{
		this.Configurations.BoxFormat.alignment = alignmentType;
	}

	public void EnableEnd()
	{
		moreInstructions = false;
		UIManagerScript.EnableSkipping();
		this.OnFinish();
	}



   
}
