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
    [SerializeField] private GameObject itemDropPrefab; // Prefab del objeto que aparecer� al soltar
    [SerializeField] private Transform playerDropPoint; // Lugar donde aparecer�n los objetos soltados

   

    // Nuevo: Referencia al �tem seleccionado
    private Item selectedItem = null;


    private void Start()
    {
        
        inventoryCanvasGroup = inventoryUIParent.GetComponent<CanvasGroup>();
        if (inventoryCanvasGroup == null)
        {
            inventoryCanvasGroup = inventoryUIParent.gameObject.AddComponent<CanvasGroup>();
        }

        
        HideInventory();
        ShowInventory();







    }

    

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.F) && selectedItem != null)
        {
            DropSelectedItem();
        }

    }

    public void DropSelectedItem()
    {
        if (selectedItem == null) return;

        // Elimina el objeto del inventario y lo coloca en el mundo
        RemoveItem(selectedItem);
        DropItem(selectedItem);

        // Limpia el �tem seleccionado
        selectedItem = null;
    }

    public void SetSelectedItem(Item item)
    {
        selectedItem = item; // Actualiza el �tem seleccionado
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

        // 1. Remover el objeto del inventario
        RemoveItem(item);

        // 2. Instanciar el objeto en el mundo
        GameObject droppedItem = Instantiate(itemDropPrefab, playerDropPoint.position, Quaternion.identity);

        // 3. Configurar el objeto con los datos del �tem
        ArtifactPickup pickup = droppedItem.GetComponent<ArtifactPickup>();
        if (pickup != null)
        {
            pickup.artifact = item as Item; // Asigna el ScriptableObject asociado
        }
    }

    public void RemoveItem(Item item)
    {
        // L�gica para eliminar el �tem del inventario
        // Por ejemplo:
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
           
            itemSlots.Add(newSlot);
            Debug.Log("holi4");
        }
    }

    public void AddItem(Item artifact)
    {
        if (artifact != null)
        {
            
            inventoryItems.Add(artifact); 
           
            UpdateInventoryUI();
            Debug.Log("holi2");
            Debug.Log($"{artifact.itemName} a�adido al inventario.");
        }
        else
        {
            
            Debug.LogWarning("El �tem es nulo y no se puede agregar al inventario.");
        }
    }
}
