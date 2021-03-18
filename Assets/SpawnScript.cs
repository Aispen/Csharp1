using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private float spawnHeight = -0.1f;

    [SerializeField] private int maxCoins;
    [SerializeField] private float spawnDelayCoin;
    [SerializeField] private GameObject coinObject;

    private float nextSpawnTime;

    [SerializeField] private int maxBombs;
    [SerializeField] private float spawnDelayBomb;
    [SerializeField] private GameObject bombObject;
    [SerializeField] private float BombLifetime;

    [SerializeField] GameObject player;

    private bool startTimerDestroyBombs = false;
    private float deleteBombsTimer;
    private float nextSpawnTimeBomb;
    
    public static int currentSpawnedCoins = 0;
    public static int currentSpawnedBombs = 0;

    //private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
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
            Instantiate(objectToSpawn, new Vector3(spawnCoords[0], spawnHeight ,spawnCoords[1]), Quaternion.identity);
        }
        
    }
    int[] CalculateCoordinates()
    {
        int area = 9;
        bool positionIsBad = true;
        int[] coords = new int[2];
        while(positionIsBad)
        {
            int randomIntX = Random.Range(-area, area); //9
            int randomIntZ = Random.Range(-area, area); //9
            if((randomIntX * -1) - player.transform.position.x > 3 || randomIntX - (player.transform.position.x*-1) > 3)
            {
                coords[0] = randomIntX;
                if ((randomIntZ*-1) - player.transform.position.z > 3 || randomIntZ - (player.transform.position.z*-1) > 3)
                {
                    coords[1] = randomIntZ;
                    positionIsBad = false;
                }
            }
        }
        //print("Player pos: X = " + player.transform.position.x + " Z = " + player.transform.position.z);
        return coords;
    }

    public static void ResetSettings()
    {
        currentSpawnedBombs = 0;
        currentSpawnedCoins = 0;
    }

    public static void ResetCollectibles()
    {
        var bombs = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (var bomb in bombs)
        {
            GameObject.Destroy(bomb);
        }
        var coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (var c in coins)
        {
            GameObject.Destroy(c);
        }
        ResetSettings();
    }
}
