using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class ActiveShakingMeditationScript : CustomTextScript {

    //game logic control
    //public bool finished;

    public GameObject Avatar;
    protected Animator Animator;

    //audio control
    AudioSource music;
    private float startTime;
    private bool triggeredMusic;
    private List<int> heartRateSamples;
    private double heartRateBaseline;

	// Use this for initialization
	public override void Start () {
       
        music = this.GetComponent<AudioSource>();
        triggeredMusic = false;

        this.Avatar.SetActive(true);
        this.Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector3(400, -300, 0);
        this.Animator = this.Avatar.GetComponentInChildren<Animator>();
	    this.heartRateSamples = new List<int>();
	}
	
    // Update is called once per frame
    public override void Update()
    {
        this.heartRateSamples.Add(SessionManager.HeartRate);
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
                    finalWaitStart = Time.time;
                    moreInstructions = false;
                    PlayOneShot();
                    
                    triggeredMusic = true;
                }
            }
        }
        if (this.triggeredMusic && !isPlaying())
        {
            UIManagerScript.EnableSkipping();
            var currentHeartRate = heartRateSamples.Average();
            this.triggeredMusic = false;
            this.music.Stop();
            this.Animator.SetBool(AnimatorControlerHashIDs.Instance.DancingExerciseBool, false);

            //if the current average hearthRate is 1.4 times bigger than the baseline, give 10 points to the user
            if (currentHeartRate > this.heartRateBaseline*1.4)
            {
                SessionManager.PlayerScore += 10;
            }
            else if (currentHeartRate > this.heartRateBaseline)
            {
                //the heart rate increased just a bit, give the user 6 points
                SessionManager.PlayerScore += 6;
            }
            else
            {
                //the user didn't do anything, just give him 2 points
                SessionManager.PlayerScore += 2;
            }
        }
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
