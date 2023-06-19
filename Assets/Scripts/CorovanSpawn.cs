using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorovanSpawn : MonoBehaviour
{
    [SerializeField] private CorovanController corovan;
    [SerializeField] private GameObject corovanPrefab;
    [SerializeField] private Transform spawnPoint;
    private bool spawned;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (spawned) return;
        spawned = true;
        var position = spawnPoint.position;
        Instantiate(corovanPrefab,
            new Vector3(position.x, position.y, position.z),
            new Quaternion(1, 1, 1, 1));
        
        //corovan.gameObject.SetActive(true);
    }
}
