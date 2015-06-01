using UnityEngine;
using System.Collections;
using TETCSharpClient;
using TETCSharpClient.Data;
using Assets.Scripts;

//This script is attached to the crosshair to make it follow the gaze of the user
public class Aiming : MonoBehaviour, IGazeListener {

    //It defines if we use the Eye Trcker or the mouse
    public bool isETActive;

    //GazeDataValidator to get the eye tracking values
    private GazeDataValidator gazeUtils;

    //Main camera
    private Camera cam;

	// Use this for initialization
	void Start () {

        //initialising GazeData stabilizer
        gazeUtils = new GazeDataValidator(30);

        //register for gaze updates
        GazeManager.Instance.AddGazeListener(this);

        cam = Camera.main;

	
	}
	
	// Update is called once per frame
	void Update () {

        //Check if the eye tracking mode is enabled
        if (isETActive)
        {
            //Get coords of the gaze
            Point2D gazeCoords = gazeUtils.GetLastValidSmoothedGazeCoordinates();

            if (null != gazeCoords)
            {
                //map gaze indicator
                Point2D gp = UnityGazeUtils.getGazeCoordsToUnityWindowCoords(gazeCoords);

                //map to screenpoint
                Vector3 screenPoint = new Vector3((float)gp.X, (float)gp.Y, cam.nearClipPlane + .1f);


                Vector3 planeCoord = cam.ScreenToWorldPoint(screenPoint);
                //Set the crosshair position to the player's gaze point
                this.transform.position = planeCoord;
            }
        }
        else
        {
            //Aiming via the mouse input
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.y = this.transform.position.y;
            this.transform.position = mousePos;

        }

     }


    public void OnGazeUpdate(GazeData gazeData)
    {
        //Add frame to GazeData cache handler
        gazeUtils.Update(gazeData);
    }
}
