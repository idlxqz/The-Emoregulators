using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CardDropHandler : MonoBehaviour, IDropHandler {

    public GameObject item
    {
        get {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if(!item){
            CardsHandler.itemBeingDragged.transform.SetParent(transform);
        }
    }
}
