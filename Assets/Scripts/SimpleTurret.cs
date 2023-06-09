using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class SimpleTurret : AbstractTurret
{
    [SerializeField] private float damage;
    [SerializeField] private float delayBetweenAttacks;
    [SerializeField] private float range;
    
    
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private GameObject bullet;

    private float nextAttackTime;
    
    private AbstractEnemy currentTarget;
    private List<AbstractEnemy> enemiesInRange;
    
    void Start()
    {
        enemiesInRange = new List<AbstractEnemy>();
        gameObject.GetComponent<CircleCollider2D>().radius = range;
    }
    
    void Update()
    {
        GetCurrentTarget();
        RotateTowardsTarget();
        Shoot();
    }

    private void GetCurrentTarget()
    {
        currentTarget = enemiesInRange.Count == 0 ? null : enemiesInRange[0];
    }

    private void RotateTowardsTarget()
    {
        if (currentTarget == null)
            return;
        var vectorToTarget = currentTarget.transform.position - transform.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Shoot()
    {
        if (nextAttackTime > 0)
        {
            nextAttackTime -= Time.deltaTime;
            return;
        }
        if (currentTarget == null)
            return;
        Instantiate(bullet, bulletSpawnPos.transform.position, transform.rotation);
        // newBullet.GetComponent<SimpleBullet>().SetDamage(damage);
       nextAttackTime = delayBetweenAttacks;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.gameObject.GetComponent<AbstractEnemy>();
        if(enemy != null)
            enemiesInRange.Add(enemy);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var enemy = other.gameObject.GetComponent<AbstractEnemy>();
        enemiesInRange.Remove(enemy);
    }
}
