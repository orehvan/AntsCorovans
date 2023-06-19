using System.Collections;
using System.Collections.Generic;
using DTerrain;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class CorovanController : MonoBehaviour
{
    public float speed; 
    public int destroyCircleSize;
    private Shape _destroyCircle;

    [SerializeField] private BasicPaintableLayer primaryLayer;
    [SerializeField] private BasicPaintableLayer secondaryLayer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform navmeshTarget;
    [SerializeField] private BaseBehavior baseObj;

    private CorovanMinigame corovanMinigame;

    private NavMeshAgent agent;
    [SerializeField] private NavMeshSurface navmesh;

    private bool corovanThingsDone = true;
    [SerializeField] private bool goingHome;
    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
        agent.SetDestination(navmeshTarget.position);
    }

    private void Start()
    {
        _destroyCircle = Shape.GenerateShapeCircle(destroyCircleSize);
        navmesh.BuildNavMesh();
        corovanMinigame = FindObjectOfType<CorovanMinigame>();

    }
    
    void Update()
    {
        if (Vector2.Distance(transform.position, navmeshTarget.position) < 2f)
        {
            if (!goingHome)
            {
                StartCoroutine(CorovanThings());
                //DoCorovanThings
                // goingHome = true;
                // navmeshTarget = baseObj.transform;
                // agent.SetDestination(navmeshTarget.position);
            }
            else
            {
                Reward();
                Destroy(gameObject);
            }
        }
        else
        {
            DestroyTerrain();
        }
        RotateSprite();
    }

    private IEnumerator CorovanThings()
    {
        yield return new WaitForSeconds(5f);
        corovanMinigame.StartGame();
        yield return new WaitUntil(() => corovanMinigame.complete);
        yield return new WaitForSeconds(5f);
        corovanMinigame.StartGame();
        yield return new WaitUntil(() => corovanMinigame.complete);
        goingHome = true;
        navmeshTarget = baseObj.transform;
        agent.SetDestination(navmeshTarget.position);
    }

    private void RotateSprite()
    {
        Vector3 nextTarget;
        nextTarget = agent.path.corners.Length > 1 ? agent.path.corners[1] : navmeshTarget.position;
        spriteRenderer.transform.rotation =
            Quaternion.LookRotation(Vector3.forward, (Vector2)nextTarget - (Vector2)transform.position);
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

    private void Reward()
    {
        PlayerController.Instance.playerResources.FireResource += 50;
        PlayerController.Instance.playerResources.PoisonResource += 50;
        PlayerController.Instance.playerResources.MetalResource += 50;
    }
}