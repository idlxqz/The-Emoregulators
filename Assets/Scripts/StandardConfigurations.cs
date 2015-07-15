using UnityEngine;

public class StandardConfigurations : MonoBehaviour
{
    public const int HorizontalOffset = 50;
    public const int VerticalOffset = 110;
    
    public Rect HalfTextArea;
    public Rect FullTextArea;
    public Texture2D TextBoxTexture;

    public GUIStyle InstructionsFormat;
    public GUIStyle BoxFormat;
    public GUIStyle TitleFormat;
    public GUIStyle SubTitleFormat;
    public GUIStyle ActivityFormat;
    public GUIStyle ScoreFormat;
    public GUIStyle HelpFormat;

	public static bool IsTheEmoregulatorsAssistantActive; 
	

    public void Awake()
    {
        DontDestroyOnLoad(this);

        this.FullTextArea = new Rect(HorizontalOffset, VerticalOffset, Screen.width - 2 * HorizontalOffset, Screen.height - 2 *VerticalOffset + 10);
        this.HalfTextArea = new Rect(HorizontalOffset, VerticalOffset, Screen.width * 0.6f - HorizontalOffset, Screen.height - 2 *VerticalOffset + 10);

        this.BoxFormat = null;
    }
    
	// Use this for initialization
	public void Start ()
	{
	}

    public void OnGUI()
    {
        
        if (this.BoxFormat == null)
        {
            this.BoxFormat = new GUIStyle(GUI.skin.box);
            this.BoxFormat.normal.background = this.TextBoxTexture;
            this.BoxFormat.normal.textColor = Color.black;
            this.BoxFormat.font = this.InstructionsFormat.font;
            this.BoxFormat.fontSize = this.InstructionsFormat.fontSize;
            this.BoxFormat.alignment = TextAnchor.MiddleLeft;
            this.BoxFormat.wordWrap = true;
            this.BoxFormat.padding.left = 15;
            this.BoxFormat.padding.right = 15;
            this.BoxFormat.padding.top = 15;

            GUI.skin.box.stretchHeight = true;
        }

        
    }
}
