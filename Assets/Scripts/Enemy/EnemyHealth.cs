using UnityEngine;
using System.Collections;

//Script attached to an enemy to specify his health
public class EnemyHealth : MonoBehaviour {

    public int StartingHealth;          //Amount of health at start
    private int currentHealth;          //Amount of current health
    public int scoreValue = 10;         //Score value received by killing it

    //Audio for explosion
    private AudioSource SA;

    //Destroy clip
    public AudioClip clip;

    //Detonator of the dead explosion
    public Detonator detonator;

	// Use this for initialization
	void Awake () {
        currentHealth = StartingHealth;
        SA = this.GetComponent<AudioSource>();
	}

    //Called when the enemy receive a shot
    void TakeDamage(int amount){

        //Check if this shot will kill him
        if(currentHealth - amount <= 0){

            //Play explosion sound
            SA.PlayOneShot(clip);   

            //Disable the enemy
            Renderer[] allChildren = GetComponentsInChildren<Renderer>();
            foreach (Renderer child in allChildren)
            {
                child.enabled = false;
            }
            //Launch the detonator to make an explosion
            GameObject det = Instantiate(detonator, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(det, 2.5f);
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponent<EnemyAttack>().enabled = false;
            Destroy(this.gameObject, clip.length);
            //Update the score
            ScoreManager.score += scoreValue;
        }else{
            //Lower his health
            currentHealth -= amount;
        }

    }

    //Detect when the enemy will receive a shot
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Shot"){
            //Destroy the bullet
            Destroy(other.gameObject);
            int damage = other.gameObject.GetComponent<ShotMover>().damage;
            TakeDamage(damage);
        }
    }
}
