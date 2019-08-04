using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { spawning, waiting, counting };


    [System.Serializable]
    public class Wave
    {

        public string name;
        public Transform normalStarChild;
        public Transform toughStarChild;
        public Transform lazyStarChild;
        public Transform angryStarChild;
        public Transform tenaciousStarChild;
        public List<Transform> enemyTypes;
        public int count;
        public float spawnRate;

    }


    public string nextLevel;
    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 15f;
    Camera mainCamera;
    public float waveCountdown = 0f;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.counting;


    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (state == SpawnState.waiting)
        {

            if (!IsEnemyAlive())
            {
                // Begin a new round
                RoundStart();
                return;
            }

            else
            {
                return;
            }

        }

        if (waveCountdown <= 0)
        {

            if (state != SpawnState.spawning)
            {
                //Start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void RoundStart()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            //nextWave = 0;
            SceneManager.LoadScene(nextLevel);
            Debug.Log("All Waves Complete!  Looping...");
        }
        else
        {
            nextWave++;
        }

    }

    bool IsEnemyAlive()
    {

        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;

    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.spawning;


        if (_wave.normalStarChild != null) {
            _wave.enemyTypes.Add(_wave.normalStarChild);
        }

        if (_wave.toughStarChild != null)
        {
            _wave.enemyTypes.Add(_wave.toughStarChild);
        }

        if (_wave.lazyStarChild != null)
        {
            _wave.enemyTypes.Add(_wave.lazyStarChild);
        }

        if (_wave.angryStarChild != null)
        {
            _wave.enemyTypes.Add(_wave.angryStarChild);
        }

        if (_wave.tenaciousStarChild != null)
        {
            _wave.enemyTypes.Add(_wave.tenaciousStarChild);
        }


        //Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemyTypes);
            yield return new WaitForSeconds(1f / _wave.spawnRate);

        }

        state = SpawnState.waiting;

        yield break;
    }

    void SpawnEnemy(List<Transform> _enemyTypes)
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points");
        }
        Debug.Log("Spawning Enemy");

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(_sp.position);
        bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        while (onScreen)
        {
            _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            screenPoint = mainCamera.WorldToViewportPoint(_sp.position);
            onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }
        Transform enemyToSpawn = _enemyTypes[Random.Range(0, _enemyTypes.Count)];
        Instantiate(enemyToSpawn, _sp.position, _sp.rotation);
    }

}
