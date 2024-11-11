using UnityEditor.Experimental;
using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    public Item Item; // Referencia al `ScriptableObject` del artefacto
    private InventoryManager inventoryManager;

    void Start()
    {
        // Buscar el componente InventoryManager en la escena
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador entra en el área del artefacto
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Debug.Log("Picked up " + Item.itemName);
        inventoryManager.AddItem(Item); // Agregar el artefacto al inventario
        Destroy(gameObject); // Destruir el objeto en el mundo después de recogerlo
    }
}
