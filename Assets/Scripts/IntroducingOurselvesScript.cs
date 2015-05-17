using UnityEngine;

public class IntroducingOurselvesScript : CustomTextScript
{
	// Use this for initialization
	public override void Start ()
	{
	    this.Configurations = GameObject.FindObjectOfType<StandardConfigurations>();
	}

    public override void OnGUI()
    {
        //draw the instructions text
        GUI.Label(this.Configurations.HalfTextArea, this.currentInstructions, this.Configurations.InstructionsFormat);
    }
    public override void Update()
    {
        
    }

    public void Setup(string _instructions)
    {
        UIManagerScript.DisableSkipping();
        this.currentInstructions = _instructions;
    }
    
}
