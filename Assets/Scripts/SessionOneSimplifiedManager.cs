using System.Diagnostics;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class SessionOneSimplifiedManager : SessionManager
{

    public Text maleButtonText;
    public Text femaleButtonText;

	// Use this for initialization
	protected override void StartLogic ()
	{
	    this.sessionTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session1Title);
	    this.sessionSubTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session1SubTitle);

        maleButtonText.text = GlobalizationService.Instance.Globalize(GlobalizationService.MaleButton);
        femaleButtonText.text = GlobalizationService.Instance.Globalize(GlobalizationService.FemaleButton); 

        System.Action nextPhase  = () => {
            hideInterface = false;
            currentState = SessionState.OpeningA;
        };
        customTitleScript.Setup(nextPhase, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningTitle));

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
                activityName = GlobalizationService.Instance.Globalize(GlobalizationService.OpeningTitle);
                log.LogInformation("Starting Opening Screen A.");
                
                //prepare custom text
                setupNextPhaseCustomText = () => {
                    log.LogInformation("Ended Opening Screen A.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.OpeningB;
                };

		        customText.Setup(setupNextPhaseCustomText, Constants.TextTimeToDisplay,GlobalizationService.Instance.Globalize(GlobalizationService.OpeningAText));
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
                    currentState = SessionState.OpeningF;
                };

                customText.Setup(setupNextPhaseCustomText, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningEText));

                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.OpeningF:
                log.LogInformation("Starting Opening Screen F.");

                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    log.LogInformation("Ended Opening Screen F.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.IntroducingOurselvesTitle;
                };

                customText.Setup(setupNextPhaseCustomText, Constants.TextTimeToDisplay, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningFText));

                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.IntroducingOurselvesTitle:
                log.LogInformation("Starting Introducing Ourselves Title");

                //prepare custom title
                setupNextPhaseCustomTitle = () =>
                {
                    currentState = SessionState.IntroducingOurselves;
                    log.LogInformation("Ended introducing ourselves Title.");
                    activityName = GlobalizationService.Instance.Globalize(GlobalizationService.IntroducingOurselvesTitle);
                    introducingOurselves.SetActive(true);
                    proceed = false;
                    nameInputField.ActivateInputField();
                    nameInputField.Select();
                };
                customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.IntroducingOurselvesTitle));
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
                    currentState = SessionState.IBoxIntroductionTitle;
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
            case SessionState.IBoxIntroductionTitle:
                log.LogInformation("Started IBox Introduction Title");
                //prepare custom title
                setupNextPhaseCustomTitle = () =>
                {
                    log.LogInformation("Ended IBox Introduction Title");
                    //start memeter introduction
                    log.LogInformation("Started IBox introduction.");
                    activityName = GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionTitle);
                    ibox.enabled = true;
                    currentState = SessionState.IBoxIntroduction;
                };
                customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionTitle));
                currentState = SessionState.CustomTitle;
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
