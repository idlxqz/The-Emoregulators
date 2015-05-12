using UnityEngine;

public class ProgressiveMuscleRelaxationScript : CustomTextScript
{

    //public bool finished;
    public GameObject Avatar;
    protected Animator Animator;
    public int AnimationId;
    private bool ExpectedMuscleTense;
    private bool ExpectedMuscleRelaxed;

    // Use this for initialization
    public override void Start()
    {
        this.ExpectedMuscleTense = false;
        this.ExpectedMuscleRelaxed = false;
        this.Avatar.SetActive(true);
        this.Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector3(400, -300, 0);
        this.Animator = this.Avatar.GetComponentInChildren<Animator>();

        //UIManagerScript.EnableSkipping();
    }

    public override void Update()
    {
        if (this.ExpectedMuscleTense && SessionManager.IsMuscleActive)
        {
            this.ExpectedMuscleTense = false;
            //after the muscle being tense, we expected to become relaxed
            this.ExpectedMuscleRelaxed = true;
            SessionManager.PlayerScore += 2;
        }
        else if (this.ExpectedMuscleRelaxed && !SessionManager.IsMuscleActive)
        {
            this.ExpectedMuscleRelaxed = false;
            SessionManager.PlayerScore += 2;
        }

        //check if the waiting time is elapsed
        if (moreInstructions)
        {
            if ((Time.time - timeStart) >= delayBetweenInstructions)
            {
                timeStart = Time.time;
                instructionsPointer++;
                currentInstructions += "\n\n" + instructions[instructionsPointer];

                if (instructionsPointer == 1 || instructionsPointer == 4)
                {
                    this.Animator.SetTrigger(this.AnimationId);
                    this.ExpectedMuscleTense = true;
                }
                //check if there are more to show
                if (instructions.Length == instructionsPointer + 1)
                {

                    finalWaitStart = Time.time;
                    moreInstructions = false;
                    //let the user skip from now on
                    UIManagerScript.EnableSkipping();
                    SessionManager.PlayerScore += 2;
                }
            }
        }
    }

    public override void Setup(System.Action nextPhaseSetup, float timeToDisplay, string[] newInstructions)
    {
        base.Setup(nextPhaseSetup, timeToDisplay, newInstructions);
        this.ExpectedMuscleRelaxed = false;
        this.ExpectedMuscleTense = false;
    }   
}
