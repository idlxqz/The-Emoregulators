using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionTwoManager : SessionManager {

    public MindfullnessScript mindfullness;
    public BasicPhScript basicPh;

    private bool firstMeMeterUse;

    // Use this for initialization
    protected override void StartLogic()
    {
        sessionTitleStart = Time.time;
        currentState = SessionState.SessionTitle;
        hideInterface = true;

        firstMeMeterUse = true;
        mindfullness = GameObject.Find("Mindfullness").GetComponent<MindfullnessScript>();
        basicPh = GameObject.Find("BasicPh").GetComponent<BasicPhScript>();
        basicPh.log = log;
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
                    log.LogInformation("Started me-meter interaction.");
                    activityName = "Me-Meter Interaction";
                    memeter.enabled = true;
                    displayIBox = true;
                    currentState = SessionState.MeMeterReuse;
                }
                break;
            case SessionState.MeMeterReuse:
                if (memeter.finished)
                {   
                    log.LogInformation("Ended me-meter interaction.");
                    //disable the memeter and proceed to the next state
                    memeter.enabled = false;
                    //first memeter use ending logic
                    if (firstMeMeterUse)
                    {
                        firstMeMeterUse = false;
                        //start introducing mindfullness exercise
                        log.LogInformation("Started mindfullness introduction.");
                        activityName = "Mindfullness Introduction";
                        //prepare custom text of introduction
                        System.Action setupNextPhase = () =>
                        {
                            log.LogInformation("Ended mindfullness introduction.");
                            //disable the custom text and proceed to the next state
                            customText.enabled = false;
                            log.LogInformation("Started mindfullness exercise");
                            activityName = "Mindfullness Exercise";
                            mindfullness.enabled = true;
                            currentState = SessionState.Mindfullness;
                        };
                        customText.Setup(setupNextPhase, 20, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
                        customText.enabled = true;
                        currentState = SessionState.CustomText;
                    }
                    else
                    {
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
                }
                break;
            case SessionState.Mindfullness:
                if (mindfullness.finished)
                {
                    log.LogInformation("Ended mindfullness exercise.");
                    //disable the mindfullness and proceed to the next state
                    mindfullness.enabled = false;
                    activityName = "BASICPh Introduction";
                    //prepare custom text of introduction
                    System.Action setupNextPhase = () =>
                    {
                        log.LogInformation("Ended BASICPh introduction.");
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;
                        log.LogInformation("Started BASICPh exercise");
                        activityName = "BASICPh Exercise";
                        basicPh.enabled = true;
                        currentState = SessionState.BasicPH;
                    };
                    customText.Setup(setupNextPhase, 20, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
                    customText.enabled = true;
                    currentState = SessionState.CustomText;
                }
                break;
            case SessionState.BasicPH:
                if (basicPh.finished)
                {
                    log.LogInformation("Ended BASICPh exercise.");
                    //disable the basicPH and proceed to the next state
                    basicPh.enabled = false;
                    //start introducing ourselves
                    log.LogInformation("Started me-meter reuse");
                    activityName = "Me-meter reuse";
                    memeter.showInstructions = false;
                    memeter.Setup();
                    memeter.enabled = true;
                    currentState = SessionState.MeMeterReuse;
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
