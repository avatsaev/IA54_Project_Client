using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class physic {
	private GameObject drone;

	private const double ConstantMaxAcceleration = 2.8;
	private const double ConstantMaxAccelerationDescente = 1;

	public physic(GameObject drone){
		this.drone = drone;
	}

	//Regarde si la force est bonne pour ne pas dépasser une accélération
	public float checkVitessePhysic(float vitesse){


		// On ne dépasse pas une acceleration
		if(CurrentAcceleration(vitesse) > maxAcceleration()){
			return (float)((forceDroneStabilise () * ConstantMaxAcceleration)/4);
		}

		if(CurrentAcceleration(vitesse) < (-1 * maxAccelerationDescente())){
			return (float)(-1 * (forceDroneStabilise () * ConstantMaxAccelerationDescente)/4);
		}

		return vitesse;

	}

	//Max vitesse que l'on veux, On ne veux pas dépasser une vitesse pour le réalisme
	private float maxAcceleration(){
		// P = m * a
		float acceleration = (float) (forceDroneStabilise () * ConstantMaxAcceleration) / drone.GetComponent<Rigidbody>().mass;
		return acceleration;
	}

	//Max vitesse que l'on veux, On ne veux pas dépasser une vitesse
	private float maxAccelerationDescente(){
		// P = m * a
		float acceleration = (float) (forceDroneStabilise () * ConstantMaxAccelerationDescente) / drone.GetComponent<Rigidbody>().mass;
		return acceleration;
	}

	//Calcule l'acceleration en fonction d'une force sur les quatre moteurs
	private float CurrentAcceleration(float force ){
		// a = F / m
		float accelerationCurrent = (force * 4) / drone.GetComponent<Rigidbody>().mass;
		return accelerationCurrent;
	}

	//Force totale appliqué sur le drone pour être stationnaire
	// Force normale
	public float forceDroneStabilise(){
		// P = m * g (g constante gravitionnelle) 
		float forceDroneAppZ = (float) drone.GetComponent<Rigidbody>().mass * Physics.gravity.magnitude *  (1+ (1 - Mathf.Cos(drone.transform.eulerAngles.z * Mathf.Deg2Rad))) ;
		float forceDroneAppX = (float) drone.GetComponent<Rigidbody>().mass * Physics.gravity.magnitude *  (1+ (1 - Mathf.Cos(drone.transform.eulerAngles.x * Mathf.Deg2Rad))) ;

		return (forceDroneAppZ + forceDroneAppX)/2;
	}

	// Calcule la force en fonction d'une acceleration, retour de l'acceleration d'un seul moteur
	public float accelerationToForce(float acceleration){
		// F = a * m
		float force = acceleration * drone.GetComponent<Rigidbody>().mass;
		return force;
	}


}
