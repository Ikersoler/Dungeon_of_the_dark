using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUIParent; 
    public GameObject itemSlotPrefab;   
    private List<Item> inventoryItems = new List<Item>(); 
    private List<GameObject> itemSlots = new List<GameObject>(); 
    private CanvasGroup inventoryCanvasGroup; 
    private bool isInventoryVisible = false;
    [SerializeField] private GameObject itemDropPrefab; // Prefab del objeto que aparecerá al soltar
    [SerializeField] private Transform playerDropPoint; // Lugar donde aparecerán los objetos soltados
    private ItemSlot selectedItem = null;


    



    private void Start()
    {
        
        inventoryCanvasGroup = inventoryUIParent.GetComponent<CanvasGroup>();
        if (inventoryCanvasGroup == null)
        {
            inventoryCanvasGroup = inventoryUIParent.gameObject.AddComponent<CanvasGroup>();
        }

        
        HideInventory();
       






    }

    

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.F) && selectedItem != null)
        {
            selectedItem.OnDropButtonPressed();
        }

    }

    public void DropSelectedItem()
    {
        if (selectedItem == null) return;

        
        RemoveItem(selectedItem.item);
        DropItem(selectedItem.item);

       
        selectedItem = null;
    }

    public void SetSelectedItem(ItemSlot item)
    {
        selectedItem = item; 
    }

    public void ToggleInventory()
    {
        if (isInventoryVisible)
        {
            HideInventory();
        }
        else
        {
            ShowInventory();
        }
    }

   

    private void HideInventory()
    {
        isInventoryVisible = false;
        inventoryCanvasGroup.alpha = 0;  
        inventoryCanvasGroup.interactable = false;  
        inventoryCanvasGroup.blocksRaycasts = false;  
    }

    private void ShowInventory()
    {
        isInventoryVisible = true;
        inventoryCanvasGroup.alpha = 1;  
        inventoryCanvasGroup.interactable = true;  
        inventoryCanvasGroup.blocksRaycasts = true;  
    }

    public void DropItem(Item item)
    {
        if (item == null) return;

        
        RemoveItem(item);

        
        GameObject droppedItem = Instantiate(itemDropPrefab, dropPosition(), Quaternion.identity);

        
        ArtifactPickup pickup = droppedItem.GetComponent<ArtifactPickup>();
        if (pickup != null)
        {
            pickup.artifact = item as Item; 
        }
    }

    private Vector3 dropPosition()
    {
        Vector3 drop = playerDropPoint.position + 2*playerDropPoint.forward;
      //  drop.z += 2;
        return drop;
    }



    public void RemoveItem(Item item)
    {
       
        inventoryItems.Remove(item);
       //UpdateUI();
    }


    


    void UpdateInventoryUI()
    {
       
        foreach (GameObject slot in itemSlots)
        {
            Destroy(slot);
        }
        itemSlots.Clear();
        
        
        foreach (Item item in inventoryItems)
        {
            GameObject newSlot = Instantiate(itemSlotPrefab, inventoryUIParent.transform);
            ItemSlot itemSlot = newSlot.GetComponent<ItemSlot>();
            
            itemSlot.item = item;

            itemSlots.Add(newSlot);
            //udate visuals  otro scrip 
            //udate visuals  otro scrip 
        }
    }

    public void AddItem(Item artifact)
    {
        if (artifact != null)
        {
            
            inventoryItems.Add(artifact); 
           
            UpdateInventoryUI();
            
            Debug.Log($"{artifact.itemName} añadido al inventario.");
        }
        else
        {
            
            Debug.LogWarning("El ítem es nulo y no se puede agregar al inventario.");
        }
    }
}
