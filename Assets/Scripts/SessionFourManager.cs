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
                log.LogInformation("Started me-meter.");
                activityName = "Me-meter";
                memeter.enabled = true;
                currentState = SessionState.MeMeter;
                break;
            case SessionState.MeMeter:
                if (memeter.finished)
                {   
                    log.LogInformation("Ended me-meter interaction.");
                    //disable the memeter and proceed to the next state
                    memeter.enabled = false;                   
                    //start introducing breathing regulation exercise
                    log.LogInformation("Started breathing regulation introduction.");
                    activityName = "Breathing Regulation Introduction";
                    //prepare custom text of introduction
                    System.Action setupNextPhase = () =>
                    {
                        log.LogInformation("Ended breathing regulation introduction.");
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;
                        log.LogInformation("Started breathing regulation exercise");
                        activityName = "Breathing Regulation Exercise";
                        breathingRegulation.enabled = true;
                        currentState = SessionState.BreathingRegulation;
                    };
                    customText.Setup(setupNextPhase, 20, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
                    customText.enabled = true;
                    currentState = SessionState.CustomText;
                }
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
                    System.Action setupNextPhase = () =>
                    {
                        log.LogInformation("Ended active shaking meditation introduction.");
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;
                        log.LogInformation("Started active shaking meditation exercise");
                        activityName = "Active/Shaking Meditation Exercise";
                        activeShakingMeditation.enabled = true;
                        currentState = SessionState.ActiveShakingMeditation;
                    };
                    customText.Setup(setupNextPhase, 20, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
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
                    System.Action setupNextPhase = () =>
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
                    System.Action setupNextPhase = () =>
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
