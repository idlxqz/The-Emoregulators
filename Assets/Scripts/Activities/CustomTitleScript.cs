using UnityEngine;
using System.Collections;

public class CustomTitleScript : Activity {

    //logic
    public bool endlessTitle;
    public int titleDuration;
    private float titleStart;
    public System.Action setupNextPhase;
    private int lastChar;
    private float loadCharTimer;
    public float charLoadInterval;
    private bool loading;

    //information
    private string title;
    private string loadedText;
    private char[] charsToShow;

    //formating
    public int titleLateralPadding;
    public int titleRectHeight;
    public GUIStyle titleFormat;

    protected override void Awake()
    {
        base.Awake();
        this.Setup(null, "The Emoregulators");
    }

    void OnGUI()
    {
        if (lastChar < charsToShow.Length && (Time.time - loadCharTimer) > charLoadInterval)
        {
            //load another char
            loadedText += charsToShow[lastChar++];
            //check if loading is done
            if (lastChar == charsToShow.Length)
            {
                titleStart = Time.time; // start final countdown
                loading = false;
            }
            loadCharTimer = Time.time;
        }
        if (loading || this.endlessTitle || ((Time.time - titleStart) < titleDuration && !this.CanContinue))
        {   
            GUI.Label(
                    new Rect(titleLateralPadding, Screen.height / 2 - titleRectHeight / 2, Screen.width - titleLateralPadding * 2, titleRectHeight),
                    loadedText,
                    titleFormat);
        }
        else
        {
            this.CanContinue = true;
        }
    }

    public void Setup(System.Action _nextPhase, string _title){
        setupNextPhase = _nextPhase;
        this.CanContinue = false;
        title = _title;
        lastChar = 0;
        charsToShow = title.ToCharArray();
        loading = true;
        loadedText = "";
        loadCharTimer = Time.time;
    }
}
