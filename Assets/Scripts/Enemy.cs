using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : AbstractEnemy
{
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private List<Vector3> waypoints;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private NavMeshAgent agent;
    private int currentWaypointNum;
    private Vector2 currentWaypoint;
    private float attackCounter = 0;
    private float attackDelay = 5f;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (isNavmeshPath)
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.updateRotation = false; 
            agent.updateUpAxis = false;
            agent.SetDestination(navmeshTarget.position);
            currentWaypoint = navmeshTarget.position;
        }
        else
        {
            ParsePath();
            currentWaypoint = waypoints[currentWaypointNum];
            currentWaypointNum++;
        }
    }
    
    void Update()
    {
        if (isNavmeshPath && navmeshTarget.hasChanged)
        {
            agent.SetDestination(navmeshTarget.position);
            currentWaypoint = navmeshTarget.position;
        }

        if (Vector2.Distance(transform.position, currentWaypoint) < 1f)
        {
            if (isNavmeshPath || waypoints.Count  == currentWaypointNum)
            {
                attackCounter -= Time.deltaTime;
                if (!(attackCounter < 0)) return;
                attackCounter = attackDelay;
                baseObj.TakeDamage(damage);
                return;
            }
            currentWaypoint = waypoints[currentWaypointNum];
            currentWaypointNum++;
            
        }

        if (!isNavmeshPath)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, currentWaypoint - (Vector2)transform.position);
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
        }
        else
        {
            Vector3 nextTarget;
            if (agent.path.corners.Length > 1)
                nextTarget = agent.path.corners[1];
            else
                nextTarget = navmeshTarget.position;
            spriteRenderer.transform.rotation =
                Quaternion.LookRotation(Vector3.forward, (Vector2)nextTarget - (Vector2)transform.position);
        }
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

    public override void SetTarget(Transform navmeshTransform)
    {
        navmeshTarget = navmeshTransform;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
}
