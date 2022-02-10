using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    //Enemy Copies
    [SerializeField] private GameObject golemCopy;
    [SerializeField] private GameObject zombieCopy;
    [SerializeField] private GameObject slimeCopy;

    //Boss Copies
    [SerializeField] private GameObject bossGolemCopy;
    [SerializeField] private GameObject bossZombieCopy;
    [SerializeField] private GameObject bossSlimeCopy;

    //Weaker Copies
    [SerializeField] private GameObject weakerGolemCopy;
    [SerializeField] private GameObject weakerZombieCopy;
    [SerializeField] private GameObject weakerSlimeCopy;

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
    public static float totalEnemyInLevel = 0; //Total enemies per wave
    public static float totalEnemyKilledInLevel = 0; //Total enemies per wave


    private int enemyCount = 0;

    public static int currentWave = 0; //Current wave #
    private int difficulty = 5; //5 = Normal?

    private int waveMaxCount = 6; //Max number of waves per day or level

    private int waveMinEnemies = 5; //Min possible number of enemies per wave
    private int waveMaxEnemies = 10; //Max possible number of enemies per wave

    private bool releaseWave = false;
    private bool isCleared = true;
    private bool waveStart = false;

    private int[] nMaxEnemyPerWave = { 0,0,0,0,0,0} ;

    [SerializeField] GameObject StageClearUI;

    // Start is called before the first frame update
    void Start()
    {
        ResetAll();
        RandomizeTotalEnemiesPerWave();
        //Add available enemies
        enemyCopies.Add(golemCopy);
        enemyCopies.Add(zombieCopy);
        enemyCopies.Add(slimeCopy);
    }

    private void Awake()
    {
        this.golemCopy.SetActive(false);
        this.zombieCopy.SetActive(false);
        this.slimeCopy.SetActive(false);

        this.bossGolemCopy.SetActive(false);
        this.bossZombieCopy.SetActive(false);
        this.bossSlimeCopy.SetActive(false);

        this.weakerGolemCopy.SetActive(false);
        this.weakerZombieCopy.SetActive(false);
        this.weakerSlimeCopy.SetActive(false);
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
            StageClearUI.SetActive(true);
        }

        //Prepare new wave
        GenerateNewWave();

        //Release wave
        ReleaseWave();

        // ------------------------------------------- FOR DEBUGGING --------------------------------------------------//
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

            if (currentWave >= waveMaxCount)
            {
                TimeBehavior.stageClear = true;
            }
        }
        // ------------------------------------------- FOR DEBUGGING --------------------------------------------------//

        if (waveStart)
        { 
            enemyCount = 0;
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null)
                {
                    enemyCount++;
                    Debug.Log("Killed: " + totalEnemyKilledInLevel + "/" + totalEnemyInLevel);

                    //totalEnemyKilledInLevel++;
                }
            }
            if (enemyCount == total)
            {
                enemyList.Clear();

                isCleared = true;
                currentWave++;

                start = false;

                if (currentWave >= waveMaxCount)
                    TimeBehavior.stageClear = true;
                waveStart = false;
            }
        }
    }

    void GenerateNewWave()
    {
        if (currentWave < waveMaxCount && isCleared && !TimeBehavior.isDaytime) // less than wavecount, nit cleared, night time
        {
            //total = Random.Range(waveMinEnemies, waveMaxEnemies + 1);

            total = nMaxEnemyPerWave[currentWave];
            isCleared = false;
            releaseWave = true;

            Debug.Log(total);
            Debug.Log("current wave = " + (currentWave+1) + " waveMaxCount" + waveMaxCount);

            //Add bosses + weaker boss + condition
            if ((currentWave + 1) % difficulty == 0 && TimeBehavior.day == 5) SpawnEnemy(bossGolemCopy);

            if (currentWave > difficulty && TimeBehavior.day == 5)
            {
                enemyCopies.Add(weakerGolemCopy);
            }
            waveStart = true;
        }
    }

    void ReleaseWave()
    {
        if (enemyList.Count < total && releaseWave)
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
        waveMaxCount = 5;
        waveMinEnemies = 5;//5
        waveMaxEnemies = 10;//10
        releaseWave = false;
        isCleared = true;
        totalEnemyInLevel = 0;
        totalEnemyKilledInLevel = 0;
        RandomizeTotalEnemiesPerWave();
    }


    public void RandomizeTotalEnemiesPerWave()
    {
        for (int i = 0; i < waveMaxCount; i++)
        {
            nMaxEnemyPerWave[i] = Random.Range(waveMinEnemies, waveMaxEnemies + (i + 1));
             totalEnemyInLevel += nMaxEnemyPerWave[i];
            Debug.Log("Num: " + nMaxEnemyPerWave[i]);
        }
         Debug.Log("total number of enemies: " + totalEnemyInLevel);

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