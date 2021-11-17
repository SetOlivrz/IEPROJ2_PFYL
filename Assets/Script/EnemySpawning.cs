using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    //Enemy Copies
    [SerializeField] private GameObject golemCopy;

    //Boss Copies
    [SerializeField] private GameObject bossGolemCopy;

    //Weaker Copies
    [SerializeField] private GameObject weakerGolemCopy;

    //Enemy List
    [SerializeField] public List<GameObject> enemyList;
    [SerializeField] private List<GameObject> enemyCopies;

    //Spawn Locations
    [SerializeField] private Transform[] spawnLocations;

    //Time
    private float ticks = 0.0f;
    private float SPAWN_INTERVAL = 5.0f;
    private bool start = false;

    //Wave
    private int total = 0; //Total enemies per wave
    public static int currentWave = 0; //Current wave #
    private int waveMaxCount = 3; //Max number of waves per day or level
    private int waveMaxEnemies = 2; //Max possible number of enemies per wave
    private bool releaseWave = false; 
    private bool isCleared = true;

    // Start is called before the first frame update
    void Start()
    {
        //Add available enemies
        enemyCopies.Add(golemCopy);
    }

    private void Awake()
    {
        this.golemCopy.SetActive(false);
        this.bossGolemCopy.SetActive(false);
        this.weakerGolemCopy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //For debugging
        if (Input.GetKeyDown(KeyCode.Q))
        {
            start = true;
        }

        if (currentWave >= waveMaxCount)
        {
            ResetAll();
        }

        //Prepare new wave
        GenerateNewWave();

        //Release wave
        ReleaseWave();

        //Delete wave: For debugging (if player cleared a wave)
        if (start /*|| TimeBehavior.isDaytime*/)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].SetActive(false);
                //Destroy(enemyList[i]);
                enemyList[i].GetComponent<EnemyBehavior>().OnKill();
            }
            enemyList.Clear();

            isCleared = true;
            currentWave++;

            start = false;

            if(currentWave >= waveMaxCount)
            TimeBehavior.stageClear = true;
        }
    }

    void GenerateNewWave()
    {
        if (currentWave < waveMaxCount && isCleared && !TimeBehavior.isDaytime)
        {
            total = Random.Range(5, waveMaxEnemies + 1);
            isCleared = false;
            releaseWave = true;

            Debug.Log(currentWave + 1 + " " + waveMaxCount);

            if(currentWave == 1 && TimeBehavior.day == 2) SpawnEnemy(bossGolemCopy);

            if (currentWave > 1 && TimeBehavior.day == 2)
            {
                enemyCopies.Add(weakerGolemCopy);
            }
        }
    }

    void ReleaseWave()
    {
        if(enemyList.Count < total && releaseWave)
        {
            this.ticks += Time.deltaTime;

            if (this.ticks > this.SPAWN_INTERVAL)
            {
                this.ticks = 0.0f;
                this.SPAWN_INTERVAL = Random.Range(5.0f, 10.0f);

                SpawnEnemy(enemyCopies[Random.Range(0, enemyCopies.Count)]);
            }
        }

        else
        {
            releaseWave = false;
        }
    }

    void SpawnEnemy(GameObject toCopy)
    {
        GameObject enemyCopy = toCopy;
        Vector3 newLocation = this.spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position;
        enemyCopy = ObjectUtils.SpawnDefault(toCopy, this.transform.parent, newLocation);
        enemyCopy.SetActive(true);
        enemyList.Add(enemyCopy);
    }

    void ResetAll()
    {
        //Time
        ticks = 0.0f;
        SPAWN_INTERVAL = 5.0f;
        start = false;

        //Wave
        total = 0; 
        currentWave = 0; 
        waveMaxCount = 3; 
        waveMaxEnemies = 2; 
        releaseWave = false;
        isCleared = true;
    }
}

public class ObjectUtils
{
    public static GameObject SpawnDefault(GameObject toSpawn, Transform parent, Vector3 localPos)
    {
        GameObject myObj = GameObject.Instantiate(toSpawn, parent);
        myObj.transform.localPosition = localPos;

        return myObj;
    }
}
