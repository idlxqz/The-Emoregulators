using Assets.Scripts;
using UnityEngine;

public class SessionFourManager : SessionManager {

    public BreathingRegulationScript breathingRegulation;
    public ActiveShakingMeditationScript activeShakingMeditation;
    public ProgressiveMuscleRelaxationScript progressiveMuscleRelaxation;
    public InnerSensationsScript innerSensations;
    public FacialMindfulnessScript facialMindfulness;
    public HowDoesMyBodyFeelScript howDoesMyBodyFeel;
    
    public EmotionListScript emotionListScript;

    public Texture2D MemeterTexture;
    public Texture2D CupTexture;

    public AudioSource AlarmSound;
    public AudioSource ApplauseSound;
    public AudioSource SeaSound;
    public AudioSource SpringSound;
    public AudioSource Alarm2Sound;

    // Use this for initialization
    protected override void StartLogic()
    {
        breathingRegulation = GameObject.Find("BreathingRegulation").GetComponent<BreathingRegulationScript>();
        activeShakingMeditation = GameObject.Find("ActiveShakingMeditation").GetComponent<ActiveShakingMeditationScript>();
        progressiveMuscleRelaxation = GameObject.Find("ProgressiveMuscleRelaxation").GetComponent<ProgressiveMuscleRelaxationScript>();
        innerSensations = GameObject.Find("InnerSensations").GetComponent<InnerSensationsScript>();
        facialMindfulness = GameObject.Find("FacialMindfulness").GetComponent<FacialMindfulnessScript>();
        howDoesMyBodyFeel = GameObject.Find("HowDoesMyBodyFeel").GetComponent<HowDoesMyBodyFeelScript>();
        CustomTextWithImage = GameObject.Find("CustomTextWithImage").GetComponent<CustomTextWithImage>();
        emotionListScript = GameObject.Find("EmotionList").GetComponent<EmotionListScript>();
        

        this.sessionTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session4Title);
        this.sessionSubTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session4SubTitle);

        hideInterface = true;

        System.Action openingTitlePhase = () =>
        {
            System.Action nextPhase = () =>
            {
                currentState = SessionState.CandleCeremonyTitle;
            };
            customTitleScript.Setup("Session2Title",nextPhase, sessionTitle);
            customTitleScript.enabled = true;
            displayIBox = false;

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
        switch (currentState)
        {
            
            case SessionState.CandleCeremonyTitle:
                log.LogInformation("Started candle ceremony title");
                //prepare custom title
                setupNextPhaseCustomTitle = () =>
                {
                    log.LogInformation("Ended candle cermony title");
                    //activate the candle cerimony
                    log.LogInformation("Started candle lighting cerimony.");
                    activityName = GlobalizationService.Instance.Globalize(GlobalizationService.CandleCeremonyTitle);
                    this.candle.instructions = GlobalizationService.Instance.Globalize(GlobalizationService.CandleCeremonyText);
                    candle.enabled = true;
                    currentState = SessionState.CandleCeremony;
                    //re-enable interface
                    hideInterface = false;
                    displayIBox = true;
                };
                customTitleScript.Setup("CandleCeremonyTitle",setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.CandleCeremonyTitle));
                currentState = SessionState.CustomTitle;
                break;

            case SessionState.CandleCeremony:
                if (canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    candle.EndActivity();
                    log.LogInformation("Ended candle lighting cerimony.");
                   
                    //prepare custom title
                    setupNextPhaseCustomTitle = () =>
                    {
                        //start introducing ourselves
                        log.LogInformation("Started a MinuteForMyselfA.");
                        activityName = GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfTitle);

                        //prepare custom text of introduction
                        setupNextPhaseCustomText = () =>
                        {
                            CustomTextWithImage.EndActivity();
                            log.LogInformation("Ended A MinuteForMyselfA.");
                            //disable the custom text and proceed to the next state
                            CustomTextWithImage.enabled = false;
                            log.LogInformation("Started A MinuteForMyselfB.");
                        
                            currentState = SessionState.MinuteForMyselfB;
                        };
                        CustomTextWithImage.Image = MemeterTexture;
                        CustomTextWithImage.ImageScale = 0.5f;

						/*if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
						{
							Bridge.UpdateWorldActivityName(this.activityName, "MinuteForMySelfA");
							SessionManager.ActiveActivity = CustomTextWithImage;
							CustomTextWithImage.Setup("MinuteForMySelfA",setupNextPhaseCustomText);
						}
						else
						{*/
							CustomTextWithImage.Setup("MinuteForMySelfA",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfAText));
						//}

                        CustomTextWithImage.enabled = true;
                        currentState = SessionState.CustomTextWithImage;
                    };

                    customTitleScript.Setup("MinuteForMySelfTitle",setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfTitle));
                    currentState = SessionState.CustomTitle;
                }
                break;

            case SessionState.MinuteForMyselfB:
                //prepare custom text of introduction
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended A MinuteForMyselfB.");
                    //disable the custom text and proceed to the next state
                    

                    currentState = SessionState.MinuteForMyselfC;
                };

				/*if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				{
					Bridge.UpdateWorldActivityName(this.activityName, "MinuteForMySelfB");
					SessionManager.ActiveActivity = customText;
					customText.Setup("MinuteForMySelfB",setupNextPhaseCustomText);
				}
				else
				{*/
					customText.Setup("MinuteForMySelfB",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfBText));
				//}

                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;

            case SessionState.MinuteForMyselfC:
                log.LogInformation("Started A MinuteForMyselfC.");
                //prepare custom text of introduction
                setupNextPhaseCustomText = () =>
                {
                    customText.MinimumWaitTime = 5;
                    customText.EndActivity();
                    log.LogInformation("Ended A MinuteForMyselfC.");
                    //disable the custom text and proceed to the next state
                    

                    currentState = SessionState.MinuteForMyselfD;
                };
                customText.MinimumWaitTime = 30;

				/*if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				{
					Bridge.UpdateWorldActivityName(this.activityName, "MinuteForMySelfC");
					SessionManager.ActiveActivity = customText;
					customText.Setup("MinuteForMySelfC",setupNextPhaseCustomText,() => this.AlarmSound.Play());
				}
				else
				{*/
					customText.Setup("MinuteForMySelfC",setupNextPhaseCustomText, () => this.AlarmSound.Play(),GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfCText));
				//}

                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;

            case SessionState.MinuteForMyselfD:
                log.LogInformation("Started A MinuteForMyselfD.");
                //prepare custom text of introduction
                setupNextPhaseCustomText = () =>
                {
                    customText.MinimumWaitTime = 5;
                    customText.EndActivity();
                    log.LogInformation("Ended A MinuteForMyselfD.");
                    //disable the custom text and proceed to the next state
                   
                    log.LogInformation("Started MeMeter activity");
                    this.showHelpButton = true;
                    this.helpTextContent =
                        GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfDText);
                    memeter.instructions = GlobalizationService.Instance.Globalize(GlobalizationService.MeMeterText);
                    memeter.showInstructions = true;
                    memeter.enabled = true;
                    memeter.Description = "MeMeter";
                    currentState = SessionState.MeMeter;
                    proceed = false;
                };
                customText.MinimumWaitTime = 10;
                customText.Setup("MinuteForMySelfD",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfDText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;

            case SessionState.MeMeter:
                if (canSkip)
                {
                    canSkip = false;
                    this.showHelpButton = false;
                    UIManagerScript.DisableSkipping();
                    memeter.EndActivity();
                    log.LogInformation("Ended me-meter interaction.");
                    //disable the memeter and proceed to the next state             
                    
                    //prepare custom title
                    setupNextPhaseCustomTitle = () =>
                    {
                        log.LogInformation("Started Facial Mindfullness A.");
                        activityName = GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessTitle);
                        //prepare custom text of introduction
                        setupNextPhaseCustomText = () =>
                        {
                            customText.MinimumWaitTime = 5;
                            customText.EndActivity();
                            log.LogInformation("Ended FacialMindfullness A.");
                            //disable the custom text and proceed to the next state
                          
                            currentState = SessionState.FacialMindfulnessB;
                        };
                        customText.MinimumWaitTime = 10;
                        customText.Setup("FacialMindfulnessA",setupNextPhaseCustomText, 
                            new Instruction[]{ new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessA1Text)),
                                               new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessA2Text))
                            });
                        customText.enabled = true;
                        currentState = SessionState.CustomText;
                    };
                    customTitleScript.Setup("FacialMindfulnessTitle",setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessTitle));
                    currentState = SessionState.CustomTitle;
                }
                break;
            case SessionState.FacialMindfulnessB:
                log.LogInformation("Started FacialMindfullness B.");
                //prepare custom text of introduction
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended FacialMindfulness B.");
                    

                    //do the facial mindfulness activity
                    log.LogInformation("Started FacialMindfulness D.");
                    this.showHelpButton = true;
                    this.helpTextContent =
                        GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessA2Text) + "\n\n"
                        + GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessB1Text) + "\n\n"
                        + GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessB2Text);
                    facialMindfulness.Setup("FacialMindfullnessD",userGender);
                    facialMindfulness.enabled = true;
                    currentState = SessionState.FacialMindfulnessD;
                };
                customText.Setup("FacialMindfullnessB",setupNextPhaseCustomText, 
                    new Instruction[]
                    {
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessB1Text)),
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessB2Text))
                    });
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.FacialMindfulnessD:
                if (canSkip)
                {
                    this.showHelpButton = false;
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    facialMindfulness.EndActivity();
                    log.LogInformation("Ended FacialMindfulness D.");

                    //prepare custom title
                    setupNextPhaseCustomTitle = () =>
                    {
                        log.LogInformation("Started breathing regulation exercise");
                        currentState = SessionState.BreathingRegulationB;
                        breathingRegulation.Avatar = this.GetPlayerAvatar;
                        breathingRegulation.Setup("BreathingRegulationA",null, () => SessionManager.PlayerScore += 5,
                            new []{
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationA1Text)),
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationA2Text)),
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationA3Text)),
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationA4Text)),
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationA5Text))});
                        breathingRegulation.enabled = true;
                    };
                    customTitleScript.Setup("BreathingRegulationTitle",setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationTitle));
                    currentState = SessionState.CustomTitle;
                }
                break;
            case SessionState.BreathingRegulationB:
                if(canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    breathingRegulation.EndActivity();
                    log.LogInformation("Ended breathing regulation A exercise.");
                    //continue on the breathing regulation exercise
                    //breathingRegulation.enabled = false;
                    log.LogInformation("Started Breathing Regulation B.");
                    breathingRegulation.Setup("BreathingRegulationB",null, () => SessionManager.PlayerScore += 5,
                            new []{
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationB1Text)),
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationB2Text)),
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationB3Text)),
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationB4Text),10)
                         });
                    breathingRegulation.enabled = true;
                    currentState = SessionState.BreathingRegulationC;
                }
                break;
            case SessionState.BreathingRegulationC:
                if(canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    breathingRegulation.EndActivity();
                    log.LogInformation("Ended breathing regulation B.");
                    //stop the breathing regulation exercise
                    breathingRegulation.Avatar.SetActive(false);
                    breathingRegulation.enabled = false;
                    //prepare custom title
                    setupNextPhaseCustomTitle = () =>
                    {
                        //start introducing active shaking meditation
                        log.LogInformation("Started active shaking meditation.");
                        activityName = GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationTitle);
                        activeShakingMeditation.Avatar = this.GetPlayerAvatar;
                        activeShakingMeditation.enabled = true;
                        activeShakingMeditation.Setup("ActiveShakingMeditationA",null, 
                            new[]{
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationA1Text)),
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationA2Text)),
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationA3Text)),
                                new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationA4Text))
                            });
                        currentState = SessionState.ActiveShakingMeditation;
                    };
                    customTitleScript.Setup("ActiveShakingMeditationTitle",setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationTitle));
                    currentState = SessionState.CustomTitle;
                }
                break;
            case SessionState.ActiveShakingMeditation:
                if (canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    activeShakingMeditation.EndActivity();
                    log.LogInformation("Ended active shaking meditation.");
                    //disable the active shaking meditation exercise
                    activeShakingMeditation.Avatar.SetActive(false);

                    var defaultWaitTime = customText.MinimumWaitTime;

                    log.LogInformation("Started ActiveShaking Meditation B");

                    setupNextPhaseCustomText = () =>
                    {
                        customText.EndActivity();
                        log.LogInformation("Ended ActiveShaking Meditation B.");
                        //disable the custom text and proceed to the next state
                        customText.MinimumWaitTime = defaultWaitTime;
                        
                        currentState = SessionState.ActiveShakingMeditationC;
                    };

                    customText.MinimumWaitTime = 60;
                    customText.Setup("ActiveShakingMeditationB",setupNextPhaseCustomText,() => { this.AlarmSound.Play(); SessionManager.PlayerScore += 5; }, GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationBText));
                    customText.enabled = true;
                    currentState = SessionState.CustomText;
                }
                break;
            case SessionState.ActiveShakingMeditationC:
                log.LogInformation("Started ActiveShaking C");

                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended ActiveShaking Meditation C.");
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.ActiveShakingMeditationD;
                };

                customText.Setup("ActiveShakingMeditationC",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationCText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.ActiveShakingMeditationD:
                log.LogInformation("Started ActiveShaking Meditation D.");

                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended ActiveShaking Meditation D.");
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.ProgressiveMuscleRelaxationTitle;
                };

                customText.Setup("ActiveShakingMeditationD",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationDText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.ProgressiveMuscleRelaxationTitle:
                log.LogInformation("Started ProgressiveMuscleRelaxation Title.");
                canSkip = false;
                UIManagerScript.DisableSkipping();
                //prepare custom title
                setupNextPhaseCustomTitle = () =>
                {
                    log.LogInformation("Ended ProgressiveMuscleRelaxation Title.");
                    currentState = SessionState.ProgressiveMuscleRelaxationA;

                };
                customTitleScript.Setup("PMRTitle",setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationTitle));
                currentState = SessionState.CustomTitle;
                break;
            case SessionState.ProgressiveMuscleRelaxationA:
                //start introducing progressive muscle relaxation
                log.LogInformation("Started progressive muscle relaxation A.");
                UIManagerScript.EnableSkipping();
                activityName = GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationTitle);
                //prepare custom text of introduction
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended progressive muscle relaxation A.");
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.ProgressiveMuscleRelaxationB;
                };

                customText.Setup("PMRA",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationAText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.ProgressiveMuscleRelaxationB:
                log.LogInformation("Started progressive muscle relaxation B.");
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended progressive muscle relaxation B.");
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.ProgressiveMuscleRelaxationC;
                };

                customText.Setup("PMRB",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationBText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.ProgressiveMuscleRelaxationC:
                log.LogInformation("Started progressive muscle relaxation C.");
                setupNextPhaseCustomText = () =>
                {
                    this.GetPlayerBall.SetActive(false);
                    progressiveMuscleRelaxation.EndActivity();
                    log.LogInformation("Ended progressive muscle relaxation C.");
                    
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.ProgressiveMuscleRelaxationD;
                };

                progressiveMuscleRelaxation.Avatar = this.GetPlayerAvatar;
                progressiveMuscleRelaxation.AnimationId = AnimatorControlerHashIDs.Instance.SqueezingExerciseTrigger;
                progressiveMuscleRelaxation.ExpectedMuscle = 1;
                progressiveMuscleRelaxation.enabled = true;
                progressiveMuscleRelaxation.SetBackground(progressiveMuscleRelaxation.GrassBackground);
                SpringSound.Play();
                
                this.GetPlayerBall.SetActive(true);

                progressiveMuscleRelaxation.Setup("PMRC",setupNextPhaseCustomText, () => SessionManager.PlayerScore += 2,
                    new []
                    {
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC1Text),0), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC2Text),3), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC3Text),5), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC4Text),5), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC5Text),5), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC6Text),5), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC7Text),5), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC8Text),5)
                    });
                
                currentState = SessionState.ProgressiveMuscleRelaxation;
                break;
            case SessionState.ProgressiveMuscleRelaxationD:
                log.LogInformation("Started progressive muscle relaxation D.");
                setupNextPhaseCustomText = () =>
                {
                    progressiveMuscleRelaxation.EndActivity();
                    log.LogInformation("Ended progressive muscle relaxation D.");
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.ProgressiveMuscleRelaxationE;
                };

                progressiveMuscleRelaxation.AnimationId = AnimatorControlerHashIDs.Instance.StreachingExerciseTrigger;
                progressiveMuscleRelaxation.ExpectedMuscle = 2;
                progressiveMuscleRelaxation.enabled = true;
                progressiveMuscleRelaxation.SetBackground(progressiveMuscleRelaxation.SunBackground);

                progressiveMuscleRelaxation.Setup("PMRD",setupNextPhaseCustomText, () => SessionManager.PlayerScore += 2,
                    new []
                    {
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD1Text),0),
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD2Text),3),
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD3Text),7),
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD4Text),7),
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD5Text),7),
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD6Text),7),
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD7Text),7)
                    });
                
                currentState = SessionState.ProgressiveMuscleRelaxation;
                break;
            case SessionState.ProgressiveMuscleRelaxationE:
                log.LogInformation("Started progressive muscle relaxation E.");
                setupNextPhaseCustomText = () =>
                {
                    progressiveMuscleRelaxation.EndActivity();
                    log.LogInformation("Ended progressive muscle relaxation E.");
                    //disable the custom text and proceed to the next state
                    currentState = SessionState.ProgressiveMuscleRelaxationF;
                };

                progressiveMuscleRelaxation.AnimationId = AnimatorControlerHashIDs.Instance.SnailExerciseTrigger;
                progressiveMuscleRelaxation.enabled = true;
                progressiveMuscleRelaxation.SetBackground(progressiveMuscleRelaxation.SnailBackground);

                progressiveMuscleRelaxation.Setup("PMRE",setupNextPhaseCustomText, () => SessionManager.PlayerScore += 2,
                    new[]
                    {
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE1Text),0), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE2Text),3), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE3Text),6), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE4Text),6), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE5Text),6), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE6Text),6), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE7Text),6), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE8Text),6)
                    });

                currentState = SessionState.ProgressiveMuscleRelaxation;
                break;
            case SessionState.ProgressiveMuscleRelaxationF:
                log.LogInformation("Started progressive muscle relaxation F.");
                setupNextPhaseCustomText = () =>
                {
                    progressiveMuscleRelaxation.EndActivity();
                    SeaSound.Stop();
                    log.LogInformation("Ended progressive muscle relaxation F.");
                    progressiveMuscleRelaxation.RevertToSessionBackground();
                    progressiveMuscleRelaxation.Avatar.SetActive(false);
                    
                    currentState = SessionState.HowDoesMyBodyFeelTitle;
                };

                progressiveMuscleRelaxation.AnimationId = AnimatorControlerHashIDs.Instance.SandExerciseTrigger;
                progressiveMuscleRelaxation.enabled = true;
                progressiveMuscleRelaxation.SetBackground(progressiveMuscleRelaxation.SandBackground);
                SpringSound.Stop();
                SeaSound.Play();

                progressiveMuscleRelaxation.Setup("PMRF",setupNextPhaseCustomText, () => SessionManager.PlayerScore += 1,
                    new[]
                    {
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationF1Text),0), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationF2Text),3), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationF3Text),7), 
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationF4Text),7)
                    });

                currentState = SessionState.ProgressiveMuscleRelaxation;
                break;
            case SessionState.HowDoesMyBodyFeelTitle:
                log.LogInformation("Started HowDoesMyBodyFeel Title");
                canSkip = false;
                UIManagerScript.DisableSkipping();
                //prepare custom title
                setupNextPhaseCustomTitle = () =>
                {
                    log.LogInformation("Ended HowDoesMyBodyFeel Title.");
                    activityName = GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelTitle);
                    currentState = SessionState.HowDoesMyBodyFeelA;

                };
                customTitleScript.Setup("HowDoesMyBodyFeelTitle",setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelTitle));
                currentState = SessionState.CustomTitle;
                break;
            case SessionState.HowDoesMyBodyFeelA:
                log.LogInformation("Started HowDoesMyBodyFeel A");
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended HowDoesMyBodyFeel A.");
                    currentState = SessionState.HowDoesMyBodyFeelB;
                };
                customText.Setup("HowDoesMyBodyFeelA",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelAText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.HowDoesMyBodyFeelB:
                log.LogInformation("Started HowDoesMyBodyFeel B");
                emotionListScript.Setup("HowDoesMyBodyFeelB",null, GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelBText));
                emotionListScript.enabled = true;
                currentState = SessionState.EmotionList;
                break;
            case SessionState.EmotionList:
                if (canSkip)
                {
                    canSkip = false;
                    emotionListScript.EndActivity();
                    log.LogInformation("Ended HowDoesMyBodyFeel B.");
                    currentState = SessionState.HowDoesMyBodyFeelC;
                }
                break;
            case SessionState.HowDoesMyBodyFeelC:
                log.LogInformation("Started HowDoesMyBodyFeel C");
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended HowDoesMyBodyFeel C.");
                    this.showHelpButton = true;
                    this.helpTextContent =
                        GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelBText) + "\n\n" +
                        GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelCText);
                    howDoesMyBodyFeel.Setup("HowDoesMyBodyFeelExercise",userGender);
                    howDoesMyBodyFeel.enabled = true;
                    //prepare the inner sensations activity
                    currentState = SessionState.HowDoesMyBodyFeel;
                };
                customText.Setup("HowDoesMyBodyFeelC",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelCText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.HowDoesMyBodyFeel:
                if (canSkip)
                {
                    canSkip = false;
                    this.showHelpButton = false;
                    howDoesMyBodyFeel.EndActivity();
                    log.LogInformation("Ended HowDoesMyBodyFeel Exercise");
                    currentState = SessionState.HowDoesMyBodyFeelD;
                }
                break;
            case SessionState.HowDoesMyBodyFeelD:
                log.LogInformation("Started HowDoesMyBodyFeel D");
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended HowDoesMyBodyFeel D.");
                    currentState = SessionState.InnerSensationsTitle;
                };
                customText.Setup("HowDoesMyBodyFeelD",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelDText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.InnerSensationsTitle:
                log.LogInformation("Started InnerSensations Title");
                canSkip = false;
                UIManagerScript.DisableSkipping();
                //prepare custom title
                setupNextPhaseCustomTitle = () =>
                {
                    currentState = SessionState.InnerSensationsA;
                };
                customTitleScript.Setup("InnterSensationsTitle",setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.InternalSensationsTitle));
                currentState = SessionState.CustomTitle;
                break;
            case SessionState.InnerSensationsA:
                //start IBOX inner sensations
                log.LogInformation("Started ibox inner sensations A.");
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended ibox inner sensations A.");
                    //disable the custom text and proceed to the next state
                    activityName = GlobalizationService.Instance.Globalize(GlobalizationService.InternalSensationsTitle);
                    this.showHelpButton = true;
                    this.helpTextContent = GlobalizationService.Instance.Globalize(GlobalizationService.InternalSensationsAText);
                    //prepare the inner sensations activity
                    innerSensations.enabled = true;
                    innerSensations.Description = "InnerSensationsB";
                    currentState = SessionState.InnerSensationsB;
                    log.LogInformation("Started ibox inner sensations B.");
                };
                customText.Setup("InnerSensationsA",setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.InternalSensationsAText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.InnerSensationsB:
                if (canSkip)
                {
                    canSkip = false;
                    this.showHelpButton = false;
                    innerSensations.EndActivity();
                    log.LogInformation("Ended ibox inner sensations exercise.");
                    //disable the inner sensations
                    //start closing Title 
                    currentState = SessionState.ClosingSessionTitle;

                }
                break;
            case SessionState.ClosingSessionTitle:
                UIManagerScript.DisableSkipping();
                log.LogInformation("Started ClosingSession Title.");
                activityName = GlobalizationService.Instance.Globalize(GlobalizationService.ClosingOfSessionTitle);
                //prepare custom title
                setupNextPhaseCustomTitle = () =>
                {
                    canSkip = false;
                    UIManagerScript.EnableSkipping();
                    log.LogInformation("Started Closing MeMeter");
                    memeter.instructions = GlobalizationService.Instance.Globalize(GlobalizationService.MeMeterClosingText);
                    memeter.showInstructions = true;
                    memeter.Setup("ClosingMeMeter");
                    memeter.enabled = true;
                    currentState = SessionState.ClosingMeMeter;
                    proceed = false;
                };
                customTitleScript.Setup("ClosingOfSessionTitle",setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.ClosingOfSessionTitle));
                currentState = SessionState.CustomTitle;
                break;
            case SessionState.ClosingMeMeter:
                if (canSkip)
                {
                    canSkip = false;
                    memeter.EndActivity();
                    log.LogInformation("Ended Closing MeMeter.");
                    //disable the memeter and proceed to the next state
                    
                    currentState = SessionState.ClosingSessionA;
                }
                break;
            case SessionState.ClosingSessionA:
                log.LogInformation("Started Closing Session A");

                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended Closing Session A");

                    log.LogInformation("Started Closing Candle activity");
                    candle.waitClickToClose = true;
                    candle.noInstructions = false;
                    candle.instructions = GlobalizationService.Instance.Globalize(GlobalizationService.ClosingOfSessionCandleText);
                    candle.isClosing = true;
                    candle.Setup("ClosingCandle");
                    candle.enabled = true;
                    currentState = SessionState.ClosingCandle;
                };
                customText.Setup("ClosingOfSessionA",setupNextPhaseCustomText, 
                    new []
                    {
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ClosingOfSessionA1Text)),
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ClosingOfSessionA2Text)),
                        new Instruction(GlobalizationService.Instance.Globalize(GlobalizationService.ClosingOfSessionA3Text))
                    });
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.ClosingCandle:
                if (candle.CanContinue)
                {
                    canSkip = false;
                    candle.EndActivity();
                    log.LogInformation("Ended Closing Candle activity.");
                    //disable the candle and proceed to the next state
                    log.LogInformation("Started Closing Session C.");
                    //prepare custom title
                    setupNextPhaseCustomText = () =>
                    {
                        CustomTextWithImage.EndActivity();
                        log.LogInformation("Ended Closing Session C.");
                        Application.Quit();
                    };

                    CustomTextWithImage.Image = this.CupTexture;
                    CustomTextWithImage.ImageScale = 1.0f;
                    CustomTextWithImage.ShowImageLabel = true;
                    CustomTextWithImage.ImageLabel = SessionManager.PlayerScore + "/100 Points";
                    CustomTextWithImage.Setup("ClosingOfSessionC",setupNextPhaseCustomText, () => this.ApplauseSound.Play(), GlobalizationService.Instance.Globalize(GlobalizationService.ClosingOfSessionCText));
                    CustomTextWithImage.MinimumWaitTime = 0;
                    CustomTextWithImage.enabled = true;
                    currentState = SessionState.CustomTextWithImage;
                }
                break;
            case SessionState.ProgressiveMuscleRelaxation:
                if (canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    //setup the next phase
                    progressiveMuscleRelaxation.setupNextPhase();
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

    }
	
}
