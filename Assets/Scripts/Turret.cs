using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    private Enemy targetEnemy;
    
    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets (Default)")]  
    public GameObject bulletPrefab;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser (Default)")]  
    public bool useLaser = false;
    public int damageOverTime = 30;
    public float slowPerc = 0.5f;
    public LineRenderer lineRenderer;
    public ParticleSystem laserEffect;

    public Light impactLight;

    [Header("Unity Set Up Fields")]

    public string enemyTag = "Enemy";
    public Transform RotateAxis;
    public float TurnSpeed = 10f;
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
            targetEnemy = NearestEnemy.GetComponent<Enemy>();
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
        {
            if (useLaser)
            {
                if(lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserEffect.Stop();
                }
            }
            return;
        }


        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }else {

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

        }

;
    }

    void LockOnTarget ()
    {
        // Target lock-on smoothing
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(RotateAxis.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        RotateAxis.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser ()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPerc);
        
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserEffect.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        

		Vector3 dir = firePoint.position - target.position;

		laserEffect.transform.position = target.position + dir.normalized;

		laserEffect.transform.rotation = Quaternion.LookRotation(dir);

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
