using UnityEngine;

public class IntroducingOurselvesScript : CustomTextScript
{
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
        this.SensorManager.StartNewActivity();
    }

    public override void EndActivity()
    {
        var sessionManager = GameObject.FindObjectOfType<SessionManager>();
        Logger.Instance.LogInformation("Selected Avatar: " + SessionManager.userGender);
        Logger.Instance.LogInformation("Selected Avatar Name: " + sessionManager.userName);
        base.EndActivity();
    }
}
