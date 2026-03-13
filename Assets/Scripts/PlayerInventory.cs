using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    
    private List<string> keys = new List<string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddKey(string id) 
    { 
        if (!keys.Contains(id)) keys.Add(id); 
    }

    public bool HasKey(string id) 
    { 
        return keys.Contains(id); 
    }
}