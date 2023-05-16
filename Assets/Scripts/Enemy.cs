using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class Enemy : AbstractEnemy
{
    public BaseBehavior baseObj;
    public float damagePerTick;
    public float attackCounter;
    public float attackDelay;
    
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private GameObject path;
    [SerializeField] private List<Vector3> waypoints;
    private int currentWaypointNum;
    private Vector2 currentWaypoint;
    void Start()
    {
        ParsePath();
        currentWaypoint = waypoints[currentWaypointNum];
        currentWaypointNum++;
    }
    
    void Update()
    {
        if (Vector2.Distance(transform.position, currentWaypoint) < 0.01f)
        {
            if (waypoints.Count  == currentWaypointNum)
            {
                // Debug.Log("Attack base or something");
                return;
            }
            currentWaypoint = waypoints[currentWaypointNum];
            currentWaypointNum++;
        }

        transform.rotation = Quaternion.LookRotation(Vector3.forward, currentWaypoint - (Vector2)transform.position);
        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Base")) DoDamage(damagePerTick);
    }

    private void ParsePath()
    {
        waypoints = new List<Vector3>();
        foreach (Transform waypoint in path.transform)
            waypoints.Add(waypoint.position);
    }

    public override void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    public override void DoDamage(float damage)
    {
        attackCounter -= Time.deltaTime;
        
        if (!(attackCounter <= 0)) return;
        attackCounter = attackDelay;
        baseObj.TakeDamage(damage);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
