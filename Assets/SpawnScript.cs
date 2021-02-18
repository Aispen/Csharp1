using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private int maxCoins;
    [SerializeField] private float spawnDelayCoin;
    [SerializeField] private GameObject coinObject;

    private float nextSpawnTime;

    [SerializeField] private int maxBombs;
    [SerializeField] private float spawnDelayBomb;
    [SerializeField] private GameObject bombObject;
    [SerializeField] private float BombLifetime;

    private bool startTimerDestroyBombs = false;
    private float deleteBombsTimer;
    private float nextSpawnTimeBomb;
    
    public static int currentSpawnedCoins = 0;
    public static int currentSpawnedBombs = 0;

    private Vector3 spawnerLocation;

    // Start is called before the first frame update
    void Start()
    {
        spawnerLocation.x = transform.position.x;
        spawnerLocation.y = transform.position.y;
        spawnerLocation.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextSpawnTime && currentSpawnedCoins <= maxCoins)
        {
            nextSpawnTime = Time.time + spawnDelayCoin;
            currentSpawnedCoins++;
            SpawnObject(coinObject);
        }

        if(Time.time >= nextSpawnTimeBomb && currentSpawnedBombs <= maxBombs)
        {
            nextSpawnTimeBomb = Time.time + spawnDelayBomb;
            currentSpawnedBombs++;
            SpawnObject(bombObject);
            //print("bomb spawned");
        }

        if (currentSpawnedBombs == maxBombs)
        {
            startTimerDestroyBombs = true;
            deleteBombsTimer = Time.time + BombLifetime;
            //print("bomb lifetime countdown");
        }
        if (startTimerDestroyBombs && Time.time >= deleteBombsTimer)
        {
            var bombs = GameObject.FindGameObjectsWithTag("Bomb");
            foreach(var bomb in bombs)
            {
                GameObject.Destroy(bomb);
            }
            currentSpawnedBombs = 0;
            startTimerDestroyBombs = false;
            nextSpawnTime = Time.time + spawnDelayBomb;
            //print("bombs deleted");
        }
    }
    void SpawnObject(GameObject objectToSpawn)
    {
        int []spawnCoords = CalculateCoordinates();

        Collider[] intersecting = Physics.OverlapSphere(new Vector3(spawnCoords[0], 0.5f, spawnCoords[1]), 0.01f);
        if (intersecting.Length == 0)
        {
            Instantiate(objectToSpawn, new Vector3(spawnerLocation.x + spawnCoords[0], spawnerLocation.y + 1, spawnerLocation.z + spawnCoords[1]), transform.rotation);
        }
        
    }
    int[] CalculateCoordinates()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        bool positionIsBad = true;
        int[] coords = new int[2];
        while(positionIsBad)
        {
            int randomIntX = Random.Range(-4, 4);
            int randomIntZ = Random.Range(-4, 4);
            if(randomIntX-player.transform.position.x > 2 || randomIntX - player.transform.position.x > -2)
            {
                coords[0] = randomIntX;
                if (randomIntZ - player.transform.position.z > 2 || randomIntZ - player.transform.position.z > -2)
                {
                    coords[1] = randomIntZ;
                    positionIsBad = false;
                }
            }
        }
        print("Player pos: X = " + player.transform.position.x + " Z = " + player.transform.position.z);
        return coords;
    }

    public static void ResetSettings()
    {
        currentSpawnedBombs = 0;
        currentSpawnedCoins = 0;
    }
}
