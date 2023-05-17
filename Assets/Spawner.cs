using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private AbstractEnemy regularEnemy;
    [SerializeField] private Transform navmeshTarget;
    [SerializeField] private int enemyQuantity;
    private int enemiesSpawned;
    [SerializeField] private float delayBetweenSpawns;
    [SerializeField] private BaseBehavior baseObj;
    private float nextSpawnTime;


    // Update is called once per frame
    void Update()
    {
        if (enemiesSpawned == enemyQuantity)
        {
            enemiesSpawned = 0;
            gameObject.SetActive(false);
        }
        if (nextSpawnTime > 0)
        {
            nextSpawnTime -= Time.deltaTime;
            return;
        }
        var newEnemy = Instantiate(regularEnemy, transform.position, transform.rotation);
        newEnemy.SetTarget(navmeshTarget);
        newEnemy.SetBaseTarget(baseObj);
        nextSpawnTime = delayBetweenSpawns;
        enemiesSpawned++;
    }
}
