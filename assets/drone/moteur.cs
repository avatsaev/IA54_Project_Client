using UnityEngine;
using System.Collections;
using UnityEngine.UI;
	
	public class moteur
	{
		//Name moteur
		private string name ;
		private Vector3 axesRotation;
		
		//Vitesse rotation de l'hélice, et force Y et X sur les moteurs
		private float vitesse ;
		private GameObject support;
		private GameObject helice;
		
		public moteur (string name, int vitesse, GameObject support,GameObject helice, Vector3 axesRotation)
		{
			this.name = name;
			this.vitesse = vitesse;
			this.support = support;
			this.axesRotation = axesRotation;
			this.helice = helice;
		}
		
		public float getVitesse(){
			return vitesse;
		}
		
		public void setVitesse(float newVitesse){
			vitesse = newVitesse;
		}

		//To put the motor on
		public void live(){
			//Force axes to rotate the drone
			support.GetComponent<Rigidbody>().AddRelativeForce (axesRotation * vitesse);
			
			// Force to fly (up)
			support.GetComponent<Rigidbody>().AddRelativeForce ((Vector3.forward * (vitesse)));
			
			// To rotate the helice's drone
			if (name.Equals ("One") || name.Equals ("Two")) {
				helice.transform.rotation = helice.transform.rotation * Quaternion.Euler ((vitesse), 0, 0);	
			}else{
				helice.transform.rotation = helice.transform.rotation * Quaternion.Euler (-(vitesse), 0, 0);	
			}

	}
}


