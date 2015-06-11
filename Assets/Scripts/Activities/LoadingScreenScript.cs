using UnityEngine;

public class LoadingScreenScript : Activity {

    //logic control
    private float startTime;
    public bool finished;
    public float loadingScreenDuration;
    public System.Action setupNextPhase;
    public GUIStyle TitleFormat;
    public GUIStyle LoadingFormat;
    public Texture2D LoadingBarTexture;
    private float completionRate;

    
	// Use this for initialization
	public override void Start ()
	{
	    this.Description = "LoadingScreen";
	    this.loadingScreenDuration = 3;
	    this.finished = false;
        this.startTime = Time.time;
        this.SensorManager.StartNewActivity();
	    this.completionRate = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    this.completionRate = (Time.time - this.startTime)/this.loadingScreenDuration;
        //check if the waiting time is elapsed
        if (this.completionRate>=1.0f)
            this.finished = true;
	}

    void OnGUI()
    {
        //draw the instructions text
        ///GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.width), );
        GUI.Label(this.Configurations.FullTextArea, "The Emoregulators", this.TitleFormat);
        GUI.Box(new Rect(this.Configurations.FullTextArea.x + 100, Screen.height / 2 + 100, (this.Configurations.FullTextArea.width - 200), 40), "");
        var width = (this.Configurations.FullTextArea.width-200)*this.completionRate;
        Debug.Log(width);
        GUI.DrawTexture(new Rect(this.Configurations.FullTextArea.x+100+1, Screen.height / 2 + 101, width-2, 40-2),this.LoadingBarTexture);
        GUI.Label(new Rect(this.Configurations.FullTextArea.x,Screen.height/2 + 140, this.Configurations.FullTextArea.width,40), "Loading session...", this.LoadingFormat);
    }
}
