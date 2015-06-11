using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveShakingMeditationScript : CustomTextScript {

    //game logic control
    //public bool canContinue;

    public GameObject Avatar;
    protected Animator Animator;

    //audio control
    AudioSource music;
    private float startTime;
    private bool triggeredMusic;
    private ICollection<int> heartRateSamples;
    private double heartRateBaseline;

	// Use this for initialization
	public override void Start () {
       
        this.music = this.GetComponent<AudioSource>();
        this.triggeredMusic = false;

        this.Avatar.SetActive(true);
        this.Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, -300, -50);
        this.Animator = this.Avatar.GetComponentInChildren<Animator>();
	    this.heartRateSamples = new List<int>();

        this.SensorManager.StartNewActivity();
	}
	
    // Update is called once per frame
    public override void Update()
    {
        this.heartRateSamples.Add(SensorManager.HeartRate);
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
                    //calculate the heartRateBaseline for the relaxed state
                    this.heartRateBaseline = heartRateSamples.Average();
                    //reset the heartRateSamples to collect the heart rate data when dancing
                    heartRateSamples = new List<int>();
                    this.Animator.SetBool(AnimatorControlerHashIDs.Instance.DancingExerciseBool,true);
                    moreInstructions = false;
                    PlayOneShot();
                    
                    triggeredMusic = true;
                }
            }
        }
        if (this.triggeredMusic && !isPlaying())
        {
            Logger.Instance.LogInformation("Music stopped.");
            UIManagerScript.EnableSkipping();
            var currentHeartRate = heartRateSamples.Average();
            this.triggeredMusic = false;
            this.music.Stop();
            this.Animator.SetBool(AnimatorControlerHashIDs.Instance.DancingExerciseBool, false);

            //if the current average hearthRate is 1.4 times bigger than the baseline, give 20 points to the user
            if (currentHeartRate > this.heartRateBaseline*1.4)
            {
                SessionManager.PlayerScore += 20;
            }
            else if (currentHeartRate > this.heartRateBaseline)
            {
                //the heart rate increased just a bit, give the user 12 points
                SessionManager.PlayerScore += 12;
            }
            else
            {
                //the user didn't do anything, just give him 4 points
                SessionManager.PlayerScore += 4;
            }
        }
    }

    public override void OnGUI()
    {
        //draw the instructions text
        GUI.Label(this.Configurations.HalfTextArea, this.currentInstructions, this.Configurations.BoxFormat);
    }

    private void PlayOneShot()
    {
        Logger.Instance.LogInformation("Music starting.");
        music.PlayDelayed(2);
        startTime = Time.time;
    }

    public bool isPlaying()
    {
        if ((Time.time - startTime) >= (music.clip.length-1))
            return false;
        return true;
    }
}
