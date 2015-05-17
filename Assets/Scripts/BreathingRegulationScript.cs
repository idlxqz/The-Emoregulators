using UnityEngine;

public class BreathingRegulationScript : CustomTextScript {

    //public bool finished;
    public GameObject Avatar;
    protected Animator Animator;

	// Use this for initialization
	public override void Start ()
	{
	    this.Configurations = GameObject.FindObjectOfType<StandardConfigurations>();
        this.Avatar.SetActive(true);
	    this.Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector3(400,-300,-100);
	    this.Animator = this.Avatar.GetComponentInChildren<Animator>();
        
        //UIManagerScript.EnableSkipping();
	}

    public override void Update()
    {
        //check if the waiting time is elapsed
        if (moreInstructions)
        {
            if ((Time.time - timeStart) >= delayBetweenInstructions)
            {
                timeStart = Time.time;
                instructionsPointer++;
                currentInstructions += "\n\n" + instructions[instructionsPointer];

                if (instructionsPointer == 1)
                {
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
        GUI.Label(this.Configurations.HalfTextArea, this.currentInstructions, this.Configurations.InstructionsFormat);
    }
    
}
