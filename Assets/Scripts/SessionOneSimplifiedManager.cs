using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class SessionOneSimplifiedManager : SessionManager
{
    public Text maleButtonText;
    public Text femaleButtonText;
    public GameObject HeartIcon;
    public GameObject HelpIcon;
    public GameObject MemeterIcon;


    BackgroundChooserScript backgroundChooserScript;
    private IntroducingOurselvesScript introducingOurselvesScript;

	// Use this for initialization
	protected override void StartLogic ()
	{
        backgroundChooserScript = GameObject.Find("BackgroundChooser").GetComponent<BackgroundChooserScript>();
	    this.introducingOurselvesScript = GameObject.Find("IntroducingOurselvesScript").GetComponent<IntroducingOurselvesScript>();

	    this.sessionTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session1Title);
	    this.sessionSubTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session1SubTitle);

        maleButtonText.text = GlobalizationService.Instance.Globalize(GlobalizationService.MaleButton);
        femaleButtonText.text = GlobalizationService.Instance.Globalize(GlobalizationService.FemaleButton);

        hideInterface = true;

	    System.Action openingTitlePhase = () =>
	    {
	        System.Action nextPhase = () =>
	        {
	            hideInterface = false;
	            currentState = SessionState.OpeningA;
	        };
	        customTitleScript.Setup(nextPhase, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningTitle));
	        customTitleScript.enabled = true;

	        currentState = SessionState.CustomTitle;
	    };

	    loadingScreenScript.setupNextPhase = openingTitlePhase;
	    loadingScreenScript.enabled = true;
        currentState = SessionState.LoadingScreen;

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
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended Opening Screen A.");
                    currentState = SessionState.OpeningB;
                };

		        customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningAText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
				
			    break;
            case SessionState.OpeningB:
                log.LogInformation("Starting Opening Screen B.");

                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended Opening Screen B.");
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.OpeningC;
                };

                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningBText));
                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningC:
                log.LogInformation("Starting Opening Screen C.");

                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended Opening Screen C.");            
                    currentState = SessionState.OpeningD;
                };

                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningCText));
                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningD:
                log.LogInformation("Starting Opening Screen D.");

                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended Opening Screen D.");
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.OpeningE;
                };

		        customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningDText));

                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningE:
                log.LogInformation("Starting Opening Screen E.");
                this.MemeterIcon.SetActive(true);
                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    this.MemeterIcon.SetActive(false);
                    customText.EndActivity();
                    log.LogInformation("Ended Opening Screen E.");
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.OpeningF;
                };

                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningEText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.OpeningF:
                log.LogInformation("Starting Opening Screen F.");
                this.HelpIcon.SetActive(true);
                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    this.HelpIcon.SetActive(false);
                    customText.EndActivity();
                    log.LogInformation("Ended Opening Screen F.");
                    //disable the custom text and proceed to the next 
                    currentState = SessionState.IntroducingOurselvesTitle;
                };

                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningFText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.IntroducingOurselvesTitle:
                log.LogInformation("Starting Introducing Ourselves Title");

                //prepare custom title
                setupNextPhaseCustomTitle = () =>
                {
                    log.LogInformation("Ended introducing ourselves Title.");
                    currentState = SessionState.IntroducingOurselvesA;
                };
                customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.IntroducingOurselvesTitle));
                currentState = SessionState.CustomTitle;
		        break;
            case SessionState.IntroducingOurselvesA:
		        canSkip = false;
                activityName = GlobalizationService.Instance.Globalize(GlobalizationService.IntroducingOurselvesTitle);
                log.LogInformation("Starting IntroducingOurselvesA.");
                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    log.LogInformation("Ended IntroducingOurselvesA");
                    customText.EndActivity();
                    log.LogInformation("Started introducing Ourselves Background Selection");
                    this.showHelpButton = true;
                    this.helpTextContent =
                        GlobalizationService.Instance.Globalize(GlobalizationService.IntroducingOurselvesBackgroundText);
                    currentState = SessionState.IntroducingOurselvesBackground;
                    backgroundChooserScript.backgroundSetter = SetBackground;
                    backgroundChooserScript.enabled = true;
                    UIManagerScript.EnableSkipping();
                };

                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.IntroducingOurselvesBackgroundText));
		        customText.enabled = true;
		        currentState = SessionState.CustomText;
                
		        break;
            case SessionState.IntroducingOurselvesBackground:
                if (canSkip)
                {
                    this.showHelpButton = false;
                    backgroundChooserScript.EndActivity();
                    log.LogInformation("Ended introducing Ourselves Background Selection");
                    backgroundChooserScript.enabled = false;
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    proceed = false;
                    this.introducingOurselves.SetActive(true);
                    this.introducingOurselvesScript.Setup(GlobalizationService.Instance.Globalize(GlobalizationService.IntroducingOurselvesAvatarText));
                    this.introducingOurselvesScript.enabled = true;
                    log.LogInformation("Started introducing Ourselves Avatar Selection");
                    currentState = SessionState.IntroducingOurselvesAvatar;
                }
                break;
            case SessionState.IntroducingOurselvesAvatar:
                if (proceed)
                {
                    introducingOurselvesScript.EndActivity();
                    log.LogInformation("Ended introducing ourselves Avatar Selection.");
                    //disable the introducing ourselves object
                    nameInputField.DeactivateInputField();
                    //GUIUtility.keyboardControl = 0; //ensure lose focus
                    introducingOurselves.SetActive(false);
                    this.introducingOurselvesScript.enabled = false;
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
                    log.LogInformation("Started IBox introduction A.");
                    this.HeartIcon.SetActive(true);
                    activityName = GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionTitle);
                    ibox.enabled = true;
                    ibox.instructions = GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionAText);
                    currentState = SessionState.IBoxIntroduction;
                };
                customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionTitle));
                currentState = SessionState.CustomTitle;
		        break;
            case SessionState.IBoxIntroduction:
                if (canSkip)
                {
                    canSkip = false;
                    this.HeartIcon.SetActive(false);
                    UIManagerScript.DisableSkipping();
                    ibox.EndActivity();
                    log.LogInformation("Ended ibox introduction Screen A.");
                    //disable the ibox and proceed to the next state
                    ibox.enabled = false;
                    displayIBox = true; //start displaying the UI icon
                    //start a custom text
                    log.LogInformation("Started ibox introduction Screen B");
               
                    //prepare custom text
                    setupNextPhaseCustomText = () => {
                        //disable the custom text and proceed to the next state
                        customText.EndActivity();
                        log.LogInformation("Ended ibox introduction Screen B.");
                        Application.LoadLevel("SessionFourScene");
                    };
                    customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionBText));
                    customText.enabled = true;
                    currentState = SessionState.CustomText;
                }
                break;
            case SessionState.CustomText:
                if (canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    //setup the next phase
                    customText.setupNextPhase();
                }
                break;
            case SessionState.CustomTitle:
                if (customTitleScript.CanContinue || canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    //setup the next phase
                    customTitleScript.setupNextPhase();
                }
                break;
            case SessionState.LoadingScreen:
		        if (loadingScreenScript.finished)
		        {
		            loadingScreenScript.enabled = false;
		            loadingScreenScript.setupNextPhase();
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
        if (currentState == SessionState.IntroducingOurselvesAvatar)
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
