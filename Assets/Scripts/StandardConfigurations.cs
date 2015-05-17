using UnityEngine;

public class StandardConfigurations : MonoBehaviour
{
    public int HorizontalOffset;
    public int VerticalOffset;
    public Rect HalfTextArea;
    public Rect FullTextArea;

    public GUIStyle InstructionsFormat;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        this.FullTextArea = new Rect(this.HorizontalOffset, this.VerticalOffset, Screen.width - 2 * this.HorizontalOffset, Screen.height - 2 * this.VerticalOffset);

        this.HalfTextArea = new Rect(this.HorizontalOffset, this.VerticalOffset, Screen.width * 0.6f - this.HorizontalOffset, Screen.height - 2 * this.VerticalOffset);
    }
    
	// Use this for initialization
	public void Start ()
	{
	    
	
	}
}
