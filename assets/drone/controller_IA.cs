using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using moteur;

public class controller_IA : MonoBehaviour {
	
	private int vitesse = 0;
	private GameObject[] helices;
	private GameObject[] supports;
	private GameObject leader;
	
	private moteur[] moteurDrone = new moteur[4];
	private regulateur reguleDrone;
	
	public int portNumber = 8235;
	
	public bool controlleWithVecteur = true;

	void Start () {
		
		vitesse = 0;
		helices = GameObject.FindGameObjectsWithTag("helice");
		supports = GameObject.FindGameObjectsWithTag("support");
		leader = GameObject.FindGameObjectWithTag("leader");
		
		moteurDrone [0] = new moteur ("One", 0, supports [0],helices[0], Vector3.down);
		moteurDrone [1] = new moteur ("Two", 0, supports [1],helices[1], Vector3.up);
		moteurDrone [2] = new moteur ("Three", 0, supports [2],helices[2], Vector3.right);
		moteurDrone [3] = new moteur ("Four", 0, supports [3],helices[3], Vector3.left);
		
		reguleDrone = new regulateur (leader, moteurDrone);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate(){

		reguleDrone.setControlWithKey (controlleWithVecteur);

		reguleDrone.live ();
		
	}

	public void setMoveVecteur(Vector3 vecteurMove){
		reguleDrone.setMoveWithVecteur (vecteurMove);
	}


}
