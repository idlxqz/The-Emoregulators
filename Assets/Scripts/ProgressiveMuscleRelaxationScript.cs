using UnityEngine;
using System.Collections;

public class ProgressiveMuscleRelaxationScript : MonoBehaviour {

    //game logic control
    public bool finished;
    public enum SessionStage
    {
        CloseBall,
        ExtendArms,
        Snail
    }
    public SessionStage currentStage;

    //logging
    public Logger log;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //TODO: animations
	}

    void OnGUI()
    {
        switch (currentStage)
        {
            case SessionStage.CloseBall:
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Click to proceed - CB"))
                    currentStage = SessionStage.ExtendArms;
                break;
            case SessionStage.ExtendArms:
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Click to proceed - EA"))
                    currentStage = SessionStage.Snail;
                break;
            case SessionStage.Snail:
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Click to proceed - Sn"))
                {
                    finished = true;
                    UIManagerScript.EnableSkipping();
                }
                break;
            default:
                log.LogInformation("Unknown stage in progressive muscle relaxation.");
                break;
        }
    }
}
