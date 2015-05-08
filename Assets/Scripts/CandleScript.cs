using UnityEngine;
using System.Collections;

public class CandleScript : MonoBehaviour {

	//candle cerimony control
	public bool finished;
	public bool isLit;
    public bool simpleCandleAnimation;
    public bool noInstructions;
    public bool waitClickToClose;
    private bool clickedToClose;
    public bool isClosing;
	//candle animation control
	public Texture2D[] frames;
	public float frameChangeInterval; //in seconds
	public float lightinUpSeconds; //in seconds
    public float lightinDownSeconds; //in seconds
	public float animationTime; //in seconds
	private int selectedFrame;
	private float previousFrameTimestamp;
	private bool lightingUp;
	private float lightingUpStart;
    private float lightingDownStart;
    public float candleScale;
    private int emptyCandleTopSpace = 151;
    private int emptyCandleLeftSpace = 166;
    private int candleHeadWidth = 40;
    private int candleHeadHeight = 76;

    //match animation and control
    public Texture2D[] matchFrames;
    private bool holdingMatch;
    private bool matchClicked;
    private int selectedMatchFrame;
    private float previousMatchTimestamp;
    private Rect matchArea;
    private Rect originalMatchArea;
    public int bottomPadding;
    public float matchScale;
    private int emptyMatchTopSpace = 318;
    private int emptyMatchLeftSpace = 49;
    private int matchHeadWidth = 115;
    private int matchHeadHeight = 128;
    public int matchVerticalAdjustment;
    public Texture2D debugText;

    //positioning control
    public int lateralOffset;
    public int textCandleSpacing;

	//candle and instructions text areas definition
	public Rect frameArea;
	public Rect instructionsArea;

	//instruction control
	public string instructions;

	//instructions format
	public GUIStyle instructionsFormat;

	//logging
	public Logger log;

	// Use this for initialization
	void Start () {
        Setup();
	}

	// Update is called once per frame
	void Update () {
        //check if match lit the candle
        if (!isClosing)
        {
            if (CanCandleBeLit())
            {
                lightingUpStart = Time.time;
                isLit = true;
                UIManagerScript.EnableSkipping();
            }
            //select the current match frame
            if (matchClicked)
            {
                if (isLit)
                {
                    //match is out
                    selectedMatchFrame = 0;
                }
                else if ((Time.time - previousMatchTimestamp) >= frameChangeInterval * 2)
                {
                    //select a random frame from the match lit ones
                    selectedMatchFrame = Random.Range(1, 4); //fully lit frames
                    //update timestamps
                    previousMatchTimestamp = Time.time;
                }
            }
            else
            {
                //match is out
                selectedMatchFrame = 0;
            }
        }

		//select the correct candle frame
		if((Time.time - previousFrameTimestamp) >= frameChangeInterval){
			//lighting up phase, show the smaller flames frames
			if(isLit && lightingUp && (Time.time - lightingUpStart < lightinUpSeconds)){
				float progress = (Time.time - lightingUpStart) / lightinUpSeconds;
				if(progress < 0.5f)
					selectedFrame = 1; //low lit frame
				else
					selectedFrame = 2; //low lit frame 2
 			}
			else if(isLit){
				//select a random lit frame from the maximum framses
				//selectedFrame = Random.Range(2, 5); //fully lit frames -> older version to be used with lightingup logic
                selectedFrame = Random.Range(1, 5); //fully lit frames
			}
			else
				selectedFrame = 0; //unlit frame
			//update timestamp
			previousFrameTimestamp = Time.time;
		}
	
	    //detect mouse click on match
        if (Input.GetMouseButtonDown(0) && !isLit)
        {
            if (matchArea.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
            {
                matchClicked = true;
                holdingMatch = true;
                //play the sound of the match lighting up
                this.GetComponent<AudioSource>().Play();
            }
        }
        //detect drag end
        else if (Input.GetMouseButtonUp(0))
        {
            holdingMatch = false;
            SetMatchOriginalRectangle();
        }
        //get the lighting action for the candle
        /*if (Input.GetMouseButtonDown(0) && !isLit){
            if (IsCandleClicked())
            {
                lightingUpStart = Time.time;
                isLit = true;
            }
        }*/
        else if (Input.GetMouseButtonDown(0) && waitClickToClose)
        {
            if (IsCandleClicked())
            {
                lightingDownStart = Time.time;
                isLit = false;
                clickedToClose = true;
            }
        }

		//check if the candle cerimony is finished
        if (isLit && !waitClickToClose && (Time.time - lightingUpStart) >= animationTime)
            finished = true;
        else if (waitClickToClose && clickedToClose && (Time.time - lightingDownStart) >= lightinDownSeconds)
            finished = true;
	}

	void OnGUI() {
        //draw the match
        if (holdingMatch)
        {
            matchArea.x = Input.mousePosition.x - matchArea.width / 2;
            matchArea.y = Screen.height - Input.mousePosition.y - matchArea.width / 2 + matchVerticalAdjustment;
        }
		//draw the candle cerimony text
        if (!simpleCandleAnimation && !noInstructions)
		    GUI.Label(instructionsArea, instructions, instructionsFormat);
		//draw the candle frame
        GUI.DrawTexture(GetCandleArea(), frames[selectedFrame]);
        //draw the match
        if (!simpleCandleAnimation && !isClosing)
        {
            if (matchFrames != null)
            {
                if (matchFrames.Length > 0)
                {
                    GUI.DrawTexture(matchArea, matchFrames[selectedMatchFrame]);
                }
            }
        }

        //DEBUG ONLY
        /*GUI.DrawTexture(GetCandleHeadRect(), debugText);
        GUI.DrawTexture(GetMatchHeadRect(), debugText);*/
	}

    private bool IsCandleClicked()
    {
        //left mouse click, check if it was inside the candle
        if (GetCandleArea().Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
        {   
            log.LogInformation("Clicked on candle area.");
            return true;
        }
        else
        {
            log.LogInformation("Clicked outside candle.");
            return false;
        }
    }

    private Rect GetCandleArea(){
        if (simpleCandleAnimation || noInstructions)
        {
            float width = frameArea.width;
            float height = frameArea.height;
            return new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height);
        }
        else
            return frameArea;
    }

    public void Setup()
    {
        isLit = false;
        previousFrameTimestamp = Time.time;
        lightingUp = true;
        finished = false;
        if (simpleCandleAnimation || waitClickToClose)
        {
            clickedToClose = false;
            isLit = true;
            lightingUp = false;
            lightingUpStart = Time.time;
        }

        matchClicked = false;
        holdingMatch = false;

        SetCandleOriginalRectangle();

        //dynamic positioning
        if (!noInstructions)
        {
            //text
            instructionsArea.x = lateralOffset;
            instructionsArea.width = Screen.width - 2 * lateralOffset - frameArea.width - textCandleSpacing;
            //candle
            frameArea.x = Screen.width - lateralOffset - frameArea.width;
        }

        SetMatchOriginalRectangle();
    }

    private void SetCandleOriginalRectangle()
    {
        frameArea = new Rect(700, 150, frames[0].width * candleScale, frames[0].height * candleScale);
    }

    private void SetMatchOriginalRectangle()
    {
        if(noInstructions)
            matchArea = new Rect(Screen.width / 2 + frameArea.width, Screen.height - matchFrames[0].height * matchScale - bottomPadding, matchFrames[0].width * matchScale, matchFrames[0].height * matchScale);
        else
            matchArea = new Rect(Screen.width / 2 - matchArea.width / 2, Screen.height - matchFrames[0].height * matchScale - bottomPadding, matchFrames[0].width * matchScale, matchFrames[0].height * matchScale);
    }

    private bool CanCandleBeLit()
    {
        return RectsOverlap(GetCandleHeadRect(), GetMatchHeadRect());
    }

    private Rect GetMatchHeadRect() {
        return new Rect(
                matchArea.x + emptyMatchLeftSpace * matchScale,
                matchArea.y + emptyMatchTopSpace * matchScale,
                matchHeadWidth * matchScale,
                matchHeadHeight * matchScale);
    }

    private Rect GetCandleHeadRect()
    {
        Rect temp = GetCandleArea();
        return new Rect(
            temp.x + emptyCandleLeftSpace * candleScale,
            temp.y + emptyCandleTopSpace * candleScale,
            candleHeadWidth * candleScale,
            candleHeadHeight * candleScale);
    }

    private bool RectsOverlap(Rect r1, Rect r2)
    {
        var widthOverlap = (r1.xMin >= r2.xMin) && (r1.xMin <= r2.xMax) ||
                        (r2.xMin >= r1.xMin) && (r2.xMin <= r1.xMax);

        var heightOverlap = (r1.yMin >= r2.yMin) && (r1.yMin <= r2.yMax) ||
                        (r2.yMin >= r1.yMin) && (r2.yMin <= r1.yMax);

        return widthOverlap && heightOverlap;
    }
}
