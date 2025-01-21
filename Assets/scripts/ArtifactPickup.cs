using UnityEditor.Experimental;
using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    public Item artifact; 
    private InventoryManager inventoryManager;

    void Start()
    { 
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Debug.Log("Picked up " + artifact);
        inventoryManager.AddItem(artifact); 
        Destroy(gameObject); 
    }

}
