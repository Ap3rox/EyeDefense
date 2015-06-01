using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System;
using UnityEngine.UI;

//Script used to collect data from the player's gaze positon according to a grid covering the screen
//The values are stored in an array for further use
public class DataCollector : MonoBehaviour {

    //Crossahir who represents the gaze of the user
    public GameObject gaze;

    //Dimension of the resolution of the heatmap
    public int width = 10;
    public int height = 10;

    //Dimension of screen
    private float SW = Screen.width;
    private float SH = Screen.height;

    //Dimension of th cells for the heatmap
    private float cellHeight;
    private float cellWidth;

    //Cells wich containes the values
    private float[,] heatMapValues;

    //Attribute to write the txt file with the measurements
    private string filePath;
    private StreamWriter sw;

    //Main camera
    public Camera cam;

    //Game Manager to get the gameover
    public GameManager GM;

    //Is made to change the path from editor to build
    private string path;

    //Time parameters
    private float timer= 0;
    public float timeBeforeStart = 5;

    //GUI for the image of the heatmap
    public Image heatmap;       

	// Use this for initialization
	void Start () {

        //Check if its in editor or not
        if (!Application.isEditor)
        {
            path = Application.dataPath + "/../../";
        }
        else
        {
            path = Application.dataPath + "/../";
        }

        //Initializing the data values to 0s
        heatMapValues = new float[height,width];
        System.Array.Clear(heatMapValues, 0, heatMapValues.Length);

        //Calculate the dimension the cells
        cellHeight = SH / height;
        cellWidth = SW / height;

        //Initialisation of the csv file
        filePath = path + "/R/heatmap.csv";
        UnityEngine.Debug.Log("Write file - : " + filePath);
        sw = new StreamWriter(filePath);
        if (!File.Exists(filePath))
        {
            UnityEngine.Debug.Log("File has been created!");
        }
	
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (!GM.gameOver && timer >= timeBeforeStart)
        {
            //Screen coords of the gaze
            Vector3 screenPos = cam.WorldToScreenPoint(gaze.transform.position);

            //Pick the right cell to increment
            int i = (int)Mathf.Floor(screenPos.y / cellHeight);
            int j = (int)Mathf.Floor(screenPos.x / cellWidth);

            //Increase the corresponding cell value
            if (i >= 0 && i < height && j >= 0 && j < width)
            {
                heatMapValues[height - 1 - i, j]++;
            }
        }
        
        //Debug option
        if (Input.GetKeyDown("g"))
        {
            GenerateResult();
        }
	}

    //Generate results by completing the csv file
    public void GenerateResult()
    {

        UnityEngine.Debug.Log("Generating results");

        //Write a csv file using the heatmapValues
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                sw.Write(heatMapValues[i,j]);
                if (j != width - 1)
                {
                    sw.Write(",");
                }
            }
            if (i != height-1)
            {
                sw.WriteLine();
            }    
        }

        RunRscript();
        //call the image of the heatmap
        Invoke("LoadImage", 1);

    }

    //Display the image of the heatmap
    void LoadImage()
    {
        heatmap.enabled = true;

        //Load the png image created by the R program
        byte[] bytes = File.ReadAllBytes(path + "/R/HM.png");
        Texture2D texture = new Texture2D(900, 900, TextureFormat.RGB24, false);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 500, 500), new Vector2(0.5f, 0.0f), 1.0f);

        heatmap.sprite = sprite;
        
    }

    //Run the external R script using Rscript.exe
    void RunRscript(){
        UnityEngine.Debug.Log("Generating Heatmap");
        sw.Flush();
        sw.Close();
        try {
         Process myProcess = new Process();
         myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
         myProcess.StartInfo.CreateNoWindow = true;
         myProcess.StartInfo.UseShellExecute = false;
         string path2;
        //Path is different if in editor or build mode
         if (!Application.isEditor)
         {
             path2 = path + "/R/heatmap.R";
         }
         else
         {
             path2 = path + "/R/heatmap2.R";
         }
         myProcess.StartInfo.FileName = path + "/R/R-3.2.0/bin/Rscript.exe";
         myProcess.StartInfo.Arguments = "\"" + path2 + "\"";
         myProcess.EnableRaisingEvents = true;
         myProcess.Start();
         myProcess.WaitForExit();
         } catch (Exception e){
             print(e);        
         }
    }

}
    
