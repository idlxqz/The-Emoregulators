using UnityEngine;

public class MEMeterScript : Activity {

	//logic control
	public bool isSelected;
    private float finalWaitStart;
    public float secondsToCloseSession;
    public bool showInstructions;

	//memeter display
	public Texture2D memeterOut;
	public Texture2D[] memeterLevels;
	public Texture2D memeterSelected;

    //memeter levels in the texture from click
    private static int LEVEL_10 = 224;
    private static int LEVEL_9 = 280;
    private static int LEVEL_8 = 336;
    private static int LEVEL_7 = 392;
    private static int LEVEL_6 = 448;
    private static int LEVEL_5 = 508;
    private static int LEVEL_4 = 562;
    private static int LEVEL_3 = 618;
    private static int LEVEL_2 = 674;
    private static int LEVEL_1 = 730;
    
	//memeter and instructions text areas definition
	public Rect memeterArea;
    public float memeterScale;


	//instruction control
	public string instructions;

	// Use this for initialization
	public override void Start () {
        //dynamic positioning
        var xcenterOfMediaArea = this.Configurations.FullTextArea.x + this.Configurations.FullTextArea.width * 0.8f -
                                this.memeterOut.width * this.memeterScale / 2;
        var ycenterOfMediaArea = this.Configurations.FullTextArea.y + this.Configurations.FullTextArea.height / 2 -
                                 this.memeterOut.height * this.memeterScale / 2;
        this.memeterArea = new Rect(xcenterOfMediaArea, ycenterOfMediaArea, this.memeterOut.width * this.memeterScale, this.memeterOut.height * this.memeterScale);
        Setup("MeMeter");
	}
	
	// Update is called once per frame
	void Update () {
        //get the mmeter selected level
        if (Input.GetMouseButtonDown(0))
        {
            //left mouse click, check if it was inside the memeter
            float clickX = Input.mousePosition.x;
            float clickY = Screen.height - Input.mousePosition.y;
            Rect workingRect = GetMemeterArea();
            if (workingRect.Contains(new Vector2(clickX, clickY)))
            {
                bool emptySelected = false;
                //determine the level selected
                if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_10, memeterSelected))
                {
                    memeterSelected = memeterLevels[10];
                    Logger.Instance.LogInformation("Memeter selection level 10");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_9, memeterSelected))
                {
                    memeterSelected = memeterLevels[9];
                    Logger.Instance.LogInformation("Memeter selection level 9");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_8, memeterSelected))
                {
                    memeterSelected = memeterLevels[8];
                    Logger.Instance.LogInformation("Memeter selection level 8");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_7, memeterSelected))
                {
                    memeterSelected = memeterLevels[7];
                    Logger.Instance.LogInformation("Memeter selection level 7");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_6, memeterSelected))
                {
                    memeterSelected = memeterLevels[6];
                    Logger.Instance.LogInformation("Memeter selection level 6");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_5, memeterSelected))
                {
                    memeterSelected = memeterLevels[5];
                    Logger.Instance.LogInformation("Memeter selection level 5");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_4, memeterSelected))
                {
                    memeterSelected = memeterLevels[4];
                    Logger.Instance.LogInformation("Memeter selection level 4");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_3, memeterSelected))
                {
                    memeterSelected = memeterLevels[3];
                    Logger.Instance.LogInformation("Memeter selection level 3");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_2, memeterSelected))
                {
                    memeterSelected = memeterLevels[2];
                    Logger.Instance.LogInformation("Memeter selection level 2");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_1, memeterSelected))
                {
                    memeterSelected = memeterLevels[1];
                    Logger.Instance.LogInformation("Memeter selection level 1");
                }
                //empty level cannot be selected
                else
                {
                    emptySelected = true;
                    //memeterSelected = memeterOut;
                    Logger.Instance.LogInformation("Memeter selection level empty");
                }

                //start countdown for closing activity
                if (finalWaitStart == 0 && !emptySelected)
                {
                    finalWaitStart = Time.time;
                    isSelected = true;
                    SessionManager.PlayerScore += 4;
                    UIManagerScript.EnableSkipping();
                }
                Logger.Instance.LogInformation("Clicked on memeter area.");
            }
            else
            {
                Logger.Instance.LogInformation("Clicked outside memeter.");
            }
        }

        //check if the memeter introduction is canContinue
        //if (isSelected && (Time.time - finalWaitStart) >= MinimumWaitTime)
        //    canContinue = true;
	}

    //fix inverted mouse Y
    public static float DetermineScreenPositionBoundary(Rect areaRectangle, int nonScaledBoundary, Texture2D nonScaledTexture)
    {
        return areaRectangle.y + nonScaledBoundary * (areaRectangle.height / nonScaledTexture.height);
    }

    void OnGUI()
    {
        //draw the instructions text
        if (showInstructions)
            GUI.Label(this.Configurations.HalfTextArea, instructions, this.Configurations.BoxFormat);
        //draw the memeter frame
        GUI.DrawTexture(GetMemeterArea(), memeterSelected);
    }

    private Rect GetMemeterArea()
    {
        if (showInstructions)
            return memeterArea;
        else
        {
            float width = memeterArea.width;
            float height = memeterArea.height;
            return new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height);
        }
    }

    public void Setup(string description)
    {
		this.Name = description;
        finalWaitStart = 0;
        isSelected = false;
        this.CanContinue = false;
        this.SensorManager.StartNewActivity();
    }

	//TheEmoregulators usage
	public void Setup()
	{
		this.instructions = "";
		this.IsMultiLineSet = false;
	}
	
	public override void WriteInstruction(string instruction, bool isMultiLine)
	{
		if (!this.IsMultiLineSet) 
		{
			this.IsMultiLine = isMultiLine;
			this.IsMultiLineSet = true;
			
			this.instructions += instruction;
		}
		else
		{
			this.instructions += "\n\n" + instruction;
		}
		
	}
	
	public override void EnableEnd()
	{
	}
}
