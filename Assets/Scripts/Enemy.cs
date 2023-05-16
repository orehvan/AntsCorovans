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
    [SerializeField] private GameObject path;
    [SerializeField] private bool isNavmeshPath;
    [SerializeField] private Transform navmeshTarget;
    [SerializeField] private List<Vector3> waypoints;
    private SpriteRenderer spriteRenderer;
    private NavMeshAgent agent;
    private int currentWaypointNum;
    private Vector2 currentWaypoint;
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
        if (Vector2.Distance(transform.position, currentWaypoint) < 0.01f)
        {
            if (isNavmeshPath || waypoints.Count  == currentWaypointNum)
            {
                // Debug.Log("Attack base or something");
                return;
            }
            currentWaypoint = waypoints[currentWaypointNum];
            currentWaypointNum++;
            
        }
        if (!isNavmeshPath)
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
        else
        {
            Vector3 nextTarget;
            if (agent.path.corners.Length > 1)
                nextTarget = agent.path.corners[1] - transform.position;
            else
                nextTarget = navmeshTarget.position;
            var angle = Vector3.Angle(nextTarget, Vector2.up);
            transform.rotation = Quaternion.Euler(0, 0, angle);
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

    private void Die()
    {
        Destroy(gameObject);
    }
}
