using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartButton ()
	{
	    SessionManager.UserCode = GameObject.Find("UserCode").GetComponent<InputField>().text;
	    var sensorManager = GameObject.FindObjectOfType<SensorManager>();
	    if (sensorManager.BaselineHeartRateSamples.Any())
	    {
            Logger.Instance.LogInformation("BaselineHR (avg,min,max): " + sensorManager.BaselineHeartRateSamples.Average() + ", " + sensorManager.BaselineHeartRateSamples.Min() + ", " + sensorManager.BaselineHeartRateSamples.Max());
	    }
	    if (sensorManager.BaselineEDASamples.Any())
	    {
            Logger.Instance.LogInformation("BaselineEDA (avg,min,max): " + sensorManager.BaselineEDASamples.Average() + ", " + sensorManager.BaselineEDASamples.Min() + ", " + sensorManager.BaselineEDASamples.Max());
	        
	    }
        Logger.Instance.LogInformation("User started Application");
		Application.LoadLevel ("SessionOneSimplified");
	}

    private void Globalize()
    {
        GameObject.Find("StartButton").GetComponentInChildren<Text>().text = GlobalizationService.Instance.Globalize(GlobalizationService.StartButton);
        GameObject.Find("QuitButton").GetComponentInChildren<Text>().text = GlobalizationService.Instance.Globalize(GlobalizationService.QuitButton);
        GameObject.Find("Placeholder").GetComponentInChildren<Text>().text = GlobalizationService.Instance.Globalize(GlobalizationService.UserCodePlaceholder);
    }

	public void QuitButton () {
		Application.Quit ();
	}

    public void ContinueButton()
    {
        SessionManager sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        if (sessionManager != null)
        {
            sessionManager.Continue();
        }
    }

    public void EnglishButton()
    {
        GlobalizationService.Instance.CurrentLanguage = SystemLanguage.English;
        this.Globalize();
    }

    public void ItalianButton()
    {
        GlobalizationService.Instance.CurrentLanguage = SystemLanguage.Italian;
        this.Globalize();
    }

    public static void EnableSkipping()
    {
        GameObject buttonContainer = GameObject.Find("SkipButton");

        //a skipping button was found we can activate it
        if (buttonContainer != null)
        {
            GameObject button = buttonContainer.transform.FindChild("Button").gameObject;
            button.SetActive(true);
        }
    }

    public static void DisableSkipping()
    {
        GameObject buttonContainer = GameObject.Find("SkipButton");

        //a skipping button was found we can deactivate it
        if (buttonContainer != null)
        {
            GameObject button = buttonContainer.transform.FindChild("Button").gameObject;
            button.SetActive(false);
        }
    }
}
