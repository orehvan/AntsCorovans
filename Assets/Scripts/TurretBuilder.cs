using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuilder : MonoBehaviour
{
    [SerializeField] private GameObject buildingUI;
    private Vector2 buildingPos;
    private bool isBuildingState;

    private Camera mainCamera;

    [SerializeField] private GameObject simpleTurret;

    private void Start()
    {
        mainCamera = Camera.main;;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isBuildingState)
            OpenBuildingUI();
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
        isBuildingState = false;
        buildingUI.gameObject.SetActive(false);
    }
}
