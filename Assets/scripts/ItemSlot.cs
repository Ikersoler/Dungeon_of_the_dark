using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private Item item;
    private InventoryManager inventoryManager;
    private Image itemImage;

    public void Setup(Item newItem, InventoryManager manager)
    {
        item = newItem;
        inventoryManager = manager;
        itemImage = GetComponentInChildren<Image>();
        itemImage.sprite = item.icon;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            inventoryManager.OnItemDragged(item);
            itemImage.raycastTarget = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        itemImage.raycastTarget = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        inventoryManager.OnItemDropped(this);
    }
}
