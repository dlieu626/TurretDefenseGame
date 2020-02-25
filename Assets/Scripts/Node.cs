using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;

    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;

    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;
    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start ()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition() 
    {
        return transform.position + positionOffset;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        return;

        BuildTurret(buildManager.GetTurretToBuild());


    }

    void BuildTurret (TurretBluePrint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
        Debug.Log("Not enough dosh doll");
        return;
        }
        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = blueprint;

        Debug.Log("Turret built!");
    }

    public void upgradeTurret()
    {
        if (PlayerStats.Money < turretBluePrint.upgradeCost)
        {
        Debug.Log("Not enough dosh doll to upgrade");
        return;
        }
        PlayerStats.Money -= turretBluePrint.upgradeCost;
        // Removes old turret
        Destroy(turret);

        // Building upgraded turret
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        isUpgraded = true;
        Debug.Log("Turret upgraded!");
    }

    void OnMouseEnter () 
    {
        if (EventSystem.current.IsPointerOverGameObject())
        return;

        if (!buildManager.CanBuild)
        return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        } else{
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit ()
    {
        rend.material.color = startColor;
    }
}
