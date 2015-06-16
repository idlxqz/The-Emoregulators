using Assets.Scripts;
using UnityEngine;

public class CustomTextWithImage : CustomTextScript {

    //ibox display
    public Texture2D Image;
    //memeter and instructions text areas definition
    private Rect ImageArea;
    //instruction control
    public float ImageScale;
    //Label information
    private Rect ImageLabelArea;
    public GUIStyle LabelFormat;
    public bool ShowImageLabel;
    public string ImageLabel;

	// Use this for initialization
	public override void Start () {
      
        base.Start();
        //dynamic positioning
	    var xcenterOfMediaArea = this.Configurations.FullTextArea.x + this.Configurations.FullTextArea.width*0.8f -
	                            this.Image.width*this.ImageScale/2;
	    var ycenterOfMediaArea = this.Configurations.FullTextArea.y + this.Configurations.FullTextArea.height/2 -
	                             this.Image.height*this.ImageScale/2;
        this.ImageArea = new Rect(xcenterOfMediaArea, ycenterOfMediaArea, this.Image.width*this.ImageScale, this.Image.height*this.ImageScale);
        this.ImageLabelArea = new Rect(xcenterOfMediaArea, ycenterOfMediaArea + this.Image.height * this.ImageScale, this.Image.width * this.ImageScale, 50);
	}

    public override void OnGUI()
    {
        
        //var rect = GUILayoutUtility.GetRect(new GUIContent(this.fullInstructions), this.Configurations.BoxFormat);
        //this.Configurations.HalfTextArea.height = rect.height + 30;
        
        
        //draw the instructions text
        GUI.Label(this.Configurations.HalfTextArea, this.currentInstructions, this.Configurations.BoxFormat);
    
        //draw the image
        GUI.DrawTexture(this.ImageArea, this.Image);

        GUI.Label(this.ImageLabelArea, this.ImageLabel,this.LabelFormat);
    }

    public override void Setup(string description, System.Action nextPhaseSetup, Instruction[] newInstructions)
    {
        base.Setup(description, nextPhaseSetup, newInstructions);
        //dynamic positioning
        var xcenterOfMediaArea = this.Configurations.FullTextArea.x + this.Configurations.FullTextArea.width * 0.8f -
                                this.Image.width * this.ImageScale / 2;
        var ycenterOfMediaArea = this.Configurations.FullTextArea.y + this.Configurations.FullTextArea.height / 2 -
                                 this.Image.height * this.ImageScale / 2;
        this.ImageArea = new Rect(xcenterOfMediaArea, ycenterOfMediaArea, this.Image.width * this.ImageScale, this.Image.height * this.ImageScale);
        this.ImageLabelArea = new Rect(xcenterOfMediaArea, ycenterOfMediaArea + this.Image.height * this.ImageScale, this.Image.width * this.ImageScale, 50);
    }
}
