using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public string requiredKey; // El nombre de la llave necesaria para abrir la puerta
    private bool isOpen = false; // Estado de la puerta (abierta o cerrada)
    private InventoryManager inventoryManager; // Referencia al gestor de inventario

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("colision");
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("he tocao puelta");
    //        TryOpenDoor();
    //    }
    //}

    public void TryOpenDoor()
    {
        if (inventoryManager.HasItem(requiredKey))
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("Necesitas la llave: " + requiredKey);
        }
    }

    void OpenDoor()
    {
        if (!isOpen)
        {
            Debug.Log("Puerta abierta!");
            isOpen = true;
            // Añade aquí la animación o lógica de apertura de la puerta
            Destroy(gameObject); // Opcional: elimina la puerta si debe desaparecer al abrirse
        }
    }
}
