using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Manager to handle the score of the player
public class ScoreManager : MonoBehaviour {

    //Score value
    public static int score;

    //Text displaying the score
    private Text text;

	// Use this for initialization
	void Awake () {

        text = GetComponent<Text>();

        //Reset the score
        score = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

        //Update the score text
        text.text = score.ToString();

	}
}
