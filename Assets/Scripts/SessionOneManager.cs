using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionOneManager : SessionManager {

	// Use this for initialization
	protected override void StartLogic () {
        sessionTitleStart = Time.time;
        currentState = SessionState.SessionTitle;
        hideInterface = true;
	}
	
	// Update is called once per frame
    protected override void UpdateLogic()
    {
		//coordinate the session state
		switch (currentState) {
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
			if(candle.finished){
				log.LogInformation("Ended candle lighting cerimony.");
				//disable the candle and proceed to the next state
				candle.enabled = false;
				//start introducing ourselves
				log.LogInformation("Started introducing ourselves.");
				activityName = "Introducing Ourselves";
				introducingOurselves.SetActive(true);
                currentState = SessionState.IntroducingOurselves;	
				proceed = false;
                nameInputField.ActivateInputField();
                nameInputField.Select();
			}
			break;
            case SessionState.IntroducingOurselves:
			if(proceed){
				log.LogInformation("User name: " + userName);
				log.LogInformation("Ended introducing ourselves.");
				//disable the introducing ourselves object
                nameInputField.DeactivateInputField();
                //GUIUtility.keyboardControl = 0; //ensure lose focus
                introducingOurselves.SetActive(false);
				//start memeter introduction
				log.LogInformation("Started me-meter introduction.");
				activityName = "Me-Meter introduction";
                memeter.enabled = true;
                currentState = SessionState.MeMeter;
                proceed = false;
			}
			break;
            case SessionState.MeMeter:
            if (memeter.finished)
            {
                log.LogInformation("Ended me-meter introduction.");
                //disable the memeter and proceed to the next state
                memeter.enabled = false;
                //start introducing ibox
                log.LogInformation("Started ibox introduction.");
                activityName = "I-Box Introducing";
                ibox.enabled = true;
                currentState = SessionState.IBoxIntroduction;
            }
			break;
            case SessionState.IBoxIntroduction:
            if (ibox.finished)
            {
                log.LogInformation("Ended ibox introduction.");
                //disable the memeter and proceed to the next state
                ibox.enabled = false;
                displayIBox = true; //start displaying the UI icon
                //start a custom text
                log.LogInformation("Started custom activity");
                activityName = "Custom activity";
                //prepare custom text
                System.Action setupNextPhase = () => {
                    log.LogInformation("Ended custom activity.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    log.LogInformation("Started me-meter reuse");
                    activityName = "Me-meter reuse";
                    memeter.showInstructions = false;
                    memeter.Setup();
                    memeter.enabled = true;
                    currentState = SessionState.MeMeterReuse;
                };
                customText.Setup(setupNextPhase, Constants.TextTimeToDisplay, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
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
            case SessionState.MeMeterReuse:
            if (memeter.finished)
            {
                log.LogInformation("Ended memeter reuse.");
                //disable the memeter and proceed to the next state
                memeter.enabled = false;
                //start a custom text
                log.LogInformation("Started closing message");
                activityName = "Closing message";
                //prepare custom text
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
                customText.Setup(setupNextPhase, Constants.TextTimeToDisplay, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
                customText.enabled = true;
                currentState = SessionState.CustomText;
            }
            break;
            case SessionState.CloseSession:
            if (candle.finished)
            {
                log.LogInformation("Ended closing activity.");
                //disable the candle and proceed to the next state
                candle.enabled = false;
                //go to session two
                Application.LoadLevel("SessionTwoScene");
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
