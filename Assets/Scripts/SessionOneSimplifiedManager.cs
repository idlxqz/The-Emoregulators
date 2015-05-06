using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionOneSimplifiedManager : SessionManager {

    public GameObject maleAvatar;
    public GameObject femaleAvatar;

	// Use this for initialization
	protected override void StartLogic () {
        sessionTitleStart = Time.time;
        currentState = SessionState.SessionTitle;
        hideInterface = true;
	}
	
	// Update is called once per frame
    protected override void UpdateLogic()
    {
        System.Action setupNextPhase;
		//coordinate the session state
		switch (currentState) {
            case SessionState.SessionTitle:
                if ((Time.time - sessionTitleStart) >= sessionTitleDuration)
                {
                    hideInterface = false;
                    currentState = SessionState.OpeningA;
                }
                break;
            case SessionState.OpeningA:
		        activityName = "Opening";
                log.LogInformation("Starting Opening Screen A.");
                
                //prepare custom text
                setupNextPhase = () => {
                    log.LogInformation("Ended Opening Screen A.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.OpeningB;
                };

		        customText.Setup(setupNextPhase, Constants.TextTimeToDisplay,
		            "Today we will learn to recognize what happens to our bodies when we are a bit nervous, and together we will see also how to better handle all these sensations.\n\nBut...we will do it having fun, playing!\n\nYes, in fact Emoregulator is a game developed for people of your age. So, today you will do different activities, some directly at the computer, other externally. Soon, you will know your avatar and he will do exactly the same as you do. You will give him the name that you want, in fact, the avatar is yourself.");
                customText.enabled = true;
                currentState = SessionState.CustomText;
				
			    break;
            case SessionState.OpeningB:
                log.LogInformation("Starting Opening Screen B.");

                //prepare custom text
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended Opening Screen B.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.OpeningC;
                };

                customText.Setup(setupNextPhase, Constants.TextTimeToDisplay,
                    "Hi! Welcome to Emoregulator, the game to learn how to handle stress and to regulate our emotions.\nOften we have to cope with situations that make us feeling stressed and that put us a little in trouble ...\nMaybe for an important exam, or because we discuss with our parents, or because we have too many things to do all at once!\nSo, we are getting nervous, we don't know what is the best to do ... we feel stomachache, headache and we don't find the solution...");
                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningC:
                log.LogInformation("Starting Opening Screen C.");

                //prepare custom text
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended Opening Screen C.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.OpeningD;
                };

                customText.Setup(setupNextPhase, Constants.TextTimeToDisplay,
                    "Before each level, you will find the written instructions of what you have to do. Also, as you saw, you were equipped with some physiological sensors. We will not do any medical examinations ;-). Through the sensors you  can see how you are improving in your ability to manage anxiety. You will see your heart rate, and more you will be good, more points you'll get!\n\nThe game consists of several levels, to move to the next one, you should always complete the previous one and then click on the continue bottom at the right.");
                customText.enabled = true;
                currentState = SessionState.CustomText;

                break;
            case SessionState.OpeningD:
                log.LogInformation("Starting Opening Screen D.");

                //prepare custom text
                setupNextPhase = () =>
                {
                    log.LogInformation("Ended Opening Screen D.");
                    //disable the custom text and proceed to the next state
                    customText.enabled = false;
                    currentState = SessionState.IntroducingOurselves;
                    log.LogInformation("Started introducing ourselves.");
                    activityName = "Introducing Ourselves";
                    introducingOurselves.SetActive(true);
                    proceed = false;
                    nameInputField.ActivateInputField();
                    nameInputField.Select();
                };

		        customText.Setup(setupNextPhase, Constants.TextTimeToDisplay,
		            "Each time you will complete an exercise, you'll earn points.\nAt each level the difficulty will increase, so you can earn more and more!\nAt the top left you will see the points that you can achieve and then, those that actually you have received. \nIn this way, your avatar will become stronger and more skilled in dealing with stress and manage emotions!\n\nLast information ...Once you start exercising, you can always click on the question icon in the lower right and read again the instructions.\nNow, it's time to have fun!\nCome on, let's start!");

                customText.enabled = true;
                currentState = SessionState.CustomText;

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
                    //start memeter introduction
                    log.LogInformation("Started IBox introduction.");
                    activityName = "IBox 1: Introduction";
                    ibox.enabled = true;
                    currentState = SessionState.IBoxIntroduction;
                    proceed = false;
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
            case SessionState.IBoxIntroduction:
                if (ibox.finished)
                {
                    log.LogInformation("Ended ibox introduction Screen A.");
                    //disable the ibox and proceed to the next state
                    ibox.enabled = false;
                    displayIBox = true; //start displaying the UI icon
                    //start a custom text
                    log.LogInformation("Started ibox introduction Screen B");
               
                    //prepare custom text
                    setupNextPhase = () => {
                        //disable the custom text and proceed to the next state
                        customText.enabled = false;

                        log.LogInformation("Ended ibox introduction Screen B.");
                        Application.LoadLevel("SessionFourScene");
                    };
                    customText.Setup(setupNextPhase, Constants.TextTimeToDisplay, "Well! We are ready to start having fun!\n\nClick to continue");
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

    public override void Continue()
    {
        //coordinate the session state
        switch (currentState)
        {

            case SessionState.CandleCeremony:
                candle.finished = true;
                break;
            case SessionState.IntroducingOurselves:
                proceed = true;
                break;
            case SessionState.MeMeter:
                memeter.finished = true;
                break;
            case SessionState.IBoxIntroduction:
                ibox.finished = true;
                break;
            case SessionState.CustomText:
                customText.finished = true;
                break;
            case SessionState.MeMeterReuse:
                memeter.finished = true;
                break;
            case SessionState.CloseSession:
                candle.finished = true;
                break;
        }
    }
}
