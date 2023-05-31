using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject[] spawners;
    [SerializeField] private CorovanController corovan;

    private bool spawnStarted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (spawnStarted) return;
        spawnStarted = true;
        corovan.gameObject.SetActive(true);
        Debug.Log("Spawn started");
        
        foreach (var spawner in spawners)
            spawner.SetActive(true);
    }
}
