using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CardsHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler  {

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        //GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {

        Rect BDropRect = GameObject.Find("BSlot").GetComponent<RectTransform>().rect;

        Debug.Log(""+itemBeingDragged.GetComponent<RectTransform>().rect);

        if (MatchDropSlots(CreateRectFromMouseAndUIPanel(itemBeingDragged.GetComponent<RectTransform>().rect)) != null)
        {
            itemBeingDragged.SetActive(false);
        }

        Debug.Log("Drag end");
        itemBeingDragged = null;
        //GetComponent<CanvasGroup>().blocksRaycasts = true;
        if(transform.parent == startParent)
            transform.position = startPosition;
    }

    private string MatchDropSlots(Rect dragged)
    {
        Rect BDrop = CreateRectForSlot("BSlot");

        Debug.Log("slot:" + BDrop);
        Debug.Log("card:" + dragged);

        if(RectsOverlap(BDrop, dragged))
            return "B";
        return null;
    }

    private bool RectsOverlap(Rect r1, Rect r2)
    {
        var widthOverlap =  (r1.xMin >= r2.xMin) && (r1.xMin <= r2.xMax) ||
                        (r2.xMin >= r1.xMin) && (r2.xMin <= r1.xMax);
   
        var heightOverlap = (r1.yMin >= r2.yMin) && (r1.yMin <= r2.yMax) ||
                        (r2.yMin >= r1.yMin) && (r2.yMin <= r1.yMax);
                       
        return widthOverlap && heightOverlap;
    }

    private Rect CreateRectForSlot(string slotName)
    {
        GameObject slot = GameObject.Find("BSlot");
        float parentX, parentY;

        //get parent contribution
        Rect parentRect = slot.transform.parent.gameObject.GetComponent<RectTransform>().rect;
        parentX = parentRect.xMin;
        parentY = parentRect.yMin;

        //combine with child data
        Rect slotRect = slot.GetComponent<RectTransform>().rect;
        return new Rect(parentX + slotRect.xMin, parentY + slotRect.yMin, slotRect.width, slotRect.height);
    }

    private Rect CreateRectFromMouseAndUIPanel(Rect panelRect)
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float panelHeight = panelRect.height;
        float panelWidth = panelRect.width;

        return new Rect(mouseX - panelWidth / 2, mouseY + panelHeight / 2, panelWidth, panelHeight);
    }
}
