using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefab);
    }

}
