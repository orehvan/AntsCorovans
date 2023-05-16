using System;
using System.Collections;
using System.Collections.Generic;
using DTerrain;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    
    public float speed; 
    public int destroyCircleSize;
    public PlayerResources playerResources;

    private Shape _destroyCircle;
    private Vector3 _targetPosition;
    private bool _isMoving;
    
    [SerializeField] protected BasicPaintableLayer primaryLayer;
    [SerializeField] protected BasicPaintableLayer secondaryLayer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
        // DestroyCircleSize = (int)Math.Ceiling(GetComponent<SpriteRenderer>().bounds.size.x);
        // destroyCircleSize = 40;
        _destroyCircle = Shape.GenerateShapeCircle(destroyCircleSize);
        
        Debug.Log($"Circle size: {destroyCircleSize}");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }

        if (!_isMoving) return;
        Move();
        DestroyTerrain();
    }

    private void Move()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _targetPosition - transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
        Debug.Log($"Mouse pos: {_targetPosition}");

        if (transform.position == _targetPosition) _isMoving = false;
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
}
