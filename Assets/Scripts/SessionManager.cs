using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour {

    //states of the session
    public enum SessionState
    {
        Start,
        CandleCeremony,
        IntroducingOurselves,
        MeMeter,
        IBoxIntroduction,
        MeMeterReuse,
        CustomText,
        CloseSession,
        Mindfullness,
        BasicPH
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
    protected Logger log;

    //hack to reuse the existing setup for the introducing ourselves
    protected bool proceed;
    protected string userName;

    //custom text support
    protected string customTextActivityName;
    protected string customTextContent;
    protected int customTextWaitTime;

    // Use this for initialization
    void Start()
    {
        currentState = SessionState.Start;
        candle = GameObject.Find("Candle").GetComponent<CandleScript>();
        memeter = GameObject.Find("MeMeter").GetComponent<MEMeterScript>();
        ibox = GameObject.Find("IBox").GetComponent<IBoxScript>();
        customText = GameObject.Find("CustomText").GetComponent<CustomTextScript>();
        activityArea = new Rect(Screen.width - 180, 22, 100, 30);
        //propagate instructions text formatting
        customText.instructionsFormat = memeter.instructionsFormat = ibox.instructionsFormat = candle.instructionsFormat;
        memeter.instructionsArea = ibox.instructionsArea = candle.instructionsArea;
        //configure all logging
        log = new Logger();
        customText.log = candle.log = memeter.log = ibox.log = log;
        //Cursor.visible = false;

        //child specific initializations
        StartLogic();
    }

    // Update is called once per frame
    void Update()
    {
        //child specific behavior
        UpdateLogic();
    }

    void OnGUI()
    {
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
        //draw ibox
        if (displayIBox)
        {
            GUI.DrawTexture(iboxArea, iboxTexture);
        }
        //draw the name of the activity
        GUI.Label(activityArea, activityName, activityFormat);

        //child specific behavior
        OnGUILogic();
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
    #endregion
}
