using UnityEngine;
using System.Collections;

//Script attached to the turret to make it shoot
public class Fire : MonoBehaviour {

    //Parameters of fire
    public int damagePerShot = 10;
    public float timeBetweenBullets = 0.15f;
    public float range = 100;

    //Auto-fire activated
    public bool autoFire = true;

    //Shot fired
    public GameObject shot;

    float timer;
    //reference to the 2 cannons of the towers
    Transform rightPart;
    Transform leftPart;
    //Bool to alternate between each cannon
    bool isRightShooting = true;

    //Audio for laser shots
    private AudioSource AS;

	// Use this for initialization
	void Awake () {

        timer = 0;
        rightPart = this.transform.GetChild(0);
        leftPart = this.transform.GetChild(1);
        AS = this.GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {

        //Add time to timer for the rate of fire
        timer += Time.deltaTime;

        //Shoot when autofire
        if (autoFire && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }
        //Shot without auofire
        else if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }
	}

    //Called to shoot a bullet
    void Shoot()
    {
        //Reset the timer
        timer = 0f;
        if (AS.enabled)
        {
            //Play shot sound
            AS.Play();
        }
        //Shoot with the right cannon
        if (isRightShooting)
        {
            Instantiate(shot, rightPart.GetChild(1).position, rightPart.rotation);
            isRightShooting = false;
        }
            //Shoot with the left cannon
        else
        {
            Instantiate(shot, leftPart.GetChild(1).position, leftPart.rotation);
            isRightShooting = true;
        }


    }
}
