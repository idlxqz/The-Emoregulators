using System.Linq;
using UnityEngine;
using System.Collections;

public class BreathingRegulationScript : CustomTextScript {

    //public bool finished;
    public GameObject Avatar;
    protected Animator Animator;
    private AnimatorControlerHashIDs HashIDs;


	// Use this for initialization
	public override void Start ()
	{
	    //base.Start();
	    var sessionManager = GameObject.Find("SessionManager");
 
	    this.HashIDs = sessionManager.GetComponent<AnimatorControlerHashIDs>();
        this.Avatar.SetActive(true);
	    this.Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector3(400,-300,0);
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
                    this.Animator.SetTrigger(this.HashIDs.BreathingExerciseTrigger);
                }
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
        //else if ((Time.time - finalWaitStart) >= secondsToCloseSession)
        //    finished = true;
    }
}
