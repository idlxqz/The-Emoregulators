using Assets.Scripts;
using UnityEngine;

public class ProgressiveMuscleRelaxationScript : CustomTextScript
{

    //public bool canContinue;
    public GameObject Avatar;
    protected Animator Animator;
    public int AnimationId;
    private bool ExpectedMuscleTense;
    private bool ExpectedMuscleRelaxed;
    public int ExpectedMuscle;
    public Texture2D GrassBackground;
    public Texture2D SnailBackground;
    public Texture2D SunBackground;
    public Texture2D SandBackground;
    private Sprite SelectedSprite;

    // Use this for initialization
    public override void Start()
    {
        this.ExpectedMuscleTense = false;
        this.ExpectedMuscleRelaxed = false;
        this.Avatar.SetActive(true);
        this.Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, -300, -50);
        this.Animator = this.Avatar.GetComponentInChildren<Animator>();
        //UIManagerScript.EnableSkipping();
    }

    public override void Update()
    {
        if (this.ExpectedMuscleTense)
        {
            if ((this.ExpectedMuscle == 1 && SensorManager.Muscle1Active) || (this.ExpectedMuscle == 2 && SensorManager.Muscle2Active))
            {
                this.ExpectedMuscleTense = false;
                //after the muscle being tense, we expected to become relaxed
                this.ExpectedMuscleRelaxed = true;
                SessionManager.PlayerScore += 1;
            }
            
        }
        else if (this.ExpectedMuscleRelaxed) 
        {
            if ((this.ExpectedMuscle == 1 && !SensorManager.Muscle1Active) || (this.ExpectedMuscle == 2 && !SensorManager.Muscle2Active))
            {
                this.ExpectedMuscleRelaxed = false;
                SessionManager.PlayerScore += 2;
            }
        }

		if(!StandardConfigurations.IsTheEmoregulatorsAssistantActive)
	        //check if the waiting time is elapsed
	        if (moreInstructions)
	        {
	            var delay = instructions[instructionsPointer+1].DelayTime;
	            if ((Time.time - timeStart) >= delay)
	            {
	                timeStart = Time.time;
	                instructionsPointer++;
	                currentInstructions += "\n\n" + instructions[instructionsPointer].Text;

	                if (instructionsPointer == 1 || instructionsPointer == 4)
	                {
	                    Logger.Instance.LogInformation("Exercise Animation Started");
	                    if (this.AnimationId == AnimatorControlerHashIDs.Instance.SnailExerciseTrigger)
	                    {
	                        //simple hack to play the alarm sound in the snail exercise
	                        GameObject.Find("Alarm2 Audio Source").GetComponent<AudioSource>().Play();
	                    }
	                    this.Animator.SetTrigger(this.AnimationId);
	                    this.ExpectedMuscleTense = true;
	                }
	                //check if there are more to show
	                if (instructions.Length == instructionsPointer + 1)
	                {
	                    moreInstructions = false;
	                    //let the user skip from now on
	                    UIManagerScript.EnableSkipping();
	                    this.OnFinish();
	                }
	            }
	        }
    }

    public override void OnGUI()
    {
        //var rect = GUILayoutUtility.GetRect(new GUIContent(this.fullInstructions), this.Configurations.BoxFormat);
        //this.Configurations.HalfTextArea.height = rect.height + 30;

        //draw the instructions text
        GUI.Label(this.Configurations.HalfTextArea, this.currentInstructions, this.Configurations.BoxFormat);
    }

    public override void Setup(string description, System.Action nextPhaseSetup, Instruction[] newInstructions)
    {
        base.Setup(description, nextPhaseSetup, newInstructions);
        this.ExpectedMuscleRelaxed = false;
        this.ExpectedMuscleTense = false;
    }

	public void Setup(string description, System.Action nextPhaseSetup, System.Action executeOnFinish)
	{
		base.Setup(description,  nextPhaseSetup, executeOnFinish);

		this.ExpectedMuscleRelaxed = false;
		this.ExpectedMuscleTense = false;
	}

    public void SetBackground(Texture2D backgroundTexture)
    {
        var cameraObject = GameObject.Find("Main Camera");
        if (cameraObject != null)
        {
            SpriteRenderer sr = cameraObject.GetComponentInChildren<SpriteRenderer>();

            if (this.SelectedSprite == null)
            {
                //the first time this method is called, it should remember the user's selected sprite for the background
                this.SelectedSprite = sr.sprite;
            }
            
            sr.sprite = Sprite.Create(backgroundTexture, new Rect(0, 0, backgroundTexture.width, backgroundTexture.height), new Vector2(0.5f, 0.5f));
        }
    }

    public void RevertToSessionBackground()
    {
        var cameraObject = GameObject.Find("Main Camera");
        if (cameraObject != null)
        {
            SpriteRenderer sr = cameraObject.GetComponentInChildren<SpriteRenderer>();
            sr.sprite = this.SelectedSprite;
        }
    }

	//TheEmoregulatorsAssistant
	public override void WriteInstruction(string instruction, bool isMultiLine)
	{
		instructionsPointer++;

		if (instructionsPointer == 1 || instructionsPointer == 4)
		{
			Logger.Instance.LogInformation("Exercise Animation Started");
			if (this.AnimationId == AnimatorControlerHashIDs.Instance.SnailExerciseTrigger)
			{
				//simple hack to play the alarm sound in the snail exercise
				GameObject.Find("Alarm2 Audio Source").GetComponent<AudioSource>().Play();
			}
			this.Animator.SetTrigger(this.AnimationId);
			this.ExpectedMuscleTense = true;
		}
		
		base.WriteInstruction(instruction,isMultiLine);
	}
}
