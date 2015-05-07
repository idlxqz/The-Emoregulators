using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using System.Collections;

public class BreathingRegulationScript : MonoBehaviour {

    public bool finished;
    public GameObject Avatar;
    protected Animator Animator;
    private AnimatorControlerHashIDs HashIDs;


	// Use this for initialization
	void Start ()
	{
	    var sessionManager = GameObject.Find("SessionManager");
	    this.HashIDs = sessionManager.GetComponent<AnimatorControlerHashIDs>();
	    this.Animator = this.Avatar.GetComponent<Animator>();

        //TODO: play breathing regulation animations and finish
        this.Avatar.SetActive(true);
        this.Animator.SetTrigger(this.HashIDs.BreathingExerciseTrigger);

	}
	
	// Update is called once per frame
	void Update () {
	    
        
        //var breathingParameter = this.Animator.parameters.FirstOrDefault(p => p.name.Equals("BreathingRegulation"));
        //this.Animator.S
        ////check if the waiting time is elapsed
        //if ((Time.time - finalWaitStart) >= secondsToCloseSession)
        //    finished = true;

	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Click to proceed"))
            finished = true;
    }
}
