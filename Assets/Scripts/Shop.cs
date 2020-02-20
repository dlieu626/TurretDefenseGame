
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    void Start ()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardTurret ()
    {
        Debug.Log("Standard Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.standardTurretPreFab);
    }

    public void PurchaseMissileLauncher ()
    {
        Debug.Log("Missile Launcher Purchased");
        buildManager.SetTurretToBuild(buildManager.missileLauncherPreFab);

    }

}
