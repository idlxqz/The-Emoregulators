using UnityEngine;
using System.Collections;

public class IBoxScript : MonoBehaviour {

    //logic control
    public bool finished;
    private float finalWaitStart;
    public float secondsToCloseSession;

    //ibox display
    public Texture2D ibox;

    //memeter and instructions text areas definition
    public Rect iboxArea;

    //instruction control
    public string instructions;
    public int instructionsIboxSpacing;
    public float scale;

    protected StandardConfigurations Configurations;

	// Use this for initialization
	void Start () {
        finalWaitStart = Time.time;

	    this.Configurations = GameObject.FindObjectOfType<StandardConfigurations>();
        //dynamic positioning
	    var xcenterOfMediaArea = this.Configurations.FullTextArea.x + this.Configurations.FullTextArea.width*0.8f -
	                            this.ibox.width*this.scale/2;
	    var ycenterOfMediaArea = this.Configurations.FullTextArea.y + this.Configurations.FullTextArea.height/2 -
	                             this.ibox.height*this.scale/2;
        this.iboxArea = new Rect(xcenterOfMediaArea, ycenterOfMediaArea, this.ibox.width*this.scale, this.ibox.height*this.scale);

        //immediatelly enable the skipping functionality 
        UIManagerScript.EnableSkipping();
	}
	
	// Update is called once per frame
	void Update () {
        //check if the waiting time is elapsed
        if ((Time.time - finalWaitStart) >= secondsToCloseSession)
            finished = true;
	}

    void OnGUI()
    {
        //draw the instructions text
        GUI.Label(this.Configurations.HalfTextArea, instructions, this.Configurations.InstructionsFormat);
        //draw the memeter frame
        GUI.DrawTexture(iboxArea, ibox);
    }
}
