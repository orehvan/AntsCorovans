using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorovanSpawn : MonoBehaviour
{
    [SerializeField] private CorovanController corovan;
    private bool spawned;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (spawned) return;
        spawned = true;
        corovan.gameObject.SetActive(true);
    }
}
