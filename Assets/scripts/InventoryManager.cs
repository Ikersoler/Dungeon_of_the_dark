using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUIParent; 
    public GameObject itemSlotPrefab;   
    private List<Item> inventoryItems = new List<Item>(); 
    private List<GameObject> itemSlots = new List<GameObject>(); 
    private CanvasGroup inventoryCanvasGroup; 
    private bool isInventoryVisible = false;
    [SerializeField] private GameObject itemDropPrefab; 
    [SerializeField] private Transform playerDropPoint;
    private ItemSlot selectedItem = null;
    private List<Item> itemList;





    private void Start()
    {
        
        inventoryCanvasGroup = inventoryUIParent.GetComponent<CanvasGroup>();
        if (inventoryCanvasGroup == null)
        {
            inventoryCanvasGroup = inventoryUIParent.gameObject.AddComponent<CanvasGroup>();
        }
        
        HideInventory();

        itemList = new List<Item>();
    }

    private void Update()
    {
        //con esto oculto y muestro el inventario

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        //con esto tiro los objetos

        if (Input.GetKeyDown(KeyCode.F) && selectedItem != null)
        {
            selectedItem.OnDropButtonPressed();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            UsePotion();
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

    //gracias a esto puedo ocultar el inventario por codigo
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
            //comprobar si se puede convertir algo en item
            pickup.artifact = item as Item; 
        }
    }

    private Vector3 dropPosition()
    {
        //esto lo que hade es dorpear el objeto dos posiciones delante mia en la direccion en la que este mirando

        Vector3 drop = playerDropPoint.position + 2*playerDropPoint.forward;
        return drop;
    }



    public void RemoveItem(Item item)
    {
       
        inventoryItems.Remove(item);
       
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

    public bool HasItem(string itemName)
    {
        foreach (Item item in inventoryItems)
        {
            Debug.Log($"it{item.itemName}");
            if (item.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }


   public void UsePotion()
    {
        foreach (Item item in inventoryItems)
        {
            if (item.isPotion)
            {
                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(item.healAmount);
                    RemoveItem(item);
                    UpdateInventoryUI();
                    Debug.Log($"Usaste {item.itemName} y recuperaste {item.healAmount} de vida.");
                }
                return;
            }
        }
        Debug.Log("No tienes pociones disponibles.");
    }
}
