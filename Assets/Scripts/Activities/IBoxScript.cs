using UnityEngine;

public class IBoxScript : Activity {

    //logic control
    private float finalWaitStart;
    public float secondsToCloseSession;

    //ibox display
    public Texture2D ibox;
    public Texture2D heartRate;

    //memeter and instructions text areas definition
    public Rect iboxArea;
    public Rect heartRateArea;

    //instruction control
    public string instructions;
    public int instructionsIboxSpacing;
    public float scale;

	// Use this for initialization
	public override void Start () {
        finalWaitStart = Time.time;

        //dynamic positioning
	    var xcenterOfMediaArea = this.Configurations.FullTextArea.x + this.Configurations.FullTextArea.width*0.8f -
	                            this.ibox.width*this.scale/2;
	    var ycenterOfMediaArea = this.Configurations.FullTextArea.y + this.Configurations.FullTextArea.height/2 -
	                             this.ibox.height*this.scale/2;
        this.iboxArea = new Rect(xcenterOfMediaArea, ycenterOfMediaArea, this.ibox.width*this.scale, this.ibox.height*this.scale);

        this.heartRateArea = new Rect(this.Configurations.HalfTextArea.x + this.Configurations.HalfTextArea.width/2-25, this.Configurations.HalfTextArea.y + this.Configurations.HalfTextArea.height/2 + 125, 50, 50);

        //immediatelly enable the skipping functionality 
        UIManagerScript.EnableSkipping();

        this.SensorManager.StartNewActivity();
	}
	
	// Update is called once per frame
	void Update () {
        //check if the waiting time is elapsed
        if ((Time.time - finalWaitStart) >= secondsToCloseSession)
            this.CanContinue = true;
	}

    void OnGUI()
    {
        //draw the instructions text
        GUI.Label(this.Configurations.HalfTextArea, instructions, this.Configurations.BoxFormat);
        //GUI.DrawTexture(this.heartRateArea, this.heartRate);
        //draw the memeter frame
        GUI.DrawTexture(iboxArea, ibox);
    }
}
