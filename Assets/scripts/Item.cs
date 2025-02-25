using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool isEquippable;
    public int powerLevel;
    public string description;
    public bool isLegendary;
    
    public bool isPotion;
    public int healAmount;
}
