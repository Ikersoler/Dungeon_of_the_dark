using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public Item item; 
   
    private CanvasGroup canvasGroup;
    private InventoryManager inventoryManager;


    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void Awake()
    {
       
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ClearItem()
    {
        item = null;
      
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item dropped!");
        
    }
  
    public void OnDropButtonPressed()
    {
        if (item != null)
        {
            Debug.Log($"estoy dropeando{item}");
            inventoryManager.DropItem(item);
            Destroy(gameObject);
        }
    }

    public void OnItemSelected()
    {
        if (item != null)
        {
            Debug.Log($"Ítem seleccionado: {item.name}");
            inventoryManager.SetSelectedItem(gameObject.GetComponent<ItemSlot>());
           

        }
    }

}

