using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public Item item; 
   //[SerializeField] private Image icon; 
    private CanvasGroup canvasGroup;
    private InventoryManager inventoryManager;


    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void Awake()
    {
       // icon = transform.Find("ItemIcon").GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

   

    public void ClearItem()
    {
        item = null;
      //  icon.sprite = null;
       // icon.enabled = false;
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

   // public void SetItem(Item newItem)
    //{
      //  item = newItem;
        //if (item != null)
        //{
         //   icon.sprite = item.icon; 
           // icon.enabled = true;
       // }
       // else
        //{
          //  icon.sprite = null;
            //icon.enabled = false;
        //}
    //}

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

    
   // private void HighlightSlot()
    //{
       
    //}
    
}

