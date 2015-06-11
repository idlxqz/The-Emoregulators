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
        
        Logger.Instance.LogInformation("User started Application");
	    
		Application.LoadLevel ("SessionZero");
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

    public void ContinueSessionZero()
    {
        SessionZeroManager session0Manager = GameObject.Find("SessionManager").GetComponent<SessionZeroManager>();
        if (session0Manager != null)
        {
            session0Manager.Continue = true;
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
