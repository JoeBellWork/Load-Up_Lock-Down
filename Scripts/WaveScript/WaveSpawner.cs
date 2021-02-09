using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { Spawning, Waiting, Counting};
    

    public WaveSingleton[] waves;
    private int nextWave = 0;


    // cell animation between waves

    public Animator anim;
    //

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;



    private float searchCountdown = 1f;

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("MEnemy").Length == 0 && GameObject.FindGameObjectsWithTag("SEnemy").Length == 0)
            {
                return false;
            }            
        }
        return true;
    }

    private SpawnState state = SpawnState.Counting;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }



    void Update()
    {
        if(state == SpawnState.Waiting)
        {
            if(!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }


        if(waveCountdown <= 0)
        {
           if(state !=SpawnState.Spawning)
           {
                StartCoroutine(SpawnWave(waves[nextWave]));
           }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }


    public  void WaveCompleted()
    {
        Debug.Log("Wave COMPLETED!");
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            
            Debug.Log(" ALL WAVES COMPLETE!!");
            Debug.Log("Looping");

            GameObject theWaveSpawner = GameObject.Find("WaveManager");
            WaveSingleton wave = theWaveSpawner.GetComponent<WaveSingleton>();
            wave.mCount = wave.mCount + (Random.Range(1, 6));
            wave.sCount = wave.mCount + (Random.Range(1, 6));
                       
        }
        else
        {
            nextWave++;
        }
    }


    IEnumerator SpawnWave(WaveSingleton _wave)
    {
        anim.SetBool("CellRise", true);// cell rise animation
        Debug.Log("Spawning Wave" + _wave.name);
        state = SpawnState.Spawning;
        

        for (int i = 0; i < _wave.sCount; i++)
        {
            SpawnEnemy(_wave.Senemy);
            yield return new WaitForSeconds(1f * _wave.rate);
        }
        for (int i = 0; i < _wave.mCount; i++)
        {
            SpawnEnemy(_wave.Menemy);
            yield return new WaitForSeconds(1f * _wave.rate);
        }

        state = SpawnState.Waiting;
        anim.SetBool("CellRise", false);
        yield break;
        
    }






    void SpawnEnemy(Transform _enemy)
    {
        if(spawnPoints.Length == 0)
        {
            Debug.LogError("No SPAWN POINTS SET");
        }


        Transform _SP = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _SP.position, _SP.rotation);
        Debug.Log("Spawning" + _enemy.name);
    }




}
