using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentInventory : MonoBehaviour
{
    private static PersistentInventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // makes the whole InventorySystem persist
        }
        else
        {
            Destroy(gameObject); // prevents duplicates
        }
    }
}
