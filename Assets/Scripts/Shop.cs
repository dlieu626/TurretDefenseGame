
using UnityEngine;

public class Shop : MonoBehaviour
{

    public TurretBluePrint standardTurret;
    public TurretBluePrint missileLauncher;
    public TurretBluePrint laserBeamer;

    BuildManager buildManager;

    void Start ()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret ()
    {
        Debug.Log("Standard Turret Purchased");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher ()
    {
        Debug.Log("Missile Launcher Purchased");
        buildManager.SelectTurretToBuild(missileLauncher);

    }

    public void SelectLaserBeamer ()
    {
        Debug.Log("Laser Beamer Purchased");
        buildManager.SelectTurretToBuild(laserBeamer);

    }

}
