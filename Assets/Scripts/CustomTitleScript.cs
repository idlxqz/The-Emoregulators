using UnityEngine;
using System.Collections;

public class CustomTitleScript : MonoBehaviour {

    //logic
    public bool finished;
    public int titleDuration;
    private float titleStart;
    public System.Action setupNextPhase;

    //information
    private string title;

    //formating
    public int titleLateralPadding;
    public int titleRectHeight;
    public GUIStyle titleFormat;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if ((Time.time - titleStart) < titleDuration && !finished)
        {
            GUI.Label(
                    new Rect(titleLateralPadding, Screen.height / 2 - titleRectHeight / 2, Screen.width - titleLateralPadding * 2, titleRectHeight),
                    title,
                    titleFormat);
        }
        else
        {
            finished = true;
        }
    }

    public void Setup(System.Action _nextPhase, string _title){
        setupNextPhase = _nextPhase;
        finished = false;
        title = _title;
        titleStart = Time.time;
    }
}
