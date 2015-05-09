using UnityEngine;
using System.Collections;

public class ActiveShakingMeditationScript : MonoBehaviour {

    //game logic control
    public bool finished;
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
	void Start () {
        currentStage = SessionStage.AvatarDancing;
        music = this.GetComponent<AudioSource>();
        triggeredMusic = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentStage == SessionStage.AvatarDancing)
        {
            //TODO: show avatar dancing also
            if (!triggeredMusic)
            {
                PlayOneShot();
                triggeredMusic = true;
            }
            else if (!isPlaying() && triggeredMusic)
            {
                currentStage = SessionStage.UserDancing;
            }
            
        }
        else if (currentStage == SessionStage.UserDancing)
        {
            //TODO: show proper animation
        }
        
	}

    void OnGUI() {
        if (currentStage == SessionStage.UserDancing)
            UIManagerScript.EnableSkipping();
    }

    private void PlayOneShot()
    {
        music.Play();
        startTime = Time.time;
    }

    public bool isPlaying()
    {
        if ((Time.time - startTime) >= music.clip.length)
            return false;
        return true;
    }
}
