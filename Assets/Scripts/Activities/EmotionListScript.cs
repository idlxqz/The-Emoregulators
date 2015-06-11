using UnityEngine;

public class EmotionListScript : CustomTextScript {

    //emotions specification
    string[] emotions;
    public Texture2D[] emotionColorTextures;
    public HowDoesMyBodyFeelScript howDoesMyBodyFeel;

    
	// Use this for initialization
	public override void Start ()
	{
        SensorManager.StartNewActivity();
	    this.howDoesMyBodyFeel = GameObject.Find("HowDoesMyBodyFeel").GetComponent<HowDoesMyBodyFeelScript>();
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
            emotionColorTextures[i] = GenerateTextureForColor(howDoesMyBodyFeel.emotionColors[i]);

        base.Start();
    }

    void OnGUI()
    {
        //draw the instructions text
        GUI.Label(this.Configurations.HalfTextArea, this.currentInstructions, this.Configurations.BoxFormat);

        //draw the emotion options
        for (int i = 0; i < emotions.Length; i++ )
        {
            GUIStyle emotionStyle = new GUIStyle(howDoesMyBodyFeel.emotionButtonFormatTemplate);
            emotionStyle.normal.background = emotionColorTextures[i];
            //emotionStyle.normal.background.co
            if (GUI.Button(GetOptionRect(i), emotions[i], emotionStyle)) { }; //button is a dummy
        }
    }

    private Rect GetOptionRect(int option)
    {
        int drawStart = Screen.height / 5;
        float availableVerticalSpace = Screen.height - (2 * Screen.height) / 5 - howDoesMyBodyFeel.optionHeight * emotions.Length;
        float verticalSpacing = availableVerticalSpace / emotions.Length;

        var xcenterOfMediaArea = this.Configurations.FullTextArea.x + this.Configurations.FullTextArea.width * 0.8f -howDoesMyBodyFeel.optionWidth/2;
        //var ycenterOfMediaArea = this.Configurations.FullTextArea.y + this.Configurations.FullTextArea.height / 2 - optionHeight/2;
        return new Rect(xcenterOfMediaArea, drawStart + (howDoesMyBodyFeel.optionHeight + verticalSpacing) * option, howDoesMyBodyFeel.optionWidth, howDoesMyBodyFeel.optionHeight);
    }

   

    private Texture2D GenerateTextureForColor(Color _toGenerate, int width = 1, int height = 1)
    {
        var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);

        // set the pixel values
        for (int i = 0; i < width; i++ )
            for (int j = 0; j < width; j++)
                texture.SetPixel(i, j, new Color(_toGenerate.r, _toGenerate.g, _toGenerate.b, howDoesMyBodyFeel.colorAlpha));
            
        // Apply all SetPixel calls
        texture.Apply();

        return texture;
    }
}
