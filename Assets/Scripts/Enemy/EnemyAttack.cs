using UnityEngine;
using System.Collections;


//SCript attached to enemy to parametrize his attack
public class EnemyAttack : MonoBehaviour {

    //Parameters of fire
    public int damagePerShot;
    public float timeBetweenBullets;

    float timer;            //Timer between attacks

    GameObject player;               // Reference to the player's position.
    PlayerHealth PH;                 //Referenc to the player's health
    NavMeshAgent nav;               // Reference to the nav mesh agent.

    bool InRange = false;           //Specify if the unit is in range to attack

    //Particles at the edge of the cannon
    private ParticleSystem particles;

    //Audio for shots
    private AudioSource AS;

	// Use this for initialization
	void Awake () {

        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PH = player.GetComponent<PlayerHealth>();
        }
        nav = GetComponent<NavMeshAgent>();

        //Set destination to be the player
        nav.SetDestination(player.transform.position);

        particles = this.GetComponentInChildren<ParticleSystem>();

        AS = this.GetComponent<AudioSource>();

	}

    void Update()
    {
         //Add time to timer for the rate of fire
        timer += Time.deltaTime;
        if (InRange && !PH.isDead)
        {
            Attack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        // If the entering collider is the player the enemy become in range to attack him
        if (other.gameObject == player)
        {
            nav.enabled = false;
            InRange = true;
        }
    }

    void Attack()
    {
        //Check if it has not shoot a few time ago
        if (timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            if (AS.enabled)
            {
                //Play cannon sound
                AS.Play();
            }
            //Reste the timer
            timer = 0;
            //Play particles on the cannon
            particles.Play();
            //Reduce health of the player
            PH.TakeDamage(damagePerShot);
        }

    }


}
