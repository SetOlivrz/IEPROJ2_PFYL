using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    //Enemy Copies
    [SerializeField] private GameObject golemCopy;

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
    private int currentWave = 0; //Current wave #
    private int waveMaxCount = 2; //Max number of waves per day or level
    private int waveMaxEnemies = 5; //Max possible number of enemies per wave
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
    }

    // Update is called once per frame
    void Update()
    {
        //For debugging
        if (Input.GetKeyDown(KeyCode.Q))
        {
            start = true;
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
                Destroy(enemyList[i]);
            }
            enemyList.Clear();

            isCleared = true;
            currentWave++;

            start = false;
        }
    }

    void GenerateNewWave()
    {
        if (currentWave < waveMaxCount && isCleared /*&& !TimeBehavior.isDaytime*/)
        {
            total = Random.Range(2, waveMaxEnemies + 1);
            isCleared = false;
            releaseWave = true;

            Debug.Log(currentWave + 1 + " " + waveMaxCount);
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

                GameObject enemyCopy = enemyCopies[Random.Range(0, enemyCopies.Count)];
                Vector3 newLocation = this.spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position;
                enemyCopy = ObjectUtils.SpawnDefault(golemCopy, this.transform.parent, newLocation);
                enemyCopy.SetActive(true);
                enemyList.Add(enemyCopy);
            }
        }

        else
        {
            releaseWave = false;
        }
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
