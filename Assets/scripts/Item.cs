using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        sword,
        helmet,
        armor,
        pants,
        healthPotion,
        monsterPart,
        key,

    }

    public ItemType itemType;
    public int amount;
}
