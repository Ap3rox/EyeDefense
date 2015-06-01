using UnityEngine;
using System.Collections;

//This script is used to detroy every gameObject going out of the game area specially the bullets
public class DestroyByBoundary : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
