using UnityEngine;
using System.Collections;

public class SetOrthogonalSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Camera>().orthographic = true;
        GetComponent<Camera>().orthographicSize =  Screen.height/2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
