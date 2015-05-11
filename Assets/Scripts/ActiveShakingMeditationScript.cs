using UnityEngine;
using System.Collections;

public class ActiveShakingMeditationScript : CustomTextScript {

    //game logic control
    //public bool finished;

    public GameObject Avatar;
    protected Animator Animator;
    private AnimatorControlerHashIDs HashIDs;

    public enum SessionStage
    {
        AvatarDancing,
        UserDancing
    }
    public SessionStage currentStage;

    //audio control
    AudioSource music;
    private float startTime;
    private bool triggeredMusic;

	// Use this for initialization
	public override void Start () {
        currentStage = SessionStage.AvatarDancing;
        music = this.GetComponent<AudioSource>();
        triggeredMusic = false;
        var sessionManager = GameObject.Find("SessionManager");

        this.HashIDs = sessionManager.GetComponent<AnimatorControlerHashIDs>();
        this.Avatar.SetActive(true);
        this.Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector3(400, -300, 0);
        this.Animator = this.Avatar.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
    //void Update () {
    //    if (currentStage == SessionStage.AvatarDancing)
    //    {
    //        //TODO: show avatar dancing also
    //        if (!triggeredMusic)
    //        {
    //            PlayOneShot();
    //            triggeredMusic = true;
    //        }
    //        else if (!isPlaying() && triggeredMusic)
    //        {
    //            currentStage = SessionStage.UserDancing;
    //        }
            
    //    }
    //    else if (currentStage == SessionStage.UserDancing)
    //    {
    //        //TODO: show proper animation
    //    }
        
    //}

    // Update is called once per frame
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
                //check if there are more to show
                if (instructions.Length == instructionsPointer + 1)
                {
                    this.Animator.SetBool(this.HashIDs.DancingExerciseBool,true);
                    finalWaitStart = Time.time;
                    moreInstructions = false;
                    PlayOneShot();
                    
                    triggeredMusic = true;
                }
            }
        }
        if (triggeredMusic && !isPlaying())
        {
            music.Stop();
            this.Animator.SetBool(this.HashIDs.DancingExerciseBool, false);
        }
        //else if ((Time.time - finalWaitStart) >= secondsToCloseSession)
        //    finished = true;
    }

    public override void OnGUI() {
        base.OnGUI();
        //if (currentStage == SessionStage.UserDancing)
        if(triggeredMusic && !isPlaying())
            UIManagerScript.EnableSkipping();
    }

    private void PlayOneShot()
    {
        music.Play();
        startTime = Time.time;
    }

    public bool isPlaying()
    {
        if ((Time.time - startTime) >= (music.clip.length-30))
            return false;
        return true;
    }
}
