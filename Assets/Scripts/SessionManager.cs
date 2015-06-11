using UnityEngine;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour {

    //states of the session
    public enum SessionState
    {
        LoadingScreen,
        Start,
        PreBaseline,
        GrossBaselineDetection,
        PostBaseline,
        OpeningA,
        OpeningB,
        OpeningC,
        OpeningD,
        OpeningE,
        OpeningF,
        IntroducingOurselvesTitle,
        IntroducingOurselvesA,
        IntroducingOurselvesBackground,
        IntroducingOurselvesAvatar,
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
        IBoxIntroductionA,
        IBoxIntroductionB,
        IBoxIntroductionC,
        MeMeterReuse,
        CustomText,
        CustomTextWithImage,
        CustomTitle,
        Mindfullness,
        BasicPH,
        BreathingRegulationB,
        BreathingRegulationC,
        ActiveShakingMeditation,
        ActiveShakingMeditationB,
        ActiveShakingMeditationC,
        ActiveShakingMeditationD,
        ProgressiveMuscleRelaxationTitle,
        ProgressiveMuscleRelaxation,
        ProgressiveMuscleRelaxationA,
        ProgressiveMuscleRelaxationB,
        ProgressiveMuscleRelaxationC,
        ProgressiveMuscleRelaxationD,
        ProgressiveMuscleRelaxationE,
        ProgressiveMuscleRelaxationF,
        InnerSensationsTitle,
        InnerSensationsA,
        InnerSensationsB,
        ClosingSessionTitle,
        ClosingSessionA,
        ClosingMeMeter,
        ClosingCandle,
        ClosingSessionB,
        HowDoesMyBodyFeelTitle,
        HowDoesMyBodyFeelA,
        HowDoesMyBodyFeelB,
        HowDoesMyBodyFeelC,
        HowDoesMyBodyFeelD,
        HowDoesMyBodyFeel,
        EmotionList,
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
    public LoadingScreenScript loadingScreenScript;
    public CustomTextWithImage CustomTextWithImage;

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
    protected string sessionTitle;
    protected string sessionSubTitle;

    //activity display formating
    public Rect activityArea;
    public string activityName;

    //ibox display
    public bool displayIBox;
    public Texture2D iboxTexture;
    public Texture2D heartTexture;
    public Rect iboxArea;
    public int heartSpacing;

    //configurations
    protected StandardConfigurations Configurations;

    //logging
    protected Logger log;

    //hack to reuse the existing setup for the introducing ourselves
    protected bool proceed;
    public string userName;

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
    public GameObject maleBall;
    public GameObject femaleAvatar;
    public GameObject femaleBall;
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

    public GameObject GetPlayerBall
    {
        get
        {
            if (userGender == Gender.Female)
                return femaleBall;
            else
                return maleBall;
        }
    }
    public static int PlayerScore { get; set; } //share it accross scenes

    public static string UserCode { get; set; }

    //background configurations
    public static Texture2D selectedBackground;
    public static Texture2D selectedIBox;

    //help text
    public bool showHelpButton;
    public bool showHelpText;
    public string helpTextContent;
    public int lateralOffsetHelp;
    public int helpButtonSize;
    public Texture2D helpButtonTexture;

    // Use this for initialization
    void Start()
    {
        showHelpButton = false;
        currentState = SessionState.Start;
        candle = GameObject.Find("Candle").GetComponent<CandleScript>();
        ibox = GameObject.Find("IBox").GetComponent<IBoxScript>();
        customText = GameObject.Find("CustomText").GetComponent<CustomTextScript>();
        customTitleScript = GameObject.Find("CustomTitle").GetComponent<CustomTitleScript>();
        loadingScreenScript = GameObject.Find("LoadingScreen").GetComponent<LoadingScreenScript>();
        CustomTextWithImage = GameObject.Find("CustomTextWithImage").GetComponent<CustomTextWithImage>();

        this.Configurations = GameObject.FindObjectOfType<StandardConfigurations>();

        activityArea = new Rect(Screen.width - 180, 22, 150, 50);
        customTextWaitTime = 5;
        
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
        //make the background fill the fov
        UpdateBackgroundPlane();
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
                GUI.DrawTexture(iboxArea, SessionManager.selectedIBox);
                GUI.DrawTexture(new Rect(iboxArea.xMin + iboxArea.width + heartSpacing, iboxArea.yMin, iboxArea.width, iboxArea.height), heartTexture);
                GUI.Label(new Rect(iboxArea.xMin, iboxArea.yMin + iboxArea.height, iboxArea.width, 15), ""+PlayerScore, this.Configurations.ScoreFormat );
                GUI.Label(new Rect(iboxArea.xMin + iboxArea.width + heartSpacing, iboxArea.yMin + iboxArea.height, iboxArea.width, 15), (SensorManager.HeartRate==0? "-" : ""+SensorManager.HeartRate), this.Configurations.ScoreFormat);
                //GUI.Label(new Rect(iboxArea.xMin + 2 * iboxArea.width + 2* heartSpacing, iboxArea.yMin + iboxArea.height, iboxArea.width, 15),""+SensorManager.Muscle1Active, scoreFormat);
                //GUI.Label(new Rect(iboxArea.xMin + 3 * iboxArea.width + 3 * heartSpacing, iboxArea.yMin + iboxArea.height, iboxArea.width, 15), "" + SensorManager.Muscle2Active, scoreFormat);
            }
            //draw title and subtitle of the session
            GUI.Label(titleArea, sessionTitle, this.Configurations.TitleFormat);
            GUI.Label(subTitleArea, sessionSubTitle, this.Configurations.SubTitleFormat);

            //draw the name of the activity
            GUI.Label(activityArea, activityName, this.Configurations.ActivityFormat);
        }

        //child specific behavior
        OnGUILogic();

        if (this.showHelpButton)
        {
            //help button
            if (GUI.Button(new Rect(Screen.width - helpButtonSize, Screen.height - helpButtonSize, helpButtonSize, helpButtonSize), helpButtonTexture, GUIStyle.none))
            {
                showHelpText = !showHelpText;
            }

            //help text always overlays everything
            if (showHelpText)
            {
                Rect helpArea = new Rect(lateralOffsetHelp, lateralOffsetHelp, Screen.width - lateralOffsetHelp * 2, Screen.height - lateralOffsetHelp * 2);
                GUI.Label(helpArea, helpTextContent, this.Configurations.HelpFormat);
            }
        }
    }

    public void SetBackground(Texture2D _selected)
    {
        selectedBackground = _selected;
        var cameraObject = GameObject.Find("Main Camera");
        if(cameraObject != null){
            SpriteRenderer sr = cameraObject.GetComponentInChildren<SpriteRenderer>();
            sr.sprite = Sprite.Create(_selected, new Rect(0, 0, _selected.width, _selected.height), new Vector2(0.5f, 0.5f));
        }
    }

    

    private void UpdateBackgroundPlane()
    {
        SpriteRenderer sr = GameObject.Find("Main Camera").GetComponentInChildren<SpriteRenderer>();
        GameObject spriteBack = GameObject.Find("Background");

        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        if(spriteBack != null)
        {
            spriteBack.transform.localScale = new Vector3(
            worldScreenWidth / sr.sprite.bounds.size.x,
            worldScreenHeight / sr.sprite.bounds.size.y, 1);
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
