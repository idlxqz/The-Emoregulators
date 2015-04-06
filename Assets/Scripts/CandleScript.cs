using UnityEngine;
using System.Collections;

public class CandleScript : MonoBehaviour {

	//candle cerimony control
	public bool finished;
	public bool isLit;
    public bool simpleCandleAnimation;
	//candle animation control
	public Texture2D[] frames;
	public float frameChangeInterval; //in seconds
	public float lightinUpSeconds; //in seconds
	public float animationTime; //in seconds
	private int selectedFrame;
	private float previousFrameTimestamp;
	private bool lightingUp;
	private float lightingUpStart;

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
				selectedFrame = Random.Range(3, 5); //fully lit frames
			}
			else
				selectedFrame = 0; //unlit frame
			//update timestamp
			previousFrameTimestamp = Time.time;
		}
			
		//get the lighting action for the candle
		if (Input.GetMouseButtonDown(0) && !isLit){
			//left mouse click, check if it was inside the candle
            if (GetCandleArea().Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
            {
				lightingUpStart = Time.time;
				isLit = true;
				log.LogInformation("Clicked on candle area.");
			}
			else{
				log.LogInformation("Clicked outside candle.");
			}
		}

		//check if the candle cerimony is finished
		if(isLit && (Time.time - lightingUpStart) >= animationTime)
			finished = true;
	}

	void OnGUI() {
		//draw the candle cerimony text
        if (!simpleCandleAnimation)
		    GUI.Label(instructionsArea, instructions, instructionsFormat);
		//draw the candle frame
        GUI.DrawTexture(GetCandleArea(), frames[selectedFrame]);
	}

    private Rect GetCandleArea(){
        if (simpleCandleAnimation)
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
        if (simpleCandleAnimation)
        {
            isLit = true;
            lightingUp = false;
            lightingUpStart = Time.time;
        }
    }

}
