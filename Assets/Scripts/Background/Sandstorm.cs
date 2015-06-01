using UnityEngine;
using System.Collections;

public class Sandstorm : MonoBehaviour {

    //Ref to the ground
    public Camera cam;

    public float rotSpeed;
    
    private float x;
    private float y;

    //Polar coordonate with angle in rads
    private float radius;
    private float angle;

	// Use this for initialization
	void Start () {
        radius = cam.orthographicSize;
        angle = 0;
	}
	
	// Update is called once per frame
	void Update () {
        x = radius * Mathf.Cos(angle);
        y = radius * Mathf.Sin(angle);
        transform.position = new Vector3(x,0,y);
        Vector3 relativePos = new Vector3(0,0,0) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
        angle += 0.1f;
	}
}
