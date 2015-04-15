using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BasicPhScript : MonoBehaviour {

    public bool finished;
    public int lateralOffset;

    public GameObject BCard;
    public GameObject ACard;
    public GameObject SCard;
    public GameObject ICard;
    public GameObject CCard;
    public GameObject PhCard;

    public int dropYPos;
    public Texture2D dropBackground;
    public int dropAreaSize;
    public GUIStyle textFormat;

    private Rect BDropArea;
    private Rect ADropArea;
    private Rect SDropArea;
    private Rect IDropArea;
    private Rect CDropArea;
    private Rect PhDropArea;
    public GameObject BDrop;

	// Use this for initialization
	void Start () {
	    //properly position the child cards
        int cardWidth = this.GetComponentInChildren<Image>().mainTexture.width;//assume the same for all cards
        int cardHeight = this.GetComponentInChildren<Image>().mainTexture.height;//assume the same for all cards
        int totalHorizontalSpace = Screen.width;
        Transform tmp;
        //B card
        tmp = BCard.transform;
        tmp.Translate(new Vector3(-tmp.position.x + lateralOffset , 0, 0));
        //BDrop.transform.position = new Vector3(tmp.position.x, dropYPos, 0);
        BDropArea = new Rect(lateralOffset - dropAreaSize / 2, dropYPos - dropAreaSize / 2, dropAreaSize, dropAreaSize);
        //A card
        tmp = ACard.transform;
        tmp.Translate(new Vector3(-tmp.position.x + lateralOffset + totalHorizontalSpace / 6, 0, 0));
        ADropArea = new Rect(lateralOffset + totalHorizontalSpace / 6 - dropAreaSize / 2, dropYPos - dropAreaSize / 2, dropAreaSize, dropAreaSize);
        //S card
        tmp = SCard.transform;
        tmp.Translate(new Vector3(-tmp.position.x + lateralOffset + 2 * (totalHorizontalSpace / 6), 0, 0));
        SDropArea = new Rect(lateralOffset + 2 * (totalHorizontalSpace / 6) - dropAreaSize / 2, dropYPos - dropAreaSize / 2, dropAreaSize, dropAreaSize);
        //I card
        tmp = ICard.transform;
        tmp.Translate(new Vector3(-tmp.position.x + lateralOffset + 3 * (totalHorizontalSpace / 6), 0, 0));
        IDropArea = new Rect(lateralOffset +3 * (totalHorizontalSpace / 6) - dropAreaSize / 2, dropYPos - dropAreaSize / 2, dropAreaSize, dropAreaSize);
        //C card
        tmp = CCard.transform;
        tmp.Translate(new Vector3(-tmp.position.x + lateralOffset + 4 * (totalHorizontalSpace / 6), 0, 0));
        CDropArea = new Rect(lateralOffset +4 * (totalHorizontalSpace / 6) - dropAreaSize / 2, dropYPos - dropAreaSize / 2, dropAreaSize, dropAreaSize);
        //Ph card
        tmp = PhCard.transform;
        tmp.Translate(new Vector3(-tmp.position.x + lateralOffset + 5 * (totalHorizontalSpace / 6), 0, 0));
        PhDropArea = new Rect(lateralOffset +5 * (totalHorizontalSpace / 6) - dropAreaSize / 2, dropYPos - dropAreaSize / 2, dropAreaSize, dropAreaSize);
    }
	
	// Update is called once per frame
	void Update () {
	    

	}

    void OnGUI()
    {
        GUI.DrawTexture(BDropArea, dropBackground);
        GUI.Label(BDropArea, "B", textFormat);

        GUI.DrawTexture(ADropArea, dropBackground);
        GUI.Label(ADropArea, "A", textFormat);

        GUI.DrawTexture(SDropArea, dropBackground);
        GUI.Label(SDropArea, "S", textFormat);

        GUI.DrawTexture(IDropArea, dropBackground);
        GUI.Label(IDropArea, "I", textFormat);

        GUI.DrawTexture(CDropArea, dropBackground);
        GUI.Label(CDropArea, "C", textFormat);

        GUI.DrawTexture(PhDropArea, dropBackground);
        GUI.Label(PhDropArea, "Ph", textFormat);
    }

    
    //Drag and drop handling
    //Image Drag
    public void HandleB()
    {
        HandleImageDrag(BCard);
    }

    public void HandleA()
    {
        HandleImageDrag(ACard);
    }

    public void HandleS()
    {
        HandleImageDrag(SCard);
    }

    public void HandleI()
    {
        HandleImageDrag(ICard);
    }

    public void HandleC()
    {
        HandleImageDrag(CCard);
    }

    public void HandleP()
    {
        HandleImageDrag(PhCard);
    }

    public void HandleImageDrag(GameObject toSet)
    {
        toSet.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    }
}
