using UnityEngine;

public class BreathingRegulationScript : CustomTextScript {

    //public bool canContinue;
    public GameObject Avatar;
    protected Animator Animator;

	// Use this for initialization
	public override void Start ()
	{
        this.Avatar.SetActive(true);
	    this.Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector3(200,-300,-100);
	    this.Animator = this.Avatar.GetComponentInChildren<Animator>();
	}

    public override void Update()
    {
        //check if the waiting time is elapsed
        if (moreInstructions)
        {
            var delay = this.instructions[this.instructionsPointer + 1].DelayTime;

            if ((Time.time - timeStart) >= delay)
            {
                timeStart = Time.time;
                instructionsPointer++;
                currentInstructions += "\n\n" + instructions[instructionsPointer].Text;

                if (instructionsPointer == 1)
                {
                    Logger.Instance.LogInformation("Playing breathing animation");
                    this.Animator.SetTrigger(AnimatorControlerHashIDs.Instance.BreathingExerciseTrigger);
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
        //draw the instructions text
        GUI.Label(this.Configurations.HalfTextArea, this.currentInstructions, this.Configurations.BoxFormat);
    }
    
}
