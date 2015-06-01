using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//This manager handle the spawning of the enemies.
//Enemies comes in waves wich are part of a round
//On each round the difficulty is increased
public class EnemyManager : MonoBehaviour {

    public float radius;                    //Radius to have the distance of spawn

    public float wavesCooldown;             //Time between each waves
    public int enemyPerWave;                //Enemy to spawn at each wave
    public int wavesPerRound;               //Number of waves during one round
    private int spawnPointNumber;           //Number of spawnPoint

    public float difficulty = 0.1f;         //Difficulty level from 0 to 1
    public float enemySpeed;                //Initial speed of the enemys

    public GameObject enemy;                //Enemy to spawn

    Vector2[] spawnPositions;               //Positions of the spawnPoints
    Quaternion[] spawnRotations;            //Rotations of the spawns

    public PlayerHealth playerHealth;       // Reference to the player's heatlh.

    private int spawnWave = 0;              //Number of waves that has spawned
    private int round = 0;                  //Number of to round to spawn - 1 round has roundLenght waves

    public Text roundText;                 //UI showing the rounf number

    private List<int> Index = new List<int>();                //List of index
    private List<int> freeIndex;            //List of free index
        
	// Use this for initialization
	void Start () {

        //Calcultae the number spawnPoints
        spawnPointNumber = enemyPerWave * wavesPerRound;

        //Create a list of index
        for (int i = 0; i < wavesPerRound; i++)
        {
            Index.Add(i);
        }

        createSpawnPositions();

        //Create a new wave every wavesCooldown seconds
        InvokeRepeating("Spawn", 1.0f, wavesCooldown);
	}

    //Spawn a wave of enemies
    void Spawn()
    {

        //No spawn if the player is dead
        if (playerHealth.isDead)
        {
            return;
        }

        //Every wavesPerRound waves the round number is increased
        if (spawnWave % wavesPerRound == 0)
        {
            round++;
            roundText.text = "Round: " + round;
            //The spawn points are available again
            freeIndex = Index.GetRange(0, Index.Count); ;
        }


        //Position picked at random between the positions thats aren't already used during this round
        int rnd = Random.Range(0, freeIndex.Count);
        int index = freeIndex[rnd];
        freeIndex.Remove(index);

        //The speed increace according to the round number and the difficulty
        float speed = enemySpeed + round * difficulty;

        //Spawn a wave with enemyPerwave enemy at the random positions
        for (int i = 0; i < enemyPerWave; i++)
        {
            int ind = index + i * (spawnPointNumber / enemyPerWave);
            GameObject invader = Instantiate(enemy, new Vector3(spawnPositions[ind].x, 0, spawnPositions[ind].y), spawnRotations[ind]) as GameObject;
            invader.GetComponent<NavMeshAgent>().speed = speed;
        }

        //Increase the number of wave spawned
        spawnWave++;
    }
	

    //Called to creat the different sapwn positions
    void createSpawnPositions()
    {
        spawnPositions = new Vector2[spawnPointNumber];
        spawnRotations = new Quaternion[spawnPointNumber];
        //Angle between each spawn spot
        float angle = 2 * Mathf.PI / spawnPointNumber;

        for (int i = 0; i < spawnPointNumber; i++)
        {
            spawnPositions[i] = polarToCartesian(i*angle, radius);
            //Angle to look at the center
            float rotAngle = -(90 + ((i*angle) * 180 / Mathf.PI));
            spawnRotations[i] = Quaternion.Euler(0, rotAngle, 0);
        }
        
    }

    //Convert from polar coodinates to cartesian
    Vector2 polarToCartesian(float angle, float radius)
    {
        return new Vector2(radius * Mathf.Cos(angle), radius*Mathf.Sin(angle));
    }
}
