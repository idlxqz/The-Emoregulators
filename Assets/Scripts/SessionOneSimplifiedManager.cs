using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionOneSimplifiedManager : SessionManager {

	// Use this for initialization
	protected override void StartLogic () {
        System.Action nextPhase  = () => {
            hideInterface = false;
            currentState = SessionState.OpeningA;
        };
        customTitleScript.Setup(nextPhase, sessionTitle);

        hideInterface = true;
        currentState = SessionState.CustomTitle;
	}
	
	// Update is called once per frame
    protected override void UpdateLogic()
    {
        System.Action setupNextPhaseCustomText;
        System.Action setupNextPhaseCustomTitle;
		//coordinate the session state
		switch (currentState) {
            case SessionState.OpeningA:
		        activityName = "Opening";
                log.LogInformation("Starting Opening Screen A.");
                
                //prepare custom text
                setupNextPhaseCustomText = () => {
                    log.LogInformation("Ended Opening Screen A.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.OpeningB;
                };

		        customText.Setup(setupNextPhaseCustomText, 
                    Constants.TextTimeToDisplay,
                    new string[] { GlobalizationService.Instance.Globalize(GlobalizationService.OpeningAText), "\n\nbla bla bla", "\n\nbla bla bla" });
                customText.enabled = true;
                currentState = SessionState.CustomText;
				
			    break;
            case SessionState.OpeningB:
                log.LogInformation("Starting Opening Screen B.");

                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    log.LogInformation("Ended Opening Screen B.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.OpeningC;
                };

                customText.Setup(setupNextPhaseCustomText, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningBText));
                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningC:
                log.LogInformation("Starting Opening Screen C.");

                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    log.LogInformation("Ended Opening Screen C.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.OpeningD;
                };

                customText.Setup(setupNextPhaseCustomText, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningCText));
                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningD:
                log.LogInformation("Starting Opening Screen D.");

                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    log.LogInformation("Ended Opening Screen D.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    customText.enabled = false;
                    currentState = SessionState.OpeningE;
                };

		        customText.Setup(setupNextPhaseCustomText, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningDText));

                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningE:
                log.LogInformation("Starting Opening Screen E.");

                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    log.LogInformation("Ended Opening Screen E.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    customText.enabled = false;
                    currentState = SessionState.OpeningF;
                };

                customText.Setup(setupNextPhaseCustomText, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningEText));

                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.OpeningF:
                log.LogInformation("Starting Opening Screen F.");

                //prepare custom title
                setupNextPhaseCustomTitle = () =>
                {
                    //prepare custom text
                    setupNextPhaseCustomText = () =>
                    {
                        log.LogInformation("Ended Opening Screen F.");
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;
                        currentState = SessionState.IntroducingOurselves;
                        log.LogInformation("Started introducing ourselves.");
                        activityName = "Introducing Ourselves";
                        introducingOurselves.SetActive(true);
                        proceed = false;
                        nameInputField.ActivateInputField();
                        nameInputField.Select();
                    };

                    customText.Setup(setupNextPhaseCustomText, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningFText));

                    customText.enabled = true;
                    currentState = SessionState.CustomText;
                };
                customTitleScript.Setup(setupNextPhaseCustomTitle, "Introducing Ourselves");
                currentState = SessionState.CustomTitle;
                break;
            case SessionState.IntroducingOurselves:
                if (proceed)
                {
                    log.LogInformation("User name: " + userName);
                    log.LogInformation("Ended introducing ourselves.");
                    //disable the introducing ourselves object
                    nameInputField.DeactivateInputField();
                    //GUIUtility.keyboardControl = 0; //ensure lose focus
                    introducingOurselves.SetActive(false);
                    proceed = false;
                    //prepare custom title
                    setupNextPhaseCustomTitle = () =>
                    {
                        //start memeter introduction
                        log.LogInformation("Started IBox introduction.");
                        activityName = "IBox 1: Introduction";
                        ibox.enabled = true;
                        currentState = SessionState.IBoxIntroduction;
                    };
                    customTitleScript.Setup(setupNextPhaseCustomTitle, "IBox 1: Introduction");
                    currentState = SessionState.CustomTitle;
                }
                else
                {
                    //set the gender specific avatar
                    if (userGender == Gender.Male)
                    {
                        maleAvatar.SetActive(true);
                        femaleAvatar.SetActive(false);
                    }
                    else
                    {
                        maleAvatar.SetActive(false);
                        femaleAvatar.SetActive(true);
                    }
                }
			    break;
            case SessionState.IBoxIntroduction:
                if (ibox.finished || canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    log.LogInformation("Ended ibox introduction Screen A.");
                    //disable the ibox and proceed to the next state
                    ibox.enabled = false;
                    displayIBox = true; //start displaying the UI icon
                    //start a custom text
                    log.LogInformation("Started ibox introduction Screen B");
               
                    //prepare custom text
                    setupNextPhaseCustomText = () => {
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;

                        log.LogInformation("Ended ibox introduction Screen B.");
                        Application.LoadLevel("SessionFourScene");
                    };
                    customText.Setup(setupNextPhaseCustomText, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionBText));
                    customText.enabled = true;
                    currentState = SessionState.CustomText;
                }
                break;
            case SessionState.CustomText:
                if (customText.finished || canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    //setup the next phase
                    customText.setupNextPhase();
                }
                break;
            case SessionState.CustomTitle:
                if (customTitleScript.finished || canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    //setup the next phase
                    customTitleScript.setupNextPhase();
                }
                break;
            
		default:
			Debug.LogError("Unknown/unhandled session state for this session.");
			break;
		}
	}

    protected override void OnGUILogic()
    {
        //check if enter pressed
        if (currentState == SessionState.IntroducingOurselves)
        {
            //check enter pressed for name input
            if (userName != "" && userName != null)
            {
                Event e = Event.current;
                if (e.keyCode == KeyCode.Return)
                    proceed = true;
            }
        }
	}
}
