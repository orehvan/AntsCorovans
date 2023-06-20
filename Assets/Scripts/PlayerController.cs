using System;
using System.Collections;
using System.Collections.Generic;
using DTerrain;
using NavMeshPlus.Components;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public bool inputDisabled = false;
    
    public float speed; 
    public int destroyCircleSize;
    public PlayerResources playerResources;

    private Shape _destroyCircle;
    private Vector3 _targetPosition;
    private bool _isMoving;
    private bool navmeshBuilt;
    private bool diggingMode;
    private bool firstStartBecauseIDontKnowWhatElseToDo = true;
    private int deliveries;

    [SerializeField] protected BasicPaintableLayer primaryLayer;
    [SerializeField] protected BasicPaintableLayer secondaryLayer;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private NavMeshAgent agent;
    [SerializeField] private NavMeshSurface navmesh;
    [SerializeField] private GameObject spawners;
    
    public event Action DiggingModeChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
        _targetPosition = transform.position;
    }
    
    private void Start()
    {
        // DestroyCircleSize = (int)Math.Ceiling(GetComponent<SpriteRenderer>().bounds.size.x);
        // destroyCircleSize = 40;
        
        _destroyCircle = Shape.GenerateShapeCircle(destroyCircleSize);
        Debug.Log($"Circle size: {destroyCircleSize}");
        navmesh.BuildNavMesh();
    }

    private void Update()
    {
        if (inputDisabled)
            return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DiggingModeChanged.Invoke();
            navmesh.BuildNavMesh();
            diggingMode = !diggingMode;
            agent.enabled = !agent.enabled;
            _targetPosition = transform.position;
            if (agent.enabled)
                agent.ResetPath();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (firstStartBecauseIDontKnowWhatElseToDo)
            {
                firstStartBecauseIDontKnowWhatElseToDo = false;
                navmesh.BuildNavMesh();
            }
            SetTargetPosition();
            navmeshBuilt = false;
        }

        if (!_isMoving)
        {
            if (!navmeshBuilt)
                navmesh.BuildNavMesh();
            navmeshBuilt = true;
            return;
        }

        if (!diggingMode)
        {
            if (Vector2.Distance(transform.position, _targetPosition) < 0.01f)
                _isMoving = false;
            else
                agent.SetDestination(_targetPosition);
            var nextTarget = agent.path.corners.Length > 1 ? agent.path.corners[1] : _targetPosition;
            spriteRenderer.transform.rotation =
                Quaternion.LookRotation(Vector3.forward, (Vector2)nextTarget - (Vector2)transform.position);
        }
        else
        {
            Move();
            DestroyTerrain();
        }
    }
    
    

    private void Move()
    {
        // LastStand(); //TODELETE
        spriteRenderer.transform.rotation =
            Quaternion.LookRotation(Vector3.forward, (Vector2)_targetPosition - (Vector2)transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
        Debug.Log($"Mouse pos: {_targetPosition}");

        if (Vector2.Distance(transform.position, _targetPosition) < 0.01f) _isMoving = false;
    }

    private void SetTargetPosition()
    {
        _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition.z = transform.position.z;

        _isMoving = true;
    }

    private void DestroyTerrain()
    {
        primaryLayer?.Paint(new PaintingParameters()
        {
            Color = Color.clear,
            Position = new Vector2Int((int)(transform.position.x * primaryLayer.PPU) - destroyCircleSize,
                (int)(transform.position.y * primaryLayer.PPU) - destroyCircleSize),
            Shape = _destroyCircle,
            PaintingMode = PaintingMode.REPLACE_COLOR,
            DestructionMode = DestructionMode.DESTROY
        });
        secondaryLayer?.Paint(new PaintingParameters()
        {
            Color = Color.clear,
            Position = new Vector2Int((int)(transform.position.x * secondaryLayer.PPU) - destroyCircleSize,
                (int)(transform.position.y * secondaryLayer.PPU) - destroyCircleSize),
            Shape = _destroyCircle,
            PaintingMode = PaintingMode.REPLACE_COLOR,
            DestructionMode = DestructionMode.DESTROY
        });
    }

    public void Delivery()
    {
        deliveries++;
        if (deliveries == 2)
            LastStand();
    }

    private void LastStand()
    {
        var cam = FindObjectOfType<Camera>();
        cam.orthographicSize = 20;
        foreach (Transform spawner in spawners.transform)
        {
            spawner.gameObject.SetActive(true);
            spawner.GetComponent<Spawner>().isLocalDefense = false;
        }
    }
}
