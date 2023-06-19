using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject[] spawners;

    private bool spawnStarted;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (spawnStarted) return;
        spawnStarted = true;
        Debug.Log("Spawn started");
        
        foreach (var spawner in spawners)
            spawner.SetActive(true);
    }
}
