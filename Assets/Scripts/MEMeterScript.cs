using UnityEngine;
using System.Collections;

public class MEMeterScript : MonoBehaviour {

	//logic control
	public bool finished;
	public bool isSelected;
    private float finalWaitStart;
    public float secondsToCloseSession;
    public bool showInstructions;

	//memeter display
	public Texture2D memeterOut;
	public Texture2D[] memeterLevels;
	public Texture2D memeterSelected;

    //memeter levels in the texture from click
    private static int LEVEL_9 = 120;
    private static int LEVEL_8 = 199;
    private static int LEVEL_7 = 276;
    private static int LEVEL_6 = 354;
    private static int LEVEL_5 = 434;
    private static int LEVEL_4 = 512;
    private static int LEVEL_3 = 590;
    private static int LEVEL_2 = 670;
    private static int LEVEL_1 = 748;
    private static int LEVEL_0 = 826;

	//memeter and instructions text areas definition
	public Rect memeterArea;
	public Rect instructionsArea;

	//centralized logging
	public Logger log;

	//instruction control
	public string instructions;
	
	//instructions format
	public GUIStyle instructionsFormat;

	// Use this for initialization
	void Start () {
        Setup();
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
                //determine the level selected
                if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_9, memeterSelected))
                {
                    memeterSelected = memeterLevels[9];
                    log.LogInformation("Memeter selection level 9");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_8, memeterSelected))
                {
                    memeterSelected = memeterLevels[8];
                    log.LogInformation("Memeter selection level 8");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_7, memeterSelected))
                {
                    memeterSelected = memeterLevels[7];
                    log.LogInformation("Memeter selection level 7");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_6, memeterSelected))
                {
                    memeterSelected = memeterLevels[6];
                    log.LogInformation("Memeter selection level 6");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_5, memeterSelected))
                {
                    memeterSelected = memeterLevels[5];
                    log.LogInformation("Memeter selection level 5");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_4, memeterSelected))
                {
                    memeterSelected = memeterLevels[4];
                    log.LogInformation("Memeter selection level 4");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_3, memeterSelected))
                {
                    memeterSelected = memeterLevels[3];
                    log.LogInformation("Memeter selection level 3");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_2, memeterSelected))
                {
                    memeterSelected = memeterLevels[2];
                    log.LogInformation("Memeter selection level 2");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_1, memeterSelected))
                {
                    memeterSelected = memeterLevels[1];
                    log.LogInformation("Memeter selection level 1");
                }
                else if (clickY < DetermineScreenPositionBoundary(workingRect, LEVEL_0, memeterSelected))
                {
                    memeterSelected = memeterLevels[0];
                    log.LogInformation("Memeter selection level 0");
                }
                else
                {
                    memeterSelected = memeterOut;
                    log.LogInformation("Memeter selection level empty");
                }

                //start countdown for closing activity
                if (finalWaitStart == 0)
                {
                    finalWaitStart = Time.time;
                    isSelected = true;
                }
                log.LogInformation("Clicked on memeter area.");
            }
            else
            {
                log.LogInformation("Clicked outside memeter.");
            }
        }

        //check if the memeter introduction is finished
        if (isSelected && (Time.time - finalWaitStart) >= secondsToCloseSession)
            finished = true;
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
            GUI.Label(instructionsArea, instructions, instructionsFormat);
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

    public void Setup()
    {
        finalWaitStart = 0;
        isSelected = false;
        finished = false;
    }
}
