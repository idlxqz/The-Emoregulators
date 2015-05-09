using UnityEngine;
using System.Collections;

public class BackgroundChooserScript : MonoBehaviour {

    //logic control
    public bool finished;
    public System.Action<Texture2D> backgroundSetter = new System.Action<Texture2D>((tex) => { });

    //draw control
    public Texture2D[] backgrounds;

    //backgroudn display configuration
    Rect[] galleryRects;
    Rect nextArea;
    Rect previousArea;
    public int lateraOffset;
    public int itemSpacing;
    int selectedBAckgroundsSet;

    //plug in logging
    Logger log;

	// Use this for initialization
	void Start () {
        //load all existing textures
        var backs = Resources.LoadAll<Texture2D>("Backgrounds");

        backgrounds = new Texture2D[backs.Length];
        int i = 0;
        foreach (var item in backs)
        {
            backgrounds[i++] = item;
        }
        //configure the display of the possible backgrounds
        int availableSpace = Screen.width - 2 * lateraOffset - itemSpacing * 2;
        int itemSide = availableSpace / 3;
        int topRowY = Screen.height / 5;

        galleryRects = new Rect[6];
        //top row
        galleryRects[0] = new Rect(lateraOffset, topRowY, itemSide, itemSide);
        galleryRects[1] = new Rect(lateraOffset + itemSide + itemSpacing, topRowY, itemSide, itemSide);
        galleryRects[2] = new Rect(lateraOffset + (itemSide + itemSpacing) * 2, topRowY, itemSide, itemSide);
        //bottom row
        galleryRects[3] = new Rect(lateraOffset, topRowY + itemSide + itemSpacing, itemSide, itemSide);
        galleryRects[4] = new Rect(lateraOffset + itemSide + itemSpacing, topRowY + itemSide + itemSpacing, itemSide, itemSide);
        galleryRects[5] = new Rect(lateraOffset + (itemSide + itemSpacing) * 2, topRowY + itemSide + itemSpacing, itemSide, itemSide);
        //buttons
        int buttonWidth = 100;
        int buttonHeight = 50;
        previousArea = new Rect(lateraOffset, topRowY + (itemSide + itemSpacing) * 2, buttonWidth, buttonHeight);
        nextArea = new Rect(Screen.width - lateraOffset - buttonWidth, topRowY + (itemSide + itemSpacing) * 2, buttonWidth, buttonHeight);
        selectedBAckgroundsSet = 0;

        log = Logger.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        //detect background selection
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickedPosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

            for (int i = 0; i < galleryRects.Length; i++)
            {
                if (galleryRects[i].Contains(clickedPosition) && CanShowGalleryItem(i))
                {
                    backgroundSetter(backgrounds[selectedBAckgroundsSet + i]);
                    log.LogInformation("Selected background: " + backgrounds[selectedBAckgroundsSet + i]);
                }
            }
        }
	}

    void OnGUI()
    {
        //display currently selectable backgrounds
        for (int i = 0; i < galleryRects.Length; i++)
        {
            if (CanShowGalleryItem(i))
                GUI.DrawTexture(galleryRects[i], backgrounds[selectedBAckgroundsSet + i]);
        }
        //control displayed images
        if(canClickPrevious()){
            if(GUI.Button(previousArea, "Previous")){
                selectedBAckgroundsSet -= 6;
            }
        }
        if (canClickNext())
        {
            if(GUI.Button(nextArea, "Next")){
                selectedBAckgroundsSet += 6;
            }
        }
    }

    private bool CanShowGalleryItem(int _item)
    {
        if (selectedBAckgroundsSet + _item < backgrounds.Length)
            return true;
        return false;
    }

    private bool canClickNext()
    {
        if (selectedBAckgroundsSet + 6 >= backgrounds.Length)
            return false;
        return true;
    }

    private bool canClickPrevious()
    {
        if (selectedBAckgroundsSet - 6 < 0)
            return false;
        return true;
    }
}
