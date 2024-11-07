using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Transform inventoryUIParent;
    public GameObject itemSlotPrefab;
    private Item selectedItem;

    void Start()
    {
        // Inicializar la UI de inventario vacía
        UpdateInventoryUI();
    }

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        UpdateInventoryUI();
    }

    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        // Limpiar la UI de inventario actual
        foreach (Transform child in inventoryUIParent)
        {
            Destroy(child.gameObject);
        }

        // Crear un nuevo icono para cada ítem en el inventario
        foreach (var item in items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, inventoryUIParent);
            slot.GetComponentInChildren<Image>().sprite = item.icon;
            slot.GetComponent<ItemSlot>().Setup(item, this);
        }
    }

    public void OnItemDragged(Item item)
    {
        selectedItem = item;
    }

    public void OnItemDropped(ItemSlot slot)
    {
        if (slot != null && selectedItem != null)
        {
            EquipItem(selectedItem);
            selectedItem = null;
        }
    }

    public void EquipItem(Item item)
    {
        // Equipar el ítem y actualizar la lógica del personaje
        Debug.Log($"Equipped: {item.name}");
    }
}
