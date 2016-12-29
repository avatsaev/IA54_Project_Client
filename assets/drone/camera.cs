using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

	public GameObject drone;
	private Vector3 tmp;

	// Use this for initialization
	void Start () {
		tmp = new Vector3 ();
		tmp = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.position = drone.transform.position + tmp.position;
	}

	void FixedUpdate () {
		this.transform.position = drone.transform.position + tmp;
	}
}
