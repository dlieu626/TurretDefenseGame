using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;

    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float range = 15f;

    [Header("Unity Set Up Fields")]

    public string enemyTag = "Enemy";
    public Transform RotateAxis;
    public float TurnSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;



    // For initialization
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget() 
    {
        GameObject [] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject NearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                NearestEnemy = enemy;
            }
        }

        if (NearestEnemy != null && shortestDistance <= range)
        {
            target = NearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        // Target lock-on smoothing
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(RotateAxis.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        RotateAxis.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot ()
    {
        GameObject bulletGO = (GameObject)Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
