using UnityEngine;

public class LoadingScreenScript : Activity {

    //logic control
    private float startTime;
    public bool finished;
    public float loadingScreenDuration;
    public System.Action setupNextPhase;
    public GUIStyle LoadingFormat;

    
	// Use this for initialization
	public override void Start ()
	{
	    this.loadingScreenDuration = 3;
	    this.finished = false;
        this.startTime = Time.time;
        this.SensorManager.StartNewActivity();
	}
	
	// Update is called once per frame
	void Update () {
        //check if the waiting time is elapsed
        if ((Time.time - startTime) >= this.loadingScreenDuration)
            this.finished = true;
	}

    void OnGUI()
    {
        //draw the instructions text
        GUI.Label(this.Configurations.FullTextArea, "Loading session ...", this.LoadingFormat);
    }
}
