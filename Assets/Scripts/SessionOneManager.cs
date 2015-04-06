using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionOneManager : MonoBehaviour {

	//states of the session
	public enum SessionState{
		Start, 
		CandleCeremony,
		IntroducingOurselves,
		MeMeter,
		IBoxIntroduction,
        MeMeterReuse,
        CustomText,
		CloseSession
	}
	public SessionState currentState;

	public GameObject facilitator;
	public CandleScript candle;
    public MEMeterScript memeter;
	public IBoxScript ibox;
    public InputField nameInputField;
    public CustomTextScript customText;

	public GameObject facilitatorFrame;

	public GameObject introducingOurselves;

    //cursor
    public Texture2D originalCursor;
    public int cursorSizeX = 32; // set to width of your cursor texture 
    public int cursorSizeY = 32; // set to height of your cursor texture
    public bool showOriginal = true;

	//activity display formating
	public Rect activityArea;
	public GUIStyle activityFormat;
	public string activityName;
    
    //ibox display
    public bool displayIBox;
    public Texture2D iboxTexture;
    public Rect iboxArea;

	//logging
	private Logger log;

	//hack to reuse the existing setup for the introducing ourselves
	private bool proceed;
	private string userName;

    //custom text support
    private string customTextActivityName;
    private string customTextContent;
    private int customTextWaitTime;

	// Use this for initialization
	void Start () {
		currentState = SessionState.Start;
		candle = this.GetComponent<CandleScript>();
        memeter = this.GetComponent<MEMeterScript>();
        ibox = this.GetComponent<IBoxScript>();
        customText = this.GetComponent<CustomTextScript>();
		activityArea = new Rect(Screen.width - 180, 22, 100, 30);
        //propagate instructions text formatting
        customText.instructionsFormat = memeter.instructionsFormat = ibox.instructionsFormat = candle.instructionsFormat;
        memeter.instructionsArea = ibox.instructionsArea = candle.instructionsArea;
		//configure all logging
		log = new Logger();
		customText.log = candle.log = memeter.log = ibox.log = log;
        //Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		//coordinate the session state
		switch (currentState) {
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
				introducingOurselves.SetActive(false);
                GUIUtility.keyboardControl = 0; //lose focus
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
                    candle.simpleCandleAnimation = true;
                    candle.Setup();
                    candle.enabled = true;
                    currentState = SessionState.CloseSession;
                };
                customText.Setup(setupNextPhase, 20, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book");
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
                //TODO: load next session?
            }
            break;
		default:
			Debug.LogError("Unknown session state.");
			break;
		}
	}

	void OnGUI() {
        //draw a custom cursor
        /*if (showOriginal == true)
        {
            GUI.DrawTexture(new Rect(
                Input.mousePosition.x - cursorSizeX / 2 + cursorSizeX / 2,
                (Screen.height - Input.mousePosition.y) - cursorSizeY / 2 + cursorSizeY / 2,
                cursorSizeX,
                cursorSizeY),
                originalCursor);
        }*/
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
        //draw ibox
        if (displayIBox)
        {
            GUI.DrawTexture(iboxArea, iboxTexture);
        }
		//draw the name of the activity
		GUI.Label(activityArea, activityName, activityFormat);
	}


	#region Hack to re-use the existing introducing ourselves logic
	public void HandleIntroductionOurselvesClick(){
		proceed = true;
	}

	public void RegisterUserName(string newName){
		userName = newName;
	}
	#endregion
}
