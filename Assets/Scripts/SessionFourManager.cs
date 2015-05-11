using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionFourManager : SessionManager {

    public BreathingRegulationScript breathingRegulation;
    public ActiveShakingMeditationScript activeShakingMeditation;
    public ProgressiveMuscleRelaxationScript progressiveMuscleRelaxation;
    public InnerSensationsScript innerSensations;
    public FacialMindfulnessScript facialMindfulness;

    // Use this for initialization
    protected override void StartLogic()
    {
        breathingRegulation = GameObject.Find("BreathingRegulation").GetComponent<BreathingRegulationScript>();
        activeShakingMeditation = GameObject.Find("ActiveShakingMeditation").GetComponent<ActiveShakingMeditationScript>();
        progressiveMuscleRelaxation = GameObject.Find("ProgressiveMuscleRelaxation").GetComponent<ProgressiveMuscleRelaxationScript>();
        innerSensations = GameObject.Find("InnerSensations").GetComponent<InnerSensationsScript>();
        facialMindfulness = GameObject.Find("FacialMindfulness").GetComponent<FacialMindfulnessScript>();

        this.sessionTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session4Title);
        this.sessionSubTitle = GlobalizationService.Instance.Globalize(GlobalizationService.Session4SubTitle);

        breathingRegulation.instructionsFormat = customText.instructionsFormat;
        activeShakingMeditation.instructionsFormat = customText.instructionsFormat;
        activeShakingMeditation.instructionsArea = breathingRegulation.instructionsArea;
        //breathingRegulation.instructionsArea = candle.instructionsArea;

        System.Action nextPhase = () =>
        {
            hideInterface = false;
            currentState = SessionState.CandleCeremonyTitle;
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
                };
                customTitleScript.Setup(setupNextPhaseCustomTitle, GlobalizationService.Instance.Globalize(GlobalizationService.CandleCeremonyTitle));
                currentState = SessionState.CustomTitle;
                break;
            case SessionState.CandleCeremony:
                if (candle.finished || canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    log.LogInformation("Ended candle lighting cerimony.");
                    //disable the candle and proceed to the next state
                    candle.enabled = false;
                    //prepare custom title
                    setupNextPhaseCustomTitle = () =>
                    {
                        //start introducing ourselves
                        log.LogInformation("Started a MinuteForMyselfA.");
                        activityName = GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfTitle);

                        //prepare custom text of introduction
                        setupNextPhaseCustomText = () =>
                        {
                            log.LogInformation("Ended A MinuteForMyselfA message.");
                            //disable the custom text and proceed to the next state
                            customText.enabled = false;
                            log.LogInformation("Started A MinuteForMyselfB activity");
                        
                            currentState = SessionState.MinuteForMyselfB;
                        };
                        customText.Setup(setupNextPhaseCustomText, 3.0f, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfAText));
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
                    log.LogInformation("Ended A MinuteForMyselfB message.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    log.LogInformation("Started A MinuteForMyselfC activity");

                    currentState = SessionState.MinuteForMyselfC;
                };
                customText.Setup(setupNextPhaseCustomText, customTextWaitTime, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfBText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.MinuteForMyselfC:
                //prepare custom text of introduction
                setupNextPhaseCustomText = () =>
                {
                    log.LogInformation("Ended A MinuteForMyselfC message.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    log.LogInformation("Started A MinuteForMyselfD activity");

                    currentState = SessionState.MinuteForMyselfD;
                };
                customText.Setup(setupNextPhaseCustomText, customTextWaitTime, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfCText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.MinuteForMyselfD:
                //prepare custom text of introduction
                setupNextPhaseCustomText = () =>
                {
                    log.LogInformation("Ended A MinuteForMyselfD message.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                   
                    log.LogInformation("Started MeMeter activity");
                    memeter.instructions = GlobalizationService.Instance.Globalize(GlobalizationService.MeMeterText);
                    memeter.showInstructions = true;
                    memeter.enabled = true;
                    currentState = SessionState.MeMeter;
                    proceed = false;
                };
                customText.Setup(setupNextPhaseCustomText, customTextWaitTime, GlobalizationService.Instance.Globalize(GlobalizationService.MinuteForMyselfDText));
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.MeMeter:
                if (memeter.finished || canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    log.LogInformation("Ended me-meter interaction.");
                    //disable the memeter and proceed to the next state
                    memeter.enabled = false;                   
                    
                    //prepare custom title
                    setupNextPhaseCustomTitle = () =>
                    {
                        log.LogInformation("Started Facial Mindfullness A.");
                        activityName = "Facial Mindfulness";
                        //prepare custom text of introduction
                        setupNextPhaseCustomText = () =>
                        {
                            log.LogInformation("Ended FacialMindfullness A.");
                            //disable the custom text and proceed to the next state
                            customText.enabled = false;
                            log.LogInformation("Started FacialMindfullness B.");
                            currentState = SessionState.FacialMindfulnessB;
                        };
                        customText.Setup(setupNextPhaseCustomText, customTextWaitTime, 
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
                //prepare custom text of introduction
                setupNextPhaseCustomText = () =>
                {
                    log.LogInformation("Ended FacialMindfulness C.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;

                    //do the facial mindfulness activity
                    log.LogInformation("Ended FacialMindfulness D.");
                    facialMindfulness.Setup(userGender);
                    facialMindfulness.enabled = true;
                    currentState = SessionState.FacialMindfulnessD;
                };
                customText.Setup(setupNextPhaseCustomText, customTextWaitTime,
                    new[]
                    {
                        GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessB1Text),
                        GlobalizationService.Instance.Globalize(GlobalizationService.FacialMindfulnessB2Text)
                    });
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.FacialMindfulnessD:
                if (facialMindfulness.finished || canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    facialMindfulness.enabled = false;
                    log.LogInformation("Ended FacialMindfulness D.");

                    //prepare custom title
                    setupNextPhaseCustomTitle = () =>
                    {
                        log.LogInformation("Started breathing regulation exercise");
                        currentState = SessionState.BreathingRegulationB;
                        breathingRegulation.Avatar = this.GetPlayerAvatar;
                        breathingRegulation.Setup(null,customTextWaitTime, 
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
                if(breathingRegulation.finished || canSkip){
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    log.LogInformation("Ended breathing regulation B exercise.");
                    //continue on the breathing regulation exercise
                    //breathingRegulation.enabled = false;
                    breathingRegulation.finished = false;

                    breathingRegulation.Setup(null, customTextWaitTime,
                            new[]{GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationB1Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationB2Text),
                                GlobalizationService.Instance.Globalize(GlobalizationService.BreathingRegulationB3Text)});
                    currentState = SessionState.BreathingRegulationC;
                }
                break;
            case SessionState.BreathingRegulationC:
                if(breathingRegulation.finished || canSkip){
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    log.LogInformation("Ended breathing regulation B exercise.");
                    //stop the breathing regulation exercise
                    breathingRegulation.Avatar.SetActive(false);
                    breathingRegulation.enabled = false;
                    //prepare custom title
                    setupNextPhaseCustomTitle = () =>
                    {
                        //start introducing active shaking meditation
                        log.LogInformation("Started active shaking meditation introduction.");
                        activityName = GlobalizationService.Instance.Globalize(GlobalizationService.ActiveMeditationTitle);
                        activeShakingMeditation.Avatar = this.GetPlayerAvatar;
                        activeShakingMeditation.enabled = true;
                        activeShakingMeditation.Setup(null, customTextWaitTime,
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
                if (activeShakingMeditation.finished || canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    log.LogInformation("Ended active shaking meditation exercise.");
                    //disable the active shaking meditation exercise
                    activeShakingMeditation.Avatar.SetActive(false);
                    activeShakingMeditation.enabled = false;

                    //prepare custom title
                    setupNextPhaseCustomTitle = () =>
                    {
                        //start introducing progressive muscle relaxation
                        log.LogInformation("Started progressive muscle relaxation introduction.");
                        activityName = "Progressive Muscle Relaxation Introduction";
                        //prepare custom text of introduction
                        setupNextPhaseCustomText = () =>
                        {
                            log.LogInformation("Ended progressive muscle relaxation introduction.");
                            //disable the custom text and proceed to the next state
                            customText.enabled = false;
                            log.LogInformation("Started progressive muscle relaxation exercise");
                            activityName = "Progressive Muscle Relaxation Exercise";
                            progressiveMuscleRelaxation.enabled = true;
                            currentState = SessionState.ProgressiveMuscleRelaxation;
                        };
                        customText.Setup(setupNextPhaseCustomText, 20, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
                        customText.enabled = true;
                        currentState = SessionState.CustomText;
                    };
                    customTitleScript.Setup(setupNextPhaseCustomTitle, "Progressive Muscle Relaxation");
                    currentState = SessionState.CustomTitle;
                }
                break;
            case SessionState.ProgressiveMuscleRelaxation:
                if (progressiveMuscleRelaxation.finished || canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    log.LogInformation("Ended progressive muscle relaxation exercise.");
                    //disable the progressive muscle relaxation
                    progressiveMuscleRelaxation.enabled = false;
                    //prepare custom title
                    setupNextPhaseCustomTitle = () =>
                    {
                        //start IBOX inner sensations
                        log.LogInformation("Started ibox inner sensations exercise.");
                        activityName = "IBOX Inner Sensations";
                        //prepare the inner sensations activity
                        innerSensations.enabled = true;
                        currentState = SessionState.InnerSensations;
                    };
                    customTitleScript.Setup(setupNextPhaseCustomTitle, "IBOX Inner Sensations");
                    currentState = SessionState.CustomTitle;
                }
                break;
            case SessionState.InnerSensations:
                if (innerSensations.finished || canSkip)
                {
                    canSkip = false;
                    UIManagerScript.DisableSkipping();
                    log.LogInformation("Ended ibox inner sensations exercise.");
                    //disable the inner sensations
                    innerSensations.enabled = false;
                    //start closing text 
                    log.LogInformation("Started closing message.");
                    activityName = "Closing message";
                    //prepare custom text of introduction
                    setupNextPhaseCustomText = () =>
                    {
                        log.LogInformation("Ended closing message.");
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;
                        displayIBox = false;
                        log.LogInformation("Started closing activity");
                        activityName = "Closing Activity";
                        candle.waitClickToClose = true;
                        candle.noInstructions = true;
                        candle.isClosing = true;
                        candle.Setup();
                        candle.enabled = true;
                        currentState = SessionState.CloseSession;
                    };
                    customText.Setup(setupNextPhaseCustomText, 20, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
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
            case SessionState.CloseSession:
                if (candle.finished)
                {
                    log.LogInformation("Ended closing activity.");
                    //disable the candle and proceed to the next state
                    candle.enabled = false;
                    //TODO: load next session?
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
