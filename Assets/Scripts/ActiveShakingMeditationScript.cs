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
            if (!isPlaying() && !triggeredMusic)
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
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Click to proceed"))
                finished = true;
        }
        
	}

    void OnGUI() { 
        if (currentStage == SessionStage.UserDancing)
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Click to proceed"))
                        finished = true;
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
