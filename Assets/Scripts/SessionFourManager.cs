using UnityEditor.VersionControl;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionFourManager : SessionManager {

    public BreathingRegulationScript breathingRegulation;
    public ActiveShakingMeditationScript activeShakingMeditation;
    public ProgressiveMuscleRelaxationScript progressiveMuscleRelaxation;
    public InnerSensationsScript innerSensations;

    // Use this for initialization
    protected override void StartLogic()
    {
        sessionTitleStart = Time.time;
        currentState = SessionState.SessionTitle;
        hideInterface = true;

        breathingRegulation = GameObject.Find("BreathingRegulation").GetComponent<BreathingRegulationScript>();
        activeShakingMeditation = GameObject.Find("ActiveShakingMeditation").GetComponent<ActiveShakingMeditationScript>();
        progressiveMuscleRelaxation = GameObject.Find("ProgressiveMuscleRelaxation").GetComponent<ProgressiveMuscleRelaxationScript>();
        innerSensations = GameObject.Find("InnerSensations").GetComponent<InnerSensationsScript>();

        innerSensations.log = progressiveMuscleRelaxation.log = log;
    }

    // Update is called once per frame
    protected override void UpdateLogic()
    {
        System.Action setupNextPhase;
        //coordinate the session state
        switch (currentState)
        {
            case SessionState.SessionTitle:
                if ((Time.time - sessionTitleStart) >= sessionTitleDuration)
                {
                    hideInterface = false;
                    currentState = SessionState.Start;
                }
                break;
            case SessionState.Start:
                //activate the cande cerimony
                log.LogInformation("Started candle lighting cerimony.");
                activityName = "Candle Lighting Cerimony";
                candle.enabled = true;
                currentState = SessionState.CandleCeremony;
                break;
            case SessionState.CandleCeremony:
                if (candle.finished)
                {
                    log.LogInformation("Ended candle lighting cerimony.");
                    //disable the candle and proceed to the next state
                    candle.enabled = false;
                    //start introducing ourselves
                    log.LogInformation("Started a MinuteForMyselfA.");
                    activityName = "A Minute for Myself";

                    //prepare custom text of introduction
                    setupNextPhase = () =>
                    {
                        log.LogInformation("Ended A MinuteForMyselfA message.");
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;
                        log.LogInformation("Started A MinuteForMyselfB activity");
                        
                        currentState = SessionState.MinuteForMyselfB;
                    };
                    customText.Setup(setupNextPhase, 3.0f, "Let's start now with the more practical exercises!");
                    customText.enabled = true;
                    currentState = SessionState.CustomText;
                }
                break;
            case SessionState.MinuteForMyselfB:
                //prepare custom text of introduction
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended A MinuteForMyselfB message.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    log.LogInformation("Started A MinuteForMyselfC activity");

                    currentState = SessionState.MinuteForMyselfC;
                };
                customText.Setup(setupNextPhase, customTextWaitTime, "Stage 1: Slow down your body and your thoughts\n\nTake a minute and focus on yourself, try to slow down your thoughts, let your mind and body relax, and pay attention to the natural rhythm of your breathing…\n\nYou may close your eyes if you wish for a few seconds, take a slow deep breath. Just focus on the this natural action you are doing every day: breathing, and notice if it feels different to breath with focuse and attention.");
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.MinuteForMyselfC:
                //prepare custom text of introduction
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended A MinuteForMyselfC message.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    log.LogInformation("Started A MinuteForMyselfD activity");

                    currentState = SessionState.MinuteForMyselfD;
                };
                customText.Setup(setupNextPhase, customTextWaitTime, "Stage 2: Orient - Focus on yourselves\n\nTry and focus yourselves in space and pay attention to what you feel, what you're doing, on the space, what is around you, and what is in the room … remind yourselves that you are in a safe and protected place");
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.MinuteForMyselfD:
                //prepare custom text of introduction
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended A MinuteForMyselfD message.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    log.LogInformation("Started MeMeter activity");
                    memeter.enabled = true;
                    currentState = SessionState.MeMeter;
                    proceed = false;
                };
                customText.Setup(setupNextPhase, customTextWaitTime, "Stage 3: Scan and rate yourselves\n\nTry and evaluate the amount of tension you have in the moment according to the ME-Meter.\nPay attention to how it feels in your body to be tense and what kind of thoughts come to your mind when you are tense");
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.MeMeter:
                if (memeter.finished)
                {   
                    log.LogInformation("Ended me-meter interaction.");
                    //disable the memeter and proceed to the next state
                    memeter.enabled = false;                   
                    
                    log.LogInformation("Started Facial Mindfullness A.");
                    activityName = "Facial Mindfulness";
                    //prepare custom text of introduction
                    setupNextPhase = () =>
                    {
                        log.LogInformation("Ended FacialMindfullness A.");
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;
                        log.LogInformation("Started FacialMindfullness B.");
                        
                        
                        currentState = SessionState.FacialMindfulnessB;
                    };
                    customText.Setup(setupNextPhase, customTextWaitTime, "Now let's focus on the face!\n\nNote the different parts of your face, your forehead, chin, mouth, eyes.\nThe different parts of your face are relaxed or tense? Where do you feel more tense? Do you have some other feelings? What is your facial expression? Try to notice without changing expression.");
                    customText.enabled = true;
                    currentState = SessionState.CustomText;
                }
                break;
            case SessionState.FacialMindfulnessB:
                //prepare custom text of introduction
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended FacialMindfulness B.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    log.LogInformation("Started FacialMindfulnessC activity");

                    currentState = SessionState.FacialMindfulnessC;
                };
                customText.Setup(setupNextPhase, customTextWaitTime, "Now select from the list the parts of the face where you feel more tense: your avatar will be colored red in the corresponding areas.\n\nThen select the parts where you feel more relaxed: your avatar will be colored blue in the corresponding areas");
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.FacialMindfulnessC:
                //prepare custom text of introduction
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended FacialMindfulness C.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    log.LogInformation("Started breathing regulation exercise");

                    currentState = SessionState.BreathingRegulation;
                    breathingRegulation.enabled = true;
                };
                customText.Setup(setupNextPhase, 3, "TODO: Replace this screen with a picture of the face...");
                customText.enabled = true;
                currentState = SessionState.CustomText;
                break;
            case SessionState.BreathingRegulation:
                if(breathingRegulation.finished){
                    log.LogInformation("Ended breathing regulation exercise.");
                    //disable the breathing regulation
                    breathingRegulation.enabled = false;
                    //start introducing active shaking meditation
                    log.LogInformation("Started active shaking meditation introduction.");
                    activityName = "Active/Shaking Meditation Introduction";
                    //prepare custom text of introduction
                    setupNextPhase = () =>
                    {
                        log.LogInformation("Ended active shaking meditation introduction.");
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;
                        log.LogInformation("Started active shaking meditation exercise");
                        activityName = "Active/Shaking Meditation Exercise";
                        activeShakingMeditation.enabled = true;
                        currentState = SessionState.ActiveShakingMeditation;
                    };
                    customText.Setup(setupNextPhase, customTextWaitTime, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
                    customText.enabled = true;
                    currentState = SessionState.CustomText;
                }
                break;
            case SessionState.ActiveShakingMeditation:
                if (activeShakingMeditation.finished)
                {
                    log.LogInformation("Ended active shaking meditation exercise.");
                    //disable the active shaking meditation exercise
                    activeShakingMeditation.enabled = false;
                    //start introducing progressive muscle relaxation
                    log.LogInformation("Started progressive muscle relaxation introduction.");
                    activityName = "Progressive Muscle Relaxation Introduction";
                    //prepare custom text of introduction
                    setupNextPhase = () =>
                    {
                        log.LogInformation("Ended progressive muscle relaxation introduction.");
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;
                        log.LogInformation("Started progressive muscle relaxation exercise");
                        activityName = "Progressive Muscle Relaxation Exercise";
                        progressiveMuscleRelaxation.enabled = true;
                        currentState = SessionState.ProgressiveMuscleRelaxation;
                    };
                    customText.Setup(setupNextPhase, 20, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
                    customText.enabled = true;
                    currentState = SessionState.CustomText;
                }
                break;
            case SessionState.ProgressiveMuscleRelaxation:
                if (progressiveMuscleRelaxation.finished)
                {
                    log.LogInformation("Ended progressive muscle relaxation exercise.");
                    //disable the progressive muscle relaxation
                    progressiveMuscleRelaxation.enabled = false;
                    //start IBOX inner sensations
                    log.LogInformation("Started ibox inner sensations exercise.");
                    activityName = "IBOX Inner Sensations";
                    //prepare the inner sensations activity
                    innerSensations.enabled = true;
                    currentState = SessionState.InnerSensations;
                }
                break;
            case SessionState.InnerSensations:
                if (innerSensations.finished)
                {
                    log.LogInformation("Ended ibox inner sensations exercise.");
                    //disable the inner sensations
                    innerSensations.enabled = false;
                    //start closing text 
                    log.LogInformation("Started closing message.");
                    activityName = "Closing message";
                    //prepare custom text of introduction
                    setupNextPhase = () =>
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
                    customText.Setup(setupNextPhase, 20, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
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
