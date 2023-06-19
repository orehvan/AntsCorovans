using System;
using System.Collections;
using System.Collections.Generic;
using DTerrain;
using NavMeshPlus.Components;
using UnityEngine;

public class CorovanSpawn : MonoBehaviour
{
    [SerializeField] private CorovanController corovan;
    [SerializeField] private GameObject corovanPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private BasicPaintableLayer collidable;
    [SerializeField] private BasicPaintableLayer visible;
    [SerializeField] private BaseBehavior baseObj;
    [SerializeField] private NavMeshSurface navmeshRoot;
    private bool spawned;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (spawned) return;
        spawned = true;
        var position = spawnPoint.position;
        var corova = Instantiate(corovanPrefab,
            new Vector3(position.x, position.y, position.z),
            new Quaternion(1, 1, 1, 1));
        corova.GetComponent<CorovanController>().Setup(collidable, visible, gameObject.transform, baseObj, navmeshRoot);

        //corovan.gameObject.SetActive(true);
    }
}
