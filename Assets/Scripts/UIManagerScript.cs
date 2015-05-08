using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartButton () {
		Application.LoadLevel ("SessionOneScene");
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
