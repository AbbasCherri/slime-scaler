using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); //Keeps the inventory for all levels
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf); // if close -> open and vice versa
        }
    }
}
