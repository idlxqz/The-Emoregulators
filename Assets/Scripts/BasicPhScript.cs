using UnityEngine;
using System.Collections;

public class BasicPhScript : MonoBehaviour {

    //logic control
    private int assignedCards; 
    public bool finished;

    //draw control
    public Texture2D BCard;
    public Texture2D ACard;
    public Texture2D SCard;
    public Texture2D ICard;
    public Texture2D CCard;
    public Texture2D PhCard;

    public Texture2D slotBackground;
    public GUIStyle slotTextStyle;

    public int cardSlotHeight;
    public int cardSlotWidth;
    public int lateralOffset;
    public int cardsYInitialPos;
    public int slotsYInitialPos;
    
    private Rect BCardRect, ACardRect, SCardRect, ICardRect, CCardRect, PhCardRect;
    private Rect BSlotRect, ASlotRect, SSlotRect, ISlotRect, CSlotRect, PhSlotRect;
    private int cardWidth, cardHeight;

    //mouse control
    private string selected;
    private Rect selectedRect;
    private Rect initialRect;

    //centralized logging
    Logger log;

	// Use this for initialization
	void Start () {
        log = Logger.Instance;
        //logic
        assignedCards = 0;
        finished = false;

        //mouse selection
        selected = "";

        //configure all the positions
        float spacing = (Screen.width - 2 * lateralOffset - 6 * cardSlotWidth) / 5;
	    
        BCardRect = new Rect(lateralOffset, cardsYInitialPos, cardSlotWidth, cardSlotHeight);
        BSlotRect = new Rect(lateralOffset, slotsYInitialPos, cardSlotWidth, cardSlotHeight);

        ACardRect = new Rect(lateralOffset + cardSlotWidth + spacing, cardsYInitialPos, cardSlotWidth, cardSlotHeight);
        ASlotRect = new Rect(lateralOffset + cardSlotWidth + spacing, slotsYInitialPos, cardSlotWidth, cardSlotHeight);
        
        SCardRect = new Rect(lateralOffset + (cardSlotWidth + spacing) * 2, cardsYInitialPos, cardSlotWidth, cardSlotHeight);
        SSlotRect = new Rect(lateralOffset + (cardSlotWidth + spacing) * 2, slotsYInitialPos, cardSlotWidth, cardSlotHeight);

        ICardRect = new Rect(lateralOffset + (cardSlotWidth + spacing) * 3, cardsYInitialPos, cardSlotWidth, cardSlotHeight);
        ISlotRect = new Rect(lateralOffset + (cardSlotWidth + spacing) * 3, slotsYInitialPos, cardSlotWidth, cardSlotHeight);
        
        CCardRect = new Rect(lateralOffset + (cardSlotWidth + spacing) * 4, cardsYInitialPos, cardSlotWidth, cardSlotHeight);
        CSlotRect = new Rect(lateralOffset + (cardSlotWidth + spacing) * 4, slotsYInitialPos, cardSlotWidth, cardSlotHeight);
        
        PhCardRect = new Rect(lateralOffset + (cardSlotWidth + spacing) * 5, cardsYInitialPos, cardSlotWidth, cardSlotHeight);
        PhSlotRect = new Rect(lateralOffset + (cardSlotWidth + spacing) * 5, slotsYInitialPos, cardSlotWidth, cardSlotHeight);
	}
	
	// Update is called once per frame
	void Update () {
        //detect mouse click on cards
        if (Input.GetMouseButtonDown(0))
        {
            if (BCardRect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
            {
                selected = "B";
                initialRect = BCardRect;
            }
            else if (ACardRect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
                selected = "A";
                initialRect = ACardRect;
            }
            else if (SCardRect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
                selected = "S";
                initialRect = SCardRect;
            }
            else if (ICardRect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
                selected = "I";
                initialRect = ICardRect;
            }
            else if (CCardRect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
                selected = "C";
                initialRect = CCardRect;
            }
            else if (PhCardRect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
                selected = "Ph";
                initialRect = PhCardRect;
            }
        }
        //detect drag end
        else if (Input.GetMouseButtonUp(0))
        {
            if (selected != "")
            {
                Debug.Log("Drop with selection");
                //check if droped on something
                Rect selectedRectangle = GetSelected();

                if (RectsOverlap(BSlotRect, selectedRectangle))
                {
                    //TODO: attribute points
                    log.LogInformation("Dropped "+selected+" on B slot.");
                    MakeSelectedDisappear();
                }
                else if (RectsOverlap(ASlotRect, selectedRectangle))
                {
                    //TODO: attribute points
                    log.LogInformation("Dropped " + selected + " on A slot.");
                    MakeSelectedDisappear();
                }
                else if (RectsOverlap(SSlotRect, selectedRectangle))
                {
                    //TODO: attribute points
                    log.LogInformation("Dropped " + selected + " on S slot.");
                    MakeSelectedDisappear();
                }
                else if (RectsOverlap(ISlotRect, selectedRectangle))
                {
                    //TODO: attribute points
                    log.LogInformation("Dropped " + selected + " on I slot.");
                    MakeSelectedDisappear();
                }
                else if (RectsOverlap(CSlotRect, selectedRectangle))
                {
                    //TODO: attribute points
                    log.LogInformation("Dropped " + selected + " on C slot.");
                    MakeSelectedDisappear();
                }
                else if (RectsOverlap(PhSlotRect, selectedRectangle))
                {
                    //TODO: attribute points
                    log.LogInformation("Dropped " + selected + " on Ph slot.");
                    MakeSelectedDisappear();
                }

                //if not revert to the original position
                switch (selected)
                {
                    case "B":
                        BCardRect = initialRect;
                        break;
                    case "A":
                        ACardRect = initialRect;
                        break;
                    case "S":
                        SCardRect = initialRect;
                        break;
                    case "I":
                        ICardRect = initialRect;
                        break;
                    case "C":
                        CCardRect = initialRect;
                        break;
                    case "Ph":
                        PhCardRect = initialRect;
                        break;
                }
            }
        }
	}

    void OnGUI()
    {
        //update with mouse movements
        if (selected != "")
        {
            switch (selected)
            {
                case "B":
                    BCardRect.x = Input.mousePosition.x - cardSlotWidth / 2;
                    BCardRect.y = Screen.height - Input.mousePosition.y - cardSlotWidth / 2;
                    break;
                case "A":
                    ACardRect.x = Input.mousePosition.x - cardSlotWidth / 2;
                    ACardRect.y = Screen.height - Input.mousePosition.y - cardSlotWidth / 2;
                    break;
                case "S":
                    SCardRect.x = Input.mousePosition.x - cardSlotWidth / 2;
                    SCardRect.y = Screen.height - Input.mousePosition.y - cardSlotWidth / 2;
                    break;
                case "I":
                    ICardRect.x = Input.mousePosition.x - cardSlotWidth / 2;
                    ICardRect.y = Screen.height - Input.mousePosition.y - cardSlotWidth / 2;
                    break;
                case "C":
                    CCardRect.x = Input.mousePosition.x - cardSlotWidth / 2;
                    CCardRect.y = Screen.height - Input.mousePosition.y - cardSlotWidth / 2;
                    break;
                case "Ph":
                    PhCardRect.x = Input.mousePosition.x - cardSlotWidth / 2;
                    PhCardRect.y = Screen.height - Input.mousePosition.y - cardSlotWidth / 2;
                    break;
            }
        }

        //Draw the slots
        GUI.DrawTexture(BSlotRect, slotBackground);
        GUI.Label(BSlotRect, "B", slotTextStyle);
        GUI.DrawTexture(ASlotRect, slotBackground);
        GUI.Label(ASlotRect, "A", slotTextStyle);
        GUI.DrawTexture(SSlotRect, slotBackground);
        GUI.Label(SSlotRect, "S", slotTextStyle);
        GUI.DrawTexture(ISlotRect, slotBackground);
        GUI.Label(ISlotRect, "I", slotTextStyle);
        GUI.DrawTexture(CSlotRect, slotBackground);
        GUI.Label(CSlotRect, "C", slotTextStyle);
        GUI.DrawTexture(PhSlotRect, slotBackground);
        GUI.Label(PhSlotRect, "Ph", slotTextStyle);

        //Draw the cards
        GUI.DrawTexture(BCardRect, BCard);
        GUI.DrawTexture(ACardRect, ACard);
        GUI.DrawTexture(SCardRect, SCard);
        GUI.DrawTexture(ICardRect, ICard);
        GUI.DrawTexture(CCardRect, CCard);
        GUI.DrawTexture(PhCardRect, PhCard);
    }

    private bool RectsOverlap(Rect r1, Rect r2)
    {
        var widthOverlap = (r1.xMin >= r2.xMin) && (r1.xMin <= r2.xMax) ||
                        (r2.xMin >= r1.xMin) && (r2.xMin <= r1.xMax);

        var heightOverlap = (r1.yMin >= r2.yMin) && (r1.yMin <= r2.yMax) ||
                        (r2.yMin >= r1.yMin) && (r2.yMin <= r1.yMax);

        return widthOverlap && heightOverlap;
    }

    //gets the selected card area rectangle, if any 
    private Rect GetSelected()
    {
        switch (selected)
        {
            case "B":
                return BCardRect;
            case "A":
                return ACardRect;
            case "S":
                return SCardRect;
            case "I":
                return ICardRect;
            case "C":
                return CCardRect;
            case "Ph":
                return PhCardRect;
            default:
                return new Rect(0,0,0,0);
        }
    }

    //move the selected card out of view
    private void MakeSelectedDisappear()
    {
        if (selected != "")
        {
            switch (selected)
            {
                case "B":
                    BCardRect.y = Screen.height + 2000;
                    break;
                case "A":
                    ACardRect.y = Screen.height + 2000;
                    break;
                case "S":
                    SCardRect.y = Screen.height + 2000;
                    break;
                case "I":
                    ICardRect.y = Screen.height + 2000;
                    break;
                case "C":
                    CCardRect.y = Screen.height + 2000;
                    break;
                case "Ph":
                    PhCardRect.y = Screen.height + 2000;
                    break;
            }
            //mark one more card as assigned
            assignedCards++;
            if (assignedCards == 6)
                finished = true; // end of activity
            //deselect the card
            selected = "";
        }
    }
}
