
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public TextMeshProUGUI upgradeCost;

    public Button upgradeButton;
    public TextMeshProUGUI sellCost;

    public void SetTarget (Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
        upgradeCost.text = "-$ " + target.turretBluePrint.upgradeCost;
        upgradeButton.interactable = true;
        }
        else {
        upgradeCost.text = "DONE";
        upgradeButton.interactable = false;

        }

        sellCost.text = "$" + target.turretBluePrint.sellPrice;

        ui.SetActive(true); 


    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.upgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell ()
    {
        target.sellTurret();
        BuildManager.instance.DeselectNode();
    }
}
