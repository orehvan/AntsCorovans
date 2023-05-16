using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretBuilder : MonoBehaviour
{
    public static TurretBuilder Instance;
    
    [SerializeField] private GameObject buildingUI;
    [SerializeField] private LineRenderer colliderRenderer;
    [SerializeField] private LayerMask turretsLayer;
    [SerializeField] private TurretInfo turretInfoPanel;
    private Vector2 buildingPos;
    private bool isBuildingState;
    private Camera mainCamera;
    private bool possibleToBuild;

    [SerializeField] private GameObject simpleTurret;

    private void Start()
    {
        mainCamera = Camera.main;
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            if (!isBuildingState)
                OpenBuildingUI();
            else
            {
                CloseBuildingUI();
                OpenBuildingUI();
            }
    }

    public void BuildSimpleTurret()
    {
        Instantiate(simpleTurret, buildingPos, new Quaternion());
        CloseBuildingUI();
    }

    public void OpenBuildingUI()
    {
        isBuildingState = true;
        buildingPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        buildingUI.transform.position = Input.mousePosition;
        buildingUI.gameObject.SetActive(true);
    }

    public void CloseBuildingUI()
    {
        HideTurretInfoPanel();
        isBuildingState = false;
        buildingUI.gameObject.SetActive(false);
        colliderRenderer.enabled = false;
    }

    public void TryBuildTurret(AbstractTurret chosenTurret)
    {
        if (!possibleToBuild || !CheckPrice(chosenTurret))
            return;
        TakePayment(chosenTurret);
        Instantiate(chosenTurret, buildingPos, new Quaternion());
        CloseBuildingUI();
    }

    private bool CheckPrice(AbstractTurret chosenTurret)
    {
        var price = chosenTurret.GetPrice();
        return PseudoPlayer.Instance.playerResources.HasEnoughResources(price);
    }

    private void TakePayment(AbstractTurret chosenTurret)
    {
        var price = chosenTurret.GetPrice();
        PseudoPlayer.Instance.playerResources.Pay(price);
    }

    private void CheckForSpaceToBuild(AbstractTurret chosenTurret)
    {
        var collisions = Physics2D.OverlapBoxAll(buildingPos, chosenTurret.turretCollider.size, 0, turretsLayer);
        if (collisions.Length != 0)
        {
            SetColliderRendererColor(Color.red);
            possibleToBuild = false;
            return;
        }
        SetColliderRendererColor(Color.green);
        possibleToBuild = true;
    }

    private void SetColliderRendererColor(Color color)
    {
        colliderRenderer.startColor = color;
        colliderRenderer.endColor = color;
    }

    public void ShowTurretCollider(AbstractTurret chosenTurret)
    {
        colliderRenderer.enabled = true;
        CheckForSpaceToBuild(chosenTurret);
        var boxCollider2D = chosenTurret.turretCollider;
        Vector3[] positions = new Vector3[4];
        positions[0] = buildingPos + new Vector2(boxCollider2D.size.x / 2.0f, boxCollider2D.size.y / 2.0f);
        positions[1] = buildingPos + new Vector2(-boxCollider2D.size.x / 2.0f, boxCollider2D.size.y / 2.0f);
        positions[2] = buildingPos + new Vector2(-boxCollider2D.size.x / 2.0f, -boxCollider2D.size.y / 2.0f);
        positions[3] = buildingPos + new Vector2(boxCollider2D.size.x / 2.0f, -boxCollider2D.size.y / 2.0f);
        colliderRenderer.SetPositions(positions);
    }

    public void HideTurretCollider()
    {
        colliderRenderer.enabled = false;
    }

    public void ShowTurretInfoPanel(AbstractTurret chosenTurret)
    {
        turretInfoPanel.gameObject.SetActive(true);
        var price = chosenTurret.GetPrice();
        turretInfoPanel.ShowTurretInfo(price.firePrice, price.poisonPrice, price.metalPrice, chosenTurret.turretImage);
    }

    public void HideTurretInfoPanel()
    {
        turretInfoPanel.gameObject.SetActive(false);
    }
}
