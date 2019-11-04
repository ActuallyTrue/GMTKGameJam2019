using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
        [HideInInspector]
        public List<Transform> enemyTypes;
        public int count;
        public float spawnRate;

        Wave()
        {
            enemyTypes = new List<Transform>();
        }

    }


    public string nextLevel;
    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;
    private GameObject Player;
    private DamageAndHealth playerHealth;

    public float timeBetweenWaves = 15f;
    Camera mainCamera;
    private float waveCountdown = 0f;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.counting;

    //callback invoked when the wave changes
    public UnityEvent WaveEvent;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        mainCamera = Camera.main;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = Player.GetComponent<DamageAndHealth>();
        if (WaveEvent == null)
        {
            WaveEvent = new UnityEvent();
        }
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
        playerHealth.addHealth(5);

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
            WaveEvent.Invoke();
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

        _wave.enemyTypes.Clear();
        //print(_wave.enemyTypes.Count.ToString());

        if (_wave.normalStarChild != null) {
            print(gameObject.name + " Added Normal");
            _wave.enemyTypes.Add(_wave.normalStarChild);
        }

        if (_wave.toughStarChild != null)
        {
            print(gameObject.name + " Added Tough");
            _wave.enemyTypes.Add(_wave.toughStarChild);
        }

        if (_wave.lazyStarChild != null)
        {
            print(gameObject.name + " Added Lazy");
            _wave.enemyTypes.Add(_wave.lazyStarChild);
        }

        if (_wave.angryStarChild != null)
        {
            print(gameObject.name + " Added Angry");
            _wave.enemyTypes.Add(_wave.angryStarChild);
        }

        if (_wave.tenaciousStarChild != null)
        {
            print(gameObject.name + " Added Tenacious");
            _wave.enemyTypes.Add(_wave.tenaciousStarChild);
        }
        print(_wave.enemyTypes.Count.ToString());


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

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(_sp.position);
        bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        while (onScreen)
        {
            _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            screenPoint = mainCamera.WorldToViewportPoint(_sp.position);
            onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }
        Transform enemyToSpawn = _enemyTypes[Random.Range(0, _enemyTypes.Count )];
        Debug.Log("Spawning Enemy " + enemyToSpawn.ToString());
        Instantiate(enemyToSpawn, _sp.position, _sp.rotation);
    }

    public string GetWaveName()
    {
        return waves[nextWave].name;
    }
    public int GetWaveNum()
    {
        return nextWave;    //actually this wave as soon as wave starts
    }
}
