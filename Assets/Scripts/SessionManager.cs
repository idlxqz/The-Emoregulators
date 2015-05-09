using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour {

    //states of the session
    public enum SessionState
    {
        Start,
        ChooseBackground,
        OpeningA,
        OpeningB,
        OpeningC,
        OpeningD,
        OpeningE,
        OpeningF,
        IntroducingOurselvesTitle,
        IntroducingOurselves,
        MinuteForMyselfTitle,
        MinuteForMyselfA,
        MinuteForMyselfB,
        MinuteForMyselfC,
        MinuteForMyselfD,
        MeMeter,
        CandleCeremonyTitle,
        CandleCeremony,
        FacialMindfulnessTitle,
        FacialMindfulnessA,
        FacialMindfulnessB,
        FacialMindfulnessC,
        FacialMindfulnessD,
        IBoxIntroductionTitle,
        IBoxIntroduction,
        MeMeterReuse,
        CustomText,
        CustomTitle,
        CloseSession,
        Mindfullness,
        BasicPH,
        BreathingRegulation,
        ActiveShakingMeditation,
        ProgressiveMuscleRelaxation,
        InnerSensations
    }

    //title support
    public bool hideInterface;
    public int sessionTitleDuration;

    public SessionState currentState;

    public GameObject facilitator;
    public CandleScript candle;
    public MEMeterScript memeter;
    public IBoxScript ibox;
    public InputField nameInputField;
    public CustomTextScript customText;
    public CustomTitleScript customTitleScript;

    public GameObject facilitatorFrame;

    public GameObject introducingOurselves;

    public Text ContinueButtonText;

    //cursor
    public Texture2D originalCursor;
    public int cursorSizeX = 32; // set to width of your cursor texture 
    public int cursorSizeY = 32; // set to height of your cursor texture
    public bool showOriginal = true;

    //session display formating
    private Rect titleArea;
    private Rect subTitleArea;
    public GUIStyle titleFormat;
    public GUIStyle subTitleFormat;
    protected string sessionTitle;
    protected string sessionSubTitle;

    //activity display formating
    public Rect activityArea;
    public GUIStyle activityFormat;
    public string activityName;

    //ibox display
    public bool displayIBox;
    public Texture2D iboxTexture;
    public Texture2D heartTexture;
    public Rect iboxArea;
    public int heartSpacing;

    //logging
    protected Logger log;

    //hack to reuse the existing setup for the introducing ourselves
    protected bool proceed;
    protected string userName;

    //custom text support
    protected string customTextActivityName;
    protected string customTextContent;
    protected int customTextWaitTime;

    //continue button logic
    protected bool canSkip;

    //user gender, avatar and score
    public enum Gender
    {
        Male,
        Female
    }

    public GameObject maleAvatar;
    public GameObject femaleAvatar;
    public static Gender userGender; //share it accross scenes 
    public GameObject GetPlayerAvatar
    {
        get
        {
            if (userGender == Gender.Female)
                return femaleAvatar;
            else
                return maleAvatar;
        }
    }
    public static int playerScore = 0; //share it accross scenes 
    public GUIStyle scoreFormat;

    //background configurations
    public static Texture2D selectedBackground;

    //help text
    public bool showHelpText;
    public string helpTextContent;
    public GUIStyle helpTextStyle;
    public int lateralOffsetHelp;
    public int helpButtonSize;
    public Texture2D helpButtonTexture;

    // Use this for initialization
    void Start()
    {
        currentState = SessionState.Start;
        candle = GameObject.Find("Candle").GetComponent<CandleScript>();
        memeter = GameObject.Find("MeMeter").GetComponent<MEMeterScript>();
        ibox = GameObject.Find("IBox").GetComponent<IBoxScript>();
        customText = GameObject.Find("CustomText").GetComponent<CustomTextScript>();
        customTitleScript = GameObject.Find("CustomTitle").GetComponent<CustomTitleScript>();

        activityArea = new Rect(Screen.width - 180, 22, 150, 50);
        activityFormat.wordWrap = true;
        activityFormat.alignment = TextAnchor.MiddleCenter;
        customTextWaitTime = 5;
        //propagate instructions text formatting
        customText.instructionsFormat = memeter.instructionsFormat = ibox.instructionsFormat = candle.instructionsFormat;
        memeter.instructionsArea = ibox.instructionsArea = candle.instructionsArea;
        //configure all logging
        log = Logger.Instance;
        //Cursor.visible = false;

        //set title and subtitle positioning
        int width = 100;
        titleArea = new Rect(Screen.width / 2 - width / 2, 10, width, 30);
        subTitleArea = new Rect(Screen.width / 2 - width / 2, 40, width, 30);

        this.ContinueButtonText.text = GlobalizationService.Instance.Globalize(GlobalizationService.ContinueButton);

        //child specific initializations
        StartLogic();

        if (selectedBackground != null)
        {
            SetBackground(selectedBackground);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //child specific behavior
        UpdateLogic();
    }

    void OnGUI()
    {
        GUI.depth = -1000;
        //shared logic
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
        if (!hideInterface && currentState != SessionState.CustomTitle)
        {
            //draw ibox
            if (displayIBox)
            {
                GUI.DrawTexture(iboxArea, iboxTexture);
                GUI.DrawTexture(new Rect(iboxArea.xMin + iboxArea.width + heartSpacing, iboxArea.yMin, iboxArea.width, iboxArea.height), heartTexture);
                GUI.Label(new Rect(iboxArea.xMin, iboxArea.yMin + iboxArea.height, iboxArea.width * 2 + heartSpacing, 15), ""+playerScore, scoreFormat);
            }
            //draw title and subtitle of the session
            GUI.Label(titleArea, sessionTitle, titleFormat);
            GUI.Label(subTitleArea, sessionSubTitle, subTitleFormat);

            //draw the name of the activity
            GUI.Label(activityArea, activityName, activityFormat);
        }

        //child specific behavior
        OnGUILogic();

        //help button
        if (GUI.Button(new Rect(Screen.width - helpButtonSize, Screen.height - helpButtonSize, helpButtonSize, helpButtonSize), helpButtonTexture, GUIStyle.none))
        {
            showHelpText = !showHelpText;
        }

        //help text always overlays everything
        if (showHelpText)
        {
            Rect helpArea = new Rect(lateralOffsetHelp, lateralOffsetHelp, Screen.width - lateralOffsetHelp *2, Screen.height - lateralOffsetHelp*2);
            GUI.Label(helpArea, helpTextContent, helpTextStyle);
        }
    }

    public void SetBackground(Texture2D _selected)
    {
        selectedBackground = _selected;
        var cameraObject = GameObject.Find("Main Camera");
        if(cameraObject != null){
            cameraObject.GetComponentInChildren<MeshRenderer>().material.mainTexture = _selected;
        }
    }

    public virtual void Continue()
    {
        canSkip = true;
    }
    
    protected virtual void StartLogic() {
        //to be implemented by child classes
    }

    //to be implemented by child classes
    protected virtual void UpdateLogic()
    {
        //to be implemented by child classes
    }

    //to be implemented by child classes
    protected virtual void OnGUILogic()
    {
        //to be implemented by child classes
    }

    #region Hack to re-use the existing introducing ourselves logic
    public void HandleIntroductionOurselvesClick()
    {
        proceed = true;
    }

    public void RegisterUserName(string newName)
    {
        userName = newName;
    }

    public void HandleSelectFemaleGender()
    {
        userGender = Gender.Female;
    }

    public void HandleSelectMaleGender()
    {
        userGender = Gender.Male;
    }
    #endregion
}
