using UnityEngine;
using System.Collections;

public class intruderBehaviourScript : MonoBehaviour {

    public float moveOffset = 1.5f;
    Vector3 nextPosition;

	// Use this for initialization
	void Start () {

       
       

    }

    // Update is called once per frame
    void Update () {
        //bouger intrus
        nextPosition = new Vector3(0, 0, moveOffset) * Time.deltaTime;
        //nextPosition *= Time.deltaTime;
        transform.position += nextPosition; 
	}
}
