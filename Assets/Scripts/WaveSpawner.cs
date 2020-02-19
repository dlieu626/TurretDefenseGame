using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 4f;
    public Text waveCountdownText;
    private float countdown = 2f;
    private int waveNumber = 1;

    void Update () 
    {
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        waveCountdownText.text = Mathf.Floor(countdown).ToString();
    }
    
    IEnumerator SpawnWave ()
    {        
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        waveNumber++;
    
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    

}
