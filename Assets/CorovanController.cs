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
    [SerializeField] private float hp = 100;
    public float currentHealth;

    [SerializeField] private BasicPaintableLayer primaryLayer;
    [SerializeField] private BasicPaintableLayer secondaryLayer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform navmeshTarget;
    [SerializeField] private BaseBehavior baseObj;
    [SerializeField] private Sprite secondForm;
    private Camera cam;

    private CorovanMinigame corovanMinigame;

    private NavMeshAgent agent;
    [SerializeField] private NavMeshSurface navmesh;

    private bool corovanThingsDone = true;
    private bool ready;
    // Start is called before the first frame update
    [SerializeField] private bool goingHome;
    private bool startedWorking;

    private void Awake()
    {
        _destroyCircle = Shape.GenerateShapeCircle(destroyCircleSize);
        navmesh?.BuildNavMesh();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
    }

    private void Start()
    {
        currentHealth = hp;
        if (navmeshTarget is not null)
            agent.SetDestination(navmeshTarget.position);
    }

    public void Setup(BasicPaintableLayer primary, BasicPaintableLayer secondary, Transform target, BaseBehavior baseO, NavMeshSurface navmesh)
    {
        primaryLayer = primary;
        secondaryLayer = secondary;
        navmeshTarget = target;
        this.navmesh = navmesh;
        navmesh.BuildNavMesh();
        agent.SetDestination(navmeshTarget.position);
        baseObj = baseO;
        ready = true;
        corovanMinigame = FindObjectOfType<CorovanMinigame>();
        cam = FindObjectOfType<Camera>();
        cam.orthographicSize = 10;
    }
    
    void Update()
    {
        if (!ready)
            return;
        if (Vector2.Distance(transform.position, navmeshTarget.position) < 2f)
        {
            if (!goingHome)
            {
                if (!startedWorking)
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
        navmeshTarget.GetComponent<CorovanSpawn>().StartLocalDefense();
        startedWorking = true;
        yield return new WaitForSeconds(5f);
        corovanMinigame.StartGame();
        yield return new WaitUntil(() => corovanMinigame.complete);
        yield return new WaitForSeconds(5);
        corovanMinigame.StartGame();
        yield return new WaitUntil(() => corovanMinigame.complete);
        yield return new WaitForSeconds(5);
        corovanMinigame.StartGame();
        yield return new WaitUntil(() => corovanMinigame.complete);
        navmeshTarget.GetComponent<CorovanSpawn>().EndLocalDefense();
        yield return new WaitForSeconds(5);
        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().sprite = secondForm;
        }
        goingHome = true;
        cam.orthographicSize = 8;
        Destroy(navmeshTarget.gameObject);
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
        PlayerController.Instance.Delivery();
        PlayerController.Instance.playerResources.FireResource += 50;
        PlayerController.Instance.playerResources.PoisonResource += 50;
        PlayerController.Instance.playerResources.MetalResource += 50;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0) 
            gameObject.SetActive(false);
    }
}
