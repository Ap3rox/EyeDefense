using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Script attached to the player to manage his health
public class PlayerHealth : MonoBehaviour {

    public int StartingHealth;          //Amount of health at start
    private int currentHealth;          //Amount of current health

    public bool isDead = false;

    //Detonator of the dead explosion
    public Detonator detonator;

    //Text displaying his health
    public Text healthText;

    //Audio for the explosion
    public AudioSource audioSource;

	// Use this for initialization
	void Awake () {
        currentHealth = StartingHealth;
        healthText.text = StartingHealth.ToString();
	}

    //Called when the player is taking damage from an enemy
    public void TakeDamage(int amount)
    {

        //Check if it kills him
        if (currentHealth - amount <= 0)
        {
            //Launch the detonator to make the explosion
            detonator.Explode();
            //Set the player as dead
            isDead = true;
            healthText.text = "0";
            if (audioSource.enabled)
            {
                audioSource.Play();
            }
            Destroy(this.gameObject);
        }
        else
        {
            //Decrese his health
            currentHealth -= amount;
            healthText.text = currentHealth.ToString();
        }

    }

}
