using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HowDoesMyBodyFeelScript : Activity {

    //logic control
    public SessionManager.Gender playerGender;
    int emotionSelected;
    bool hasEmotionSelected;

    //drawing areas control
    float avatarScreenshotScale;
    public int optionHeight;
    public int optionWidth;
    Rect draggedEmotionRect;

    //emotions specification
    string[] emotions;
    public Color[] emotionColors;
    public float colorAlpha;
    public Texture2D[] emotionColorTextures;
    public GUIStyle emotionButtonFormatTemplate;
    public int colorDragSize;

    //body droppable areas
    enum BodyArea
    {
        Head,
        Heart,
        Chest,
        Belly,
        ArmLeft,
        ArmRight,
        HandLeft,
        HandRight,
        Legs,
        Feet
    }
    Dictionary<BodyArea, Rect> droppableRects;
    Dictionary<BodyArea, Texture2D> bodyAreasOverlays;
    Dictionary<BodyArea, string> bodyEmotions; 

    //graphics
    public Texture2D maleAvatar;
    public Texture2D femaleAvatar;

	// Use this for initialization
	public override void Start () {
        //setup the emotions strings
        emotions = new string[6];
        emotions[0] = GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelHappiness);
        emotions[1] = GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelSadness);
        emotions[2] = GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelAnger);
        emotions[3] = GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelFear);
        emotions[4] = GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelDisgust);
        emotions[5] = GlobalizationService.Instance.Globalize(GlobalizationService.HowDoesMyBodyFeelSurprise);

        //generate a texture with the specified color for each emotion so we can use later on to do masks
        emotionColorTextures = new Texture2D[6];
        for (int i = 0; i < emotions.Length; i++ )
            emotionColorTextures[i] = GenerateTextureForColor(emotionColors[i]);

        //basic dragging settings
        draggedEmotionRect.width = colorDragSize;
        draggedEmotionRect.height = colorDragSize;

        SetupDroppableRects();
        SetupBodyAreasOverlays();

        avatarScreenshotScale = 1;

        this.SensorManager.StartNewActivity();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 mouseScreenPosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        //detect mouse click
        if (Input.GetMouseButtonDown(0) && !hasEmotionSelected)
        {
            //detect clikcing on the emotions
            for (int i = 0; i < emotions.Length; i++)
            {
                if (GetOptionRect(i).Contains(mouseScreenPosition))
                {
                    //clicked on emotion i
                    hasEmotionSelected = true;
                    emotionSelected = i;
                    break;
                }
            }
        }
        //detect drag end
        else if (Input.GetMouseButtonUp(0) && hasEmotionSelected)
        {
            foreach (var item in droppableRects)
            {
                if (RectsOverlap(item.Value, draggedEmotionRect))
                {
                    if (!this.CanContinue)
                    {
                        this.CanContinue = true;
                        UIManagerScript.EnableSkipping();
                        SessionManager.PlayerScore += 10;
                    }
                   
                    //dropped on this item
                    bodyAreasOverlays[item.Key] = emotionColorTextures[emotionSelected];
                    //special cases
                    if (item.Key == BodyArea.ArmLeft || item.Key == BodyArea.ArmRight)
                    {
                        bodyAreasOverlays[BodyArea.ArmLeft] = emotionColorTextures[emotionSelected];
                        bodyAreasOverlays[BodyArea.ArmRight] = emotionColorTextures[emotionSelected];
                        bodyEmotions[BodyArea.ArmLeft] = emotions[emotionSelected];
                        bodyEmotions[BodyArea.ArmRight] = emotions[emotionSelected];
                    }
                    else if (item.Key == BodyArea.HandLeft || item.Key == BodyArea.HandRight)
                    {
                        bodyAreasOverlays[BodyArea.HandLeft] = emotionColorTextures[emotionSelected];
                        bodyAreasOverlays[BodyArea.HandRight] = emotionColorTextures[emotionSelected];
                        bodyEmotions[BodyArea.HandLeft] = emotions[emotionSelected];
                        bodyEmotions[BodyArea.HandRight] = emotions[emotionSelected];
                    }
                    //look no more so we can have priorities in when we have overlapping areas
                    break;
                }
            }
            //clear selection
            hasEmotionSelected = false;
            emotionSelected = -1;
        }
	}

    void OnGUI()
    {
        //draw the correct avatar
        if (playerGender == SessionManager.Gender.Female)
        {
            GUI.DrawTexture(GetAvatarRect(femaleAvatar), femaleAvatar);
        }
        else
        {
            GUI.DrawTexture(GetAvatarRect(maleAvatar), maleAvatar);
        }

        //draw the overlays and enable the user clearing them
        List<BodyArea> toClear = new List<BodyArea>();
        foreach (var item in bodyAreasOverlays.Reverse())
        {
            //if there is an overlay to be drawn
            if (item.Value != null)
            {
                GUI.DrawTexture(droppableRects[item.Key], item.Value);
                if(GUI.Button(droppableRects[item.Key], "", GUIStyle.none)){
                    //generic clearing
                    toClear.Add(item.Key);
                    //special cases
                    if (item.Key == BodyArea.ArmLeft || item.Key == BodyArea.ArmRight)
                    {
                        toClear.Add(BodyArea.ArmLeft);
                        toClear.Add(BodyArea.ArmRight);
                    }
                    else if (item.Key == BodyArea.HandLeft || item.Key == BodyArea.HandRight)
                    {
                        toClear.Add(BodyArea.HandLeft);
                        toClear.Add(BodyArea.HandRight);
                    }
                }
            }
        }
        //clear clicked overlays
        foreach (BodyArea clearing in toClear)
        {
            bodyAreasOverlays[clearing] = null;
            bodyEmotions[clearing] = "None";
        }

        //draw the emotion options
        for (int i = 0; i < emotions.Length; i++ )
        {
            GUIStyle emotionStyle = new GUIStyle(emotionButtonFormatTemplate);
            emotionStyle.normal.background = emotionColorTextures[i];
            //emotionStyle.normal.background.co
            if (GUI.Button(GetOptionRect(i), emotions[i], emotionStyle)) { }; //button is a dummy
        }

        //emotion dragging graphics
        if (hasEmotionSelected)
        {
            draggedEmotionRect.x = Input.mousePosition.x - colorDragSize / 2;
            draggedEmotionRect.y = Screen.height - Input.mousePosition.y - colorDragSize / 2;

            GUI.DrawTexture(draggedEmotionRect, GenerateTextureForColor(emotionColors[emotionSelected], colorDragSize, colorDragSize));            
        }
    }

    private Rect GetOptionRect(int option)
    {
        int drawStart = Screen.height / 5;
        float availableVerticalSpace = Screen.height - (2 * Screen.height) / 5 - optionHeight * emotions.Length;
        float verticalSpacing = availableVerticalSpace / emotions.Length;

        Rect selectedRect = new Rect(
            Screen.width / 5,
            drawStart + (optionHeight + verticalSpacing) * option,
            optionWidth,
            optionHeight);
        return selectedRect;
    }

    private Rect GetAvatarRect(Texture2D _avatarScreenshot)
    {
        int divider = 8;
        float screenshotHeight = Screen.height - (2 * Screen.height) / divider;
        avatarScreenshotScale = screenshotHeight / _avatarScreenshot.height;

        Rect avatarRect = new Rect(
            Screen.width - _avatarScreenshot.width * avatarScreenshotScale - Screen.width / divider,
            Screen.height / divider,
            _avatarScreenshot.width * avatarScreenshotScale,
            screenshotHeight);

        return avatarRect;
    }

    private Texture2D GenerateTextureForColor(Color _toGenerate, int width = 1, int height = 1)
    {
        var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);

        // set the pixel values
        for (int i = 0; i < width; i++ )
            for (int j = 0; j < width; j++)
                texture.SetPixel(i, j, new Color(_toGenerate.r, _toGenerate.g, _toGenerate.b, colorAlpha));
            
        // Apply all SetPixel calls
        texture.Apply();

        return texture;
    }

    private void SetupDroppableRects(){
        Rect avatarRect;
        //draw the correct avatar
        if (playerGender == SessionManager.Gender.Female)
        {
            avatarRect = GetAvatarRect(femaleAvatar);
            //setup the correct areas
            droppableRects = new Dictionary<BodyArea, Rect>()
            {
                {BodyArea.Head, new Rect(30, 0, 128, 122)},
                {BodyArea.Heart, new Rect(100, 161, 24, 42)},
                {BodyArea.Chest, new Rect(57, 132, 76, 103)},
                {BodyArea.ArmLeft, new Rect(137, 136, 55, 168)},
                {BodyArea.ArmRight, new Rect(0, 136, 56, 162)},
                {BodyArea.HandLeft, new Rect(150, 305, 42, 56)},
                {BodyArea.HandRight, new Rect(0, 300, 34, 54)},
                {BodyArea.Belly, new Rect(61, 239, 73, 60)},
                {BodyArea.Legs, new Rect(41, 329, 107, 246)},
                {BodyArea.Feet, new Rect(42, 580, 113, 43)}
            };
        }
        else
        {
            avatarRect = GetAvatarRect(maleAvatar);
            //setup the correct areas
            droppableRects = new Dictionary<BodyArea, Rect>()
            {
                {BodyArea.Head, new Rect(55, 0, 90, 109)},
                {BodyArea.Heart, new Rect(104, 143, 26, 43)},
                {BodyArea.Chest, new Rect(62, 120, 81, 102)},
                {BodyArea.ArmLeft, new Rect(149, 135, 37, 159)},
                {BodyArea.ArmRight, new Rect(0, 131, 56, 154)},
                {BodyArea.HandLeft, new Rect(160, 297, 30, 65)},
                {BodyArea.HandRight, new Rect(0, 294, 33, 59)},
                {BodyArea.Belly, new Rect(60, 223, 86, 78)},
                {BodyArea.Legs, new Rect(46, 326, 112, 260)},
                {BodyArea.Feet, new Rect(40, 592, 128, 32)}
            };
        }
        //update the rectangles to include the proper avatar positioning and scale
        Dictionary<BodyArea, Rect> adjustedRects = new Dictionary<BodyArea, Rect>();
        foreach (var item in droppableRects)
        {
            adjustedRects.Add(
                item.Key, 
                new Rect(
                    avatarRect.x + item.Value.x * avatarScreenshotScale,
                    avatarRect.y + item.Value.y * avatarScreenshotScale,
                    item.Value.width * avatarScreenshotScale,
                    item.Value.height * avatarScreenshotScale));
        }
        droppableRects = adjustedRects;
    }

    private void SetupBodyAreasOverlays()
    {
        Texture2D initialTexture = null;
        bodyAreasOverlays = new Dictionary<BodyArea, Texture2D>(){
            {BodyArea.Head, initialTexture},
            {BodyArea.Chest, initialTexture},
            {BodyArea.Heart, initialTexture},
            {BodyArea.Belly, initialTexture},
            {BodyArea.ArmLeft, initialTexture},
            {BodyArea.ArmRight, initialTexture},
            {BodyArea.HandLeft, initialTexture},
            {BodyArea.HandRight, initialTexture},
            {BodyArea.Legs, initialTexture},
            {BodyArea.Feet, initialTexture}
        };
        bodyEmotions = new Dictionary<BodyArea, string>()
        {
            {BodyArea.Head, "None"},
            {BodyArea.Chest, "None"},
            {BodyArea.Heart, "None"},
            {BodyArea.Belly, "None"},
            {BodyArea.ArmLeft, "None"},
            {BodyArea.ArmRight, "None"},
            {BodyArea.HandLeft, "None"},
            {BodyArea.HandRight, "None"},
            {BodyArea.Legs, "None"},
            {BodyArea.Feet, "None"}
        };
    }

    public void Setup(string description, SessionManager.Gender _playerGender)
    {
        this.Description = description;
        this.CanContinue = false;
        playerGender = _playerGender;
        hasEmotionSelected = false;
        emotionSelected = -1;
        draggedEmotionRect.width = optionWidth;
        draggedEmotionRect.height = optionHeight;
        SetupBodyAreasOverlays();
        SetupDroppableRects();
    }

    private bool RectsOverlap(Rect r1, Rect r2)
    {
        var widthOverlap = (r1.xMin >= r2.xMin) && (r1.xMin <= r2.xMax) ||
                        (r2.xMin >= r1.xMin) && (r2.xMin <= r1.xMax);

        var heightOverlap = (r1.yMin >= r2.yMin) && (r1.yMin <= r2.yMax) ||
                        (r2.yMin >= r1.yMin) && (r2.yMin <= r1.yMax);

        return widthOverlap && heightOverlap;
    }

    public override void EndActivity()
    {
        foreach (var bodyEmotion in this.bodyEmotions)
        {
            Logger.Instance.LogInformation("BodyPart: " + bodyEmotion.Key + " emotion: " + bodyEmotion.Value);
        }
        base.EndActivity();
    }
}
