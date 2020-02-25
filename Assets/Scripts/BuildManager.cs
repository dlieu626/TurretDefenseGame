using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake ()
    {
        if (instance != null)
        {
            Debug.Log("More than 1 BuildManager in Scene");
            return;
        }
        instance = this;
    }

    private TurretBluePrint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;
    public bool CanBuild { get { return turretToBuild != null; }}
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; }}

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
        Debug.Log("Not enough dosh doll");
        return;
        }
        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret built! Money left:" + PlayerStats.Money);
    }
    
    public void SelectNode (Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }


}
