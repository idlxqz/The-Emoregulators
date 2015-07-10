using UnityEngine;

public class IntroducingOurselvesScript : CustomTextScript
{
    public override void OnGUI()
    {
        //draw the instructions text
        GUI.Label(this.Configurations.HalfTextArea, this.currentInstructions, this.Configurations.BoxFormat);
    }

    public override void Update()
    {
    }
    

    public void Setup(string description, string _instructions)
    {
        this.Name = description;
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
