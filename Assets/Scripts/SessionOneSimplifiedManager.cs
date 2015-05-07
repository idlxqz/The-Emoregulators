using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionOneSimplifiedManager : SessionManager {

    public GameObject maleAvatar;
    public GameObject femaleAvatar;

	// Use this for initialization
	protected override void StartLogic () {
        sessionTitleStart = Time.time;
        currentState = SessionState.SessionTitle;
        hideInterface = true;
	}
	
	// Update is called once per frame
    protected override void UpdateLogic()
    {
        System.Action setupNextPhase;
		//coordinate the session state
		switch (currentState) {
            case SessionState.SessionTitle:
                if ((Time.time - sessionTitleStart) >= sessionTitleDuration)
                {
                    hideInterface = false;
                    currentState = SessionState.OpeningA;
                }
                break;
            case SessionState.OpeningA:
		        activityName = "Opening";
                log.LogInformation("Starting Opening Screen A.");
                
                //prepare custom text
                setupNextPhase = () => {
                    log.LogInformation("Ended Opening Screen A.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.OpeningB;
                };

		        customText.Setup(setupNextPhase, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningAText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
				
			    break;
            case SessionState.OpeningB:
                log.LogInformation("Starting Opening Screen B.");

                //prepare custom text
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended Opening Screen B.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.OpeningC;
                };

                customText.Setup(setupNextPhase, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningBText));
                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningC:
                log.LogInformation("Starting Opening Screen C.");

                //prepare custom text
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended Opening Screen C.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.OpeningD;
                };

                customText.Setup(setupNextPhase, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningCText));
                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningD:
                log.LogInformation("Starting Opening Screen D.");

                //prepare custom text
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended Opening Screen D.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    customText.enabled = false;
                    currentState = SessionState.OpeningE;
                };

		        customText.Setup(setupNextPhase, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningDText));

                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningE:
                log.LogInformation("Starting Opening Screen E.");

                //prepare custom text
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended Opening Screen E.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    customText.enabled = false;
                    currentState = SessionState.OpeningF;
                };

                customText.Setup(setupNextPhase, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningEText));

                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningF:
                log.LogInformation("Starting Opening Screen F.");

                //prepare custom text
                setupNextPhase = () =>
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

                customText.Setup(setupNextPhase, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningFText));

                customText.enabled = true;
                currentState = SessionState.CustomText;

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
                    //start memeter introduction
                    log.LogInformation("Started IBox introduction.");
                    activityName = "IBox 1: Introduction";
                    ibox.enabled = true;
                    currentState = SessionState.IBoxIntroduction;
                    proceed = false;
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
                if (ibox.finished)
                {
                    log.LogInformation("Ended ibox introduction Screen A.");
                    //disable the ibox and proceed to the next state
                    ibox.enabled = false;
                    displayIBox = true; //start displaying the UI icon
                    //start a custom text
                    log.LogInformation("Started ibox introduction Screen B");
               
                    //prepare custom text
                    setupNextPhase = () => {
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;

                        log.LogInformation("Ended ibox introduction Screen B.");
                        Application.LoadLevel("SessionFourScene");
                    };
                    customText.Setup(setupNextPhase, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionBText));
                    customText.enabled = true;
                    currentState = SessionState.CustomText;
                }
                break;
            case SessionState.CustomText:
                if (customText.finished)
                {
                    //setup the next phase
                    customText.setupNextPhase();
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

    public override void Continue()
    {
        //coordinate the session state
        switch (currentState)
        {

            case SessionState.CandleCeremony:
                candle.finished = true;
                break;
            case SessionState.IntroducingOurselves:
                proceed = true;
                break;
            case SessionState.MeMeter:
                memeter.finished = true;
                break;
            case SessionState.IBoxIntroduction:
                ibox.finished = true;
                break;
            case SessionState.CustomText:
                customText.finished = true;
                break;
            case SessionState.MeMeterReuse:
                memeter.finished = true;
                break;
            case SessionState.CloseSession:
                candle.finished = true;
                break;
        }
    }
}
