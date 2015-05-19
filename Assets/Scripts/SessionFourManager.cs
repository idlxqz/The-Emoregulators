using UnityEngine;

public class SessionFourManager : SessionManager {

    public BreathingRegulationScript breathingRegulation;
    public ActiveShakingMeditationScript activeShakingMeditation;
    public ProgressiveMuscleRelaxationScript progressiveMuscleRelaxation;
    public InnerSensationsScript innerSensations;
    public FacialMindfulnessScript facialMindfulness;
    public HowDoesMyBodyFeelScript howDoesMyBodyFeel;

    public AudioSource AlarmSound;

    // Use this for initialization
    protected override void StartLogic()
    {
        breathingRegulation = GameObject.Find("BreathingRegulation").GetComponent<BreathingRegulationScript>();
        activeShakingMeditation = GameObject.Find("ActiveShakingMeditation").GetComponent<ActiveShakingMeditationScript>();
        progressiveMuscleRelaxation = GameObject.Find("ProgressiveMuscleRelaxation").GetComponent<ProgressiveMuscleRelaxationScript>();
        innerSensations = GameObject.Find("InnerSensations").GetComponent<InnerSensationsScript>();
        facialMindfulness = GameObject.Find("FacialMindfulness").GetComponent<FacialMindfulnessScript>();
        howDoesMyBodyFeel = GameObject.Find("HowDoesMyBodyFeel").GetComponent<HowDoesMyBodyFeelScript>();

        this.sessionTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session4Title);
        this.sessionSubTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session4SubTitle);

        System.Action nextPhase = () =>
        {
            
            currentState = SessionState.CandleCeremonyTitle;
        };
        customTitleScript.Setup(nextPhase, sessionTitle);
        hideInterface = true;
        displayIBox = false;
        
        currentState = SessionState.CustomTitle;
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
                customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.CandleCeremonyTitle));
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
                            customText.EndActivity();
                            log.LogInformation("Ended A MinuteForMyselfA.");
                            //disable the custom text and proceed to the next state
                            customText.enabled = false;
                            log.LogInformation("Started A MinuteForMyselfB.");
                        
                            currentState = SessionState.MinuteForMyselfB;
                        };
                        customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfAText));
                        customText.enabled = true;
                        currentState = SessionState.CustomText;
                    };
                    customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfTitle));
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
                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfBText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.MinuteForMyselfC:
                log.LogInformation("Started A MinuteForMyselfC.");
                //prepare custom text of introduction
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended A MinuteForMyselfC.");
                    //disable the custom text and proceed to the next state
                    

                    currentState = SessionState.MinuteForMyselfD;
                };
                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfCText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.MinuteForMyselfD:
                log.LogInformation("Started A MinuteForMyselfD.");
                //prepare custom text of introduction
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended A MinuteForMyselfD.");
                    //disable the custom text and proceed to the next state
                   
                    log.LogInformation("Started MeMeter activity");
                    memeter.instructions = GlobalizationService.Instance.Globalize(GlobalizationService.MeMeterText);
                    memeter.showInstructions = true;
                    memeter.enabled = true;
                    currentState = SessionState.MeMeter;
                    proceed = false;
                };
                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfDText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.MeMeter:
                if (canSkip)
                {
                    canSkip = false;
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
                            customText.EndActivity();
                            log.LogInformation("Ended FacialMindfullness A.");
                            //disable the custom text and proceed to the next state
                          
                            currentState = SessionState.FacialMindfulnessB;
                        };
                        customText.Setup(setupNextPhaseCustomText, 
                            new []{
                                GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessA1Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessA2Text)
                            });
                        customText.enabled = true;
                        currentState = SessionState.CustomText;
                    };
                    customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessTitle));
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
                    facialMindfulness.Setup(userGender);
                    facialMindfulness.enabled = true;
                    currentState = SessionState.FacialMindfulnessD;
                };
                customText.Setup(setupNextPhaseCustomText, 
                    new[]
                    {
                        GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessB1Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessB2Text)
                    });
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.FacialMindfulnessD:
                if (canSkip)
                {
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
                        breathingRegulation.Setup(null, () => SessionManager.PlayerScore += 5,
                            new[]{GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationA1Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationA2Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationA3Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationA4Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationA5Text)});
                        breathingRegulation.enabled = true;
                    };
                    customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationTitle));
                    currentState = SessionState.CustomTitle;
                }
                break;
            case SessionState.BreathingRegulationB:
                if(canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    breathingRegulation.EndActivity();
                    log.LogInformation("Ended breathing regulation B exercise.");
                    //continue on the breathing regulation exercise
                    //breathingRegulation.enabled = false;
                    log.LogInformation("Started Breathing Regulation C.");
                    breathingRegulation.Setup(null, () => SessionManager.PlayerScore += 5,
                            new[]{GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationB1Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationB2Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationB3Text)});
                    currentState = SessionState.BreathingRegulationC;
                }
                break;
            case SessionState.BreathingRegulationC:
                if(canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    breathingRegulation.EndActivity();
                    log.LogInformation("Ended breathing regulation C.");
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
                        activeShakingMeditation.Setup(null, 
                            new[]{GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationA1Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationA2Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationA3Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationA4Text)
                            });
                        currentState = SessionState.ActiveShakingMeditation;
                    };
                    customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationTitle));
                    currentState = SessionState.CustomTitle;
                }
                break;
            case SessionState.ActiveShakingMeditation:
                if (canSkip)
                {
                    canSkip = false;
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
                    customText.Setup(setupNextPhaseCustomText,() => { this.AlarmSound.Play(); SessionManager.PlayerScore += 5; }, GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationBText));
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

                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationCText));
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

                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationDText));
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
                customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationTitle));
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

                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationAText));
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

                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationBText));
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
                progressiveMuscleRelaxation.enabled = true;
                progressiveMuscleRelaxation.SetBackground(progressiveMuscleRelaxation.GrassBackground);
                
                this.GetPlayerBall.SetActive(true);

                progressiveMuscleRelaxation.Setup(setupNextPhaseCustomText, () => SessionManager.PlayerScore += 2,
                    new []
                    {
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC1Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC2Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC3Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC4Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC5Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC6Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationC7Text)
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

                progressiveMuscleRelaxation.delayBetweenInstructions = 7;
                progressiveMuscleRelaxation.AnimationId = AnimatorControlerHashIDs.Instance.StreachingExerciseTrigger;
                progressiveMuscleRelaxation.enabled = true;
                progressiveMuscleRelaxation.SetBackground(progressiveMuscleRelaxation.SunBackground);

                progressiveMuscleRelaxation.Setup(setupNextPhaseCustomText, () => SessionManager.PlayerScore += 2,
                    new []
                    {
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD1Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD2Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD3Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD4Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD5Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD6Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationD7Text)
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

                progressiveMuscleRelaxation.Setup(setupNextPhaseCustomText, () => SessionManager.PlayerScore += 2,
                    new[]
                    {
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE1Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE2Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE3Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE4Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE5Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationE6Text)
                    });

                currentState = SessionState.ProgressiveMuscleRelaxation;
                break;
            case SessionState.ProgressiveMuscleRelaxationF:
                log.LogInformation("Started progressive muscle relaxation F.");
                setupNextPhaseCustomText = () =>
                {
                    progressiveMuscleRelaxation.EndActivity();
                    log.LogInformation("Ended progressive muscle relaxation F.");
                    progressiveMuscleRelaxation.RevertToSessionBackground();
                    progressiveMuscleRelaxation.Avatar.SetActive(false);
                    
                    currentState = SessionState.HowDoesMyBodyFeelTitle;
                };

                progressiveMuscleRelaxation.AnimationId = AnimatorControlerHashIDs.Instance.SandExerciseTrigger;
                progressiveMuscleRelaxation.enabled = true;
                progressiveMuscleRelaxation.SetBackground(progressiveMuscleRelaxation.SandBackground);

                progressiveMuscleRelaxation.Setup(setupNextPhaseCustomText, () => SessionManager.PlayerScore += 1,
                    new[]
                    {
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationF1Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationF2Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationF3Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.ProgressiveMuscleRelaxationF4Text)
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
                customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelTitle));
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
                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelAText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.HowDoesMyBodyFeelB:
                log.LogInformation("Started HowDoesMyBodyFeel B");
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended HowDoesMyBodyFeel B.");
                    currentState = SessionState.HowDoesMyBodyFeelC;
                };
                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelBText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.HowDoesMyBodyFeelC:
                log.LogInformation("Started HowDoesMyBodyFeel C");
                setupNextPhaseCustomText = () =>
                {
                    customText.EndActivity();
                    log.LogInformation("Ended HowDoesMyBodyFeel C.");
                    
                    log.LogInformation("Started HowDoesMyBodyFeel Exercise");
                    howDoesMyBodyFeel.Setup(userGender);
                    howDoesMyBodyFeel.enabled = true;
                    //prepare the inner sensations activity
                    currentState = SessionState.HowDoesMyBodyFeel;
                };
                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelCText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.HowDoesMyBodyFeel:
                if (canSkip)
                {
                    canSkip = false;
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
                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelDText));
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
                customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.InternalSensationsTitle));
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
                    //prepare the inner sensations activity
                    innerSensations.enabled = true;
                    currentState = SessionState.InnerSensationsB;
                    log.LogInformation("Started ibox inner sensations B.");
                };
                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.InternalSensationsAText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.InnerSensationsB:
                if (canSkip)
                {
                    innerSensations.EndActivity();
                    log.LogInformation("Ended ibox inner sensations exercise.");
                    //disable the inner sensations
                    //start closing Title 
                    currentState = SessionState.ClosingSessionTitle;

                }
                break;
            case SessionState.ClosingSessionTitle:
                canSkip = false;
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
                    memeter.Setup();
                    memeter.enabled = true;
                    currentState = SessionState.ClosingMeMeter;
                    proceed = false;
                };
                customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.ClosingOfSessionTitle));
                currentState = SessionState.CustomTitle;
                break;
            case SessionState.ClosingMeMeter:
                if (canSkip)
                {
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
                    candle.Setup();
                    candle.enabled = true;
                    currentState = SessionState.ClosingCandle;
                };
                customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.ClosingOfSessionAText));
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
                        customText.EndActivity();
                        log.LogInformation("Ended Closing Session C.");
                        Application.Quit();
                    };
                    customText.Setup(setupNextPhaseCustomText, GlobalizationService.Instance.Globalize(GlobalizationService.ClosingOfSessionCText));
                    customText.enabled = true;
                    currentState = SessionState.CustomText;
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
            case SessionState.CustomTitle:
                if (customTitleScript.CanContinue || canSkip)
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

    }
	
}
