using UnityEngine;
using System.Collections;

//Script attached to shot to make it move
public class ShotMover : MonoBehaviour {

    public float speed;         //Speed of the shot
    public int damage;          //Damage of the shot
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
}
