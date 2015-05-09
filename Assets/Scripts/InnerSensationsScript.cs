using UnityEngine;
using System.Collections;

public class InnerSensationsScript : MonoBehaviour {

    //game logic
    public bool finished;

    //swatches & boxes
    public Texture2D[] swatches;
    public Texture2D[] iboxes;
    public Texture2D selectedIbox;
    
    //positioning
    public int lateralOffset;
    public int selectedIboxY;
    public int selectedIboxHeight;
    public int swatchBottomSpacing;
    public int swatchSpacing;
    Rect selectedIboxRect;
    Rect[] swatchRects;

    //logging
    Logger log;

	// Use this for initialization
	void Start () {
        log = Logger.Instance;
        //prepare the swatches rectangles
        int swCount = swatches.Length;
        swatchRects = new Rect[swCount];
        int swatchWidth = (Screen.width - 2 * lateralOffset - (swCount - 1) * swatchSpacing) / swCount;
        int swatchHeight = (swatches[0].height * swatchWidth) / swatches[0].width;

        for (int i = 0; i < swCount; i++)
        {
            swatchRects[i] = new Rect(
                lateralOffset + i * (swatchWidth + swatchSpacing), 
                Screen.height - swatchHeight - swatchBottomSpacing,
                swatchWidth,
                swatchHeight);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < swatchRects.Length; i++ )
            {
                if (swatchRects[i].Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
                {
                    selectedIbox = iboxes[i];
                    log.LogInformation("Selected box: "+selectedIbox.name);
                    UIManagerScript.EnableSkipping();
                }
            }
        }
	}

    void OnGUI()
    {
        //draw the main ibox
        int width = (selectedIbox.width * selectedIboxHeight) / selectedIbox.height;
        selectedIboxRect = new Rect(Screen.width / 2 - width / 2, selectedIboxY, width, selectedIboxHeight);

        GUI.DrawTexture(selectedIboxRect, selectedIbox);

        //draw the swatches
        for (int i = 0; i < swatches.Length; i++ )
        {
            GUI.DrawTexture(swatchRects[i], swatches[i]);
        }
    }
}
