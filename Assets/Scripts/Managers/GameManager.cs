using UnityEngine;
using System.Collections;

//Manager to handle the different phase of the game
public class GameManager : MonoBehaviour {

    //Health of the player
    public PlayerHealth PH;

    //Is the game over
    public bool gameOver = false;

    //Reference to the HUD of the results
    public GameObject HUD;

    //Reference to all audio source of the game
    public AudioSource[] ASs;
	
	// Update is called once per frame
	void Update () {

        //Restart the game
         if (Input.GetKeyDown("r"))
        {
            RestartGame();
        }

        //Mute all audio sources
         if (Input.GetKeyDown("m"))
         {
             Mute();
         }

         //Leave the game
         if (Input.GetKey(KeyCode.Escape))
         {
             Application.Quit();
         }

        //Set the gameover
         if (PH.isDead)
         {
             gameOver = true;
             //Display results
             HUD.SetActive(true);

         }
	
	}

    //Called to restart the game
    void RestartGame(){
        Application.LoadLevel(0);
    }

    //Called to mute all audios
    void Mute()
    {
        foreach (AudioSource a in ASs)
        {
            a.enabled = !a.enabled;
        }
    }

}
