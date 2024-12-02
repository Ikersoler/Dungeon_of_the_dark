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

        // 3. Configurar el objeto con los datos del ítem
        ArtifactPickup pickup = droppedItem.GetComponent<ArtifactPickup>();
        if (pickup != null)
        {
            pickup.artifact = item as Item; // Asigna el ScriptableObject asociado
        }
    }

    public void RemoveItem(Item item)
    {
        // Lógica para eliminar el ítem del inventario
        // Por ejemplo:
        inventoryItems.Remove(item);
        UpdateUI();
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
            Debug.Log($"{artifact.itemName} añadido al inventario.");
        }
        else
        {
            
            Debug.LogWarning("El ítem es nulo y no se puede agregar al inventario.");
        }
    }
}
