using UnityEngine;
using System.Collections;

//Script attached to the turret to make it follow the crosshair
public class TurretRotation : MonoBehaviour {

    //The target (crosshair)
    public GameObject target;

    //Speed of rotation
    public float rotSpeed;
	
	// Update is called once per frame
	void Update () {

        //Get the position of the crosshair in the height of the turret
        Vector3 planePosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        //Direction to look at
        Vector3 diff = planePosition - transform.position;

        if (diff != new Vector3 (0,0,0))
        {
            //Rotate the turret to make it face the crossahir
            Quaternion targetRotation = Quaternion.LookRotation(planePosition - transform.position);
            Quaternion shifted = Quaternion.Euler(0, targetRotation.eulerAngles.y - 90, 0);
            float str = Mathf.Min(rotSpeed * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, shifted, str);
        }
        
	
	}
}
