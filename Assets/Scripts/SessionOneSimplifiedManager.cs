using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class SessionOneSimplifiedManager : SessionManager
{
    public Text maleButtonText;
    public Text femaleButtonText;
    public Texture2D HelpIconTexture;
    public Texture2D MeMeterTexture;

    BackgroundChooserScript backgroundChooserScript;
    private IntroducingOurselvesScript introducingOurselvesScript;
    private InnerSensationsScript innerSensationsScript;

	// Use this for initialization
	protected override void StartLogic ()
	{
        this.backgroundChooserScript = GameObject.Find("BackgroundChooser").GetComponent<BackgroundChooserScript>();
	    this.introducingOurselvesScript = GameObject.Find("IntroducingOurselvesScript").GetComponent<IntroducingOurselvesScript>();
	    this.innerSensationsScript = GameObject.Find("InnerSensations").GetComponent<InnerSensationsScript>();

	    this.sessionTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session1Title);
	    this.sessionSubTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session1SubTitle);

        maleButtonText.text = GlobalizationService.Instance.Globalize(GlobalizationService.MaleButton);
        femaleButtonText.text = GlobalizationService.Instance.Globalize(GlobalizationService.FemaleButton);

        hideInterface = true;

	    SessionManager.selectedIBox = this.iboxTexture;

	    System.Action openingTitlePhase = () =>
	    {
	        System.Action nextPhase = () =>
	        {
	            hideInterface = false;
	            currentState = SessionState.OpeningA;
	        };
	        customTitleScript.Setup("OpeningTitle",nextPhase, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningTitle));
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
				
				if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				{
					Bridge.UpdateWorldActivityName(this.activityName, "OpeningA");
					SessionManager.ActiveActivity = customText;
					customText.Setup("OpeningA",setupNextPhaseCustomText);
				}
				else
				{
					customText.Setup("OpeningA",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningAText));
				}
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

				if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				{
					Bridge.UpdateWorldActivityName(this.activityName, "OpeningB");
					SessionManager.ActiveActivity = customText;
					customText.Setup("OpeningB",setupNextPhaseCustomText);
				}
				else
				{
					customText.Setup("OpeningB",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningBText));
				}
				customText.enabled = true;
                currentState = SessionState.CustomText;

                break;

            case SessionState.OpeningC:
                log.LogInformation("Starting Opening Screen C.");

                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    CustomTextWithImage.EndActivity();
                    log.LogInformation("Ended Opening Screen C.");            
                    currentState = SessionState.OpeningD;
                };
				
				CustomTextWithImage.Image = this.heartTexture;
				CustomTextWithImage.ImageScale = 0.2f;	

				if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				{
					Bridge.UpdateWorldActivityName(this.activityName, "OpeningC");
					SessionManager.ActiveActivity = CustomTextWithImage;
					CustomTextWithImage.Setup("OpeningC",setupNextPhaseCustomText);
				}
				else
				{
					CustomTextWithImage.Setup("OpeningC",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningCText));
				}

                CustomTextWithImage.enabled = true;
                currentState = SessionState.CustomTextWithImage;

                break;

            case SessionState.OpeningD:
                log.LogInformation("Starting Opening Screen D.");

                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended Opening Screen D.");
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.OpeningF;
                };

				if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				{
					Bridge.UpdateWorldActivityName(this.activityName, "OpeningD");
					SessionManager.ActiveActivity = customText;
					customText.Setup("OpeningD",setupNextPhaseCustomText);
				}
				else
				{
					customText.Setup("OpeningD",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningDText));
				}

                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            /*case SessionState.OpeningE:
                log.LogInformation("Starting Opening Screen E.");
                
                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                   
                    CustomTextWithImage.EndActivity();
                    log.LogInformation("Ended Opening Screen E.");
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.OpeningF;
                };

		        CustomTextWithImage.Image = this.MeMeterTexture;
		        CustomTextWithImage.ImageScale = 0.5f;
                CustomTextWithImage.Setup("OpeningE",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningEText));
                CustomTextWithImage.enabled = true;
                currentState = SessionState.CustomTextWithImage;
                break;
             * */

            case SessionState.OpeningF:
                log.LogInformation("Starting Opening Screen F.");
                //prepare custom text
                setupNextPhaseCustomText = () =>
                {
                    CustomTextWithImage.EndActivity();
                    log.LogInformation("Ended Opening Screen F.");
                    //disable the custom text and proceed to the next 
                    currentState = SessionState.IntroducingOurselvesTitle;
                };

		        CustomTextWithImage.Image = this.helpButtonTexture;
                CustomTextWithImage.ImageScale = 0.05f;

				if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				{
					Bridge.UpdateWorldActivityName(this.activityName, "OpeningF");
					SessionManager.ActiveActivity = CustomTextWithImage;
					CustomTextWithImage.Setup("OpeningF",setupNextPhaseCustomText);
				}
				else
				{
					CustomTextWithImage.Setup("OpeningF",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.OpeningFText));
				}

                CustomTextWithImage.enabled = true;
                currentState = SessionState.CustomTextWithImage;
                break;

            case SessionState.IntroducingOurselvesTitle:
                log.LogInformation("Starting Introducing Ourselves Title");

                //prepare custom title
                setupNextPhaseCustomTitle = () =>
                {
                    log.LogInformation("Ended introducing ourselves Title.");
                    currentState = SessionState.IntroducingOurselvesA;
                };
                customTitleScript.Setup("IntroducingOuselvesTitle",setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.IntroducingOurselvesTitle));
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
                    backgroundChooserScript.Description = "BackgroundSelection";
                    backgroundChooserScript.backgroundSetter = SetBackground;
                    backgroundChooserScript.enabled = true;
                    UIManagerScript.EnableSkipping();
                };
				
				if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				{
					Bridge.UpdateWorldActivityName(this.activityName, "BackgroundSelectionDescription");
					SessionManager.ActiveActivity = customText;
					customText.Setup("BackgroundSelectionDescription",setupNextPhaseCustomText);
				}
				else
				{
					customText.Setup("BackgroundSelectionDescription",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.IntroducingOurselvesBackgroundText));
				}

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

					if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
					{
						Bridge.UpdateWorldActivityName(this.activityName, "AvatarSelection");
						SessionManager.ActiveActivity = introducingOurselvesScript;
						introducingOurselvesScript.Setup("AvatarSelection",null);
					}
					else
					{
						this.introducingOurselvesScript.Setup("AvatarSelection",GlobalizationService.Instance.Globalize(GlobalizationService.IntroducingOurselvesAvatarText));
					}
                    
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
                    activityName = GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionTitle);
                    ibox.enabled = true;
                    ibox.Description = "IBoxIntroductionA";
                    ibox.instructions = GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionAText);
                    currentState = SessionState.IBoxIntroductionA;
                };
                customTitleScript.Setup("IBoxIntroductionTitle",setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionTitle));
                currentState = SessionState.CustomTitle;
		        break;

            case SessionState.IBoxIntroductionA:
                if (canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    ibox.EndActivity();
                    log.LogInformation("Ended ibox introduction Screen A.");
                    //disable the ibox and proceed to the next state
                    ibox.enabled = false;
                    displayIBox = true; //start displaying the UI icon
                    this.currentState = SessionState.IBoxIntroductionB;
                }
                break;

            case SessionState.IBoxIntroductionB:
                log.LogInformation("Started ibox introduction Screen B");
                setupNextPhaseCustomText = () => {
                        //disable the custom text and proceed to the next state
                        customText.EndActivity();
                        log.LogInformation("Ended ibox introduction Screen B.");

                        this.showHelpButton = true;
                        this.helpTextContent = GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionBText);
                        //prepare the inner sensations activity
                        this.innerSensationsScript.enabled = true;
                        this.innerSensationsScript.Description = "IBoxSelection";
                        currentState = SessionState.InnerSensationsB;
                        log.LogInformation("Started ibox selection.");
                        
                    };

					if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
					{
						Bridge.UpdateWorldActivityName(this.activityName, "IBoxIntroductionB");
						SessionManager.ActiveActivity = customText;
						customText.Setup("IBoxIntroductionB",setupNextPhaseCustomText);
					}
					else
					{
						customText.Setup("IBoxIntroductionB",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionBText));
					}		

                    customText.enabled = true;
                    currentState = SessionState.CustomText;
		        break;

            case SessionState.InnerSensationsB:
                if (canSkip)
                {
                    canSkip = false;
                    this.showHelpButton = false;
                    this.innerSensationsScript.EndActivity();
                    log.LogInformation("Ended ibox selection exercise.");
                    //disable the inner sensations
                    //start closing Title 
                    currentState = SessionState.IBoxIntroductionC;
                    
                }
                break;

            case SessionState.IBoxIntroductionC:
                log.LogInformation("Started IBoxIntroduction Screen C");
		        setupNextPhaseCustomText = () =>
		        {
		            customText.EndActivity();
		            log.LogInformation("Ended IBoxIntroduction Screen C");
		            Application.LoadLevel("SessionFourScene");
		        };

				if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				{
					Bridge.UpdateWorldActivityName(this.activityName, "IBoxIntroductionC");
					SessionManager.ActiveActivity = customText;
					customText.Setup("IBoxIntroductionC",setupNextPhaseCustomText);
				}
				else
				{
					customText.Setup("IBoxIntroductionC",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.IBoxIntroductionCText));
				}	

                customText.enabled = true;
                currentState = SessionState.CustomText;
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

            case SessionState.CustomTextWithImage:
                if (canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    //setup the next phase
                    CustomTextWithImage.setupNextPhase();
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
