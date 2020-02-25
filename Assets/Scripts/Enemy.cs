using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float health = 100;
    public int moneyGained = 50;
    public GameObject deathEffect;

    void Start ()
    {
        speed = startSpeed;
    }

    public void TakeDamage (float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow (float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        PlayerStats.Money += moneyGained;
        Destroy(effect, 2f);        
        Destroy(gameObject);
    }

}
