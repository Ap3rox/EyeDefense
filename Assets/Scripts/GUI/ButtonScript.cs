using UnityEngine;
using System.Collections;

//Script attached to the buttons on the GUI
public class ButtonScript : MonoBehaviour {

    public DataCollector DC;

    //Restart the whole game
    public void RestartGame()
    {
        Application.LoadLevel(0);
    }

    //Generate the heatmap
    public void GenerateResults()
    {
        DC.GenerateResult();
    }
}
