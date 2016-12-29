using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class regulateur {

	//Data which is used to move the drone
	private float hauteur = 0;
	private CommandeDrone commande;

	private bool chooseControlKey;
	private bool chooseControlAndroid;

	//Data to apply a force on the motor
	private float vitesseAllMoteur = 0;
	private float vitesseToUp = 0;

	private GameObject drone;
	private moteur[] moteurDrone;
	private physic worldPhysic;
	private BehaviorMovement lookForDirection;

	private PID PIDxz;
	private PID PIDy;
	private PID PIDOy;

	public regulateur(GameObject drone,moteur[] moteurDrone){
		this.drone = drone;
		this.moteurDrone = moteurDrone;
		this.worldPhysic = new physic (drone);
		this.PIDxz = new PID (0.7F, 0.008F, 0.0005F);
		this.PIDy = new PID (5.0F, 0.0004F, 0.005F);
		this.PIDOy = new PID (0.003F, 0.000000056F,0.0000005F);
		this.lookForDirection = new BehaviorMovement ();
	}


	//SETTER
	public void setHauteur(float haut){
		this.hauteur = haut;
	}

	public void setControlWithKey(bool drapeau){
		this.chooseControlKey = drapeau;
	}

	public void setControlWithAndroid(bool drapeau){
		this.chooseControlAndroid = drapeau;
	}
	
	public void setCommande(CommandeDrone monter){
		this.commande = monter;
	}
	 
	public void setvitesseToUp(float v){
		float testVitesse = this.vitesseToUp += v;
		this.vitesseToUp = worldPhysic.checkVitessePhysic (testVitesse);
	}

	public float getVitesseToUp(){
		return this.vitesseToUp;
	}

	//GETTER
	public float getHauteur(){
		return this.hauteur;
	}

	public float getVitesse(){
		return this.vitesseAllMoteur;
	}

	public CommandeDrone getCommande(){
		return this.commande;
	}

	public void live(){

		if (!chooseControlAndroid) {
			if (this.chooseControlKey) {
				setMoveWithKey ();
			} else {
				setMoveWithVecteur (new Vector3 (2, 0, 0));
			}
		} else {
			if (!this.chooseControlKey) {
				setMoveWithAndroid();
			}
		}
		for (int j = 0; j < moteurDrone.Length; j++) {
			moteurDrone[j].live ();
		}
	}


	private void setMoveWithKey(){
	

		float angleX = 0.0F;
		float angleZ = 0.0F;
		float[] angle = new float[2];

		//Tous les autres PID sont censé être à 0
		switch (getCommande ()) {

		case CommandeDrone.Stabilise :
		case CommandeDrone.RotationGauche :
		case CommandeDrone.RotationDroite :
		case CommandeDrone.Droite :
		case CommandeDrone.Gauche :
		case CommandeDrone.Haut :
		case CommandeDrone.Bas :
			angle = lookForDirection.manageMovementAngle(getCommande());
			angleX = angle[0];
			angleZ = angle[1];
			break;

		case CommandeDrone.Monter :
			setvitesseToUp(6);
			break;

		case CommandeDrone.Descendre :
			setvitesseToUp(-6);
			break;

			 
		}

		prepareMove (angleX, angleZ);

	}

	private void setMoveWithAndroid(){
		
		
		float angleX = 0.0F;
		float angleZ = 0.0F;
		float[] angle = new float[2];
		
		//Tous les autres PID sont censé être à 0
		switch (getCommande ()) {
			
		case CommandeDrone.Stabilise :
		case CommandeDrone.RotationGauche :
		case CommandeDrone.RotationDroite :
			angle = lookForDirection.manageMovementAngle(getCommande());
			angleX = angle[0];
			angleZ = angle[1];
			break;
			
		case CommandeDrone.Monter :
			setvitesseToUp(6);
			break;
			
		case CommandeDrone.Descendre :
			setvitesseToUp(-6);
			break;
			
			
		}
		
		prepareMove (angleX, angleZ);
		
	}
	
	public void setMoveWithVecteur(Vector3 mouvement){

		float[] directionFinal = new float[2];

		//Indication qu'on effectue les mouvements avec un vecteur
		if (mouvement.y != 0) {
			setCommande (CommandeDrone.Vecteur);
		}

		//Récupère Monter//descendre
		if (mouvement.y < 0) {
			this.vitesseToUp = mouvement.y + (-1 * mouvement.magnitude);
		} else {
			this.vitesseToUp = mouvement.y + mouvement.magnitude;
		}

		//Préparation des calcules
		if (mouvement.x != 0 || mouvement.y != 0) {
			mouvement.Normalize ();

			float[] directionX = new float[2];
			float[] directionZ = new float[2];

		//Déplacement sur la coordonnée x
			if (mouvement.x > 0) {
				directionX [0] = 30.0F;
				directionX [1] = 0.0F;
			} else {
				if (mouvement.x < 0) {
					directionX [0] = 30.0F;
					directionX [1] = 0.0F;
				}
			}

			//Déplacement sur la coordonnée y
			if (mouvement.z > 0) {
				directionZ [0] = 0.0F;
				directionZ [1] = -30.0F;
			} else {
				if (mouvement.z < 0) {
					directionZ [0] = 0.0F;
					directionZ [1] = -30.0F;
				}
			}

			//Conversion du vecteur en force pour le drone
			directionFinal [0] = ((directionX [0] * mouvement.x) + (directionZ [0] * mouvement.z));// * mouvement.magnitude;
			directionFinal [1] = ((directionX [1] * mouvement.x) + (directionZ [1] * mouvement.z));// * mouvement.magnitude;


		} else {
			directionFinal[0] = 0;
			directionFinal[0] = 0;
		}
	
		prepareMove (directionFinal [0], directionFinal [1]);
		
	}

	public void prepareMove(float angleX, float angleZ){
		float pidReturnZ = 0.0F;
		float pidReturnX = 0.0F;
		float pidReturnY = 0.0F;


		pidReturnZ = PIDxz.updatePID (angleX, (float)drone.transform.eulerAngles.z, Time.deltaTime);
		pidReturnX = PIDxz.updatePID (angleZ, (float)drone.transform.eulerAngles.x, Time.deltaTime);
		pidReturnY = PIDy.updatePID (getHauteur(), (float)drone.GetComponent<Rigidbody>().position.y, Time.deltaTime);
		moveStabiliseAxe(pidReturnZ,pidReturnX,pidReturnY);
	}

	private void moveStabiliseAxe(float pidOz, float pidOx, float pidOy){


		int[] dirStabiliseOx = lookForDirection.getStabiliseOx ();
		int[] dirStabiliseOz = lookForDirection.getStabiliseOz ();

		float PIDForceOx = pidOx * 0.1F;
		float PIDForceOz = pidOz * 0.1F;
		float PIDForceOy = pidOy * 1.0F;

		//forceDroneStabilise renvoie une force total, on la divise par 4 pour obtenir la force pour chaque moteur
		//Pour la hauteur

		float sForce = 0.0F;

		//Pour ajuster la hauteur avec le PID
		if (getCommande () != CommandeDrone.Monter && getCommande () != CommandeDrone.Descendre && getCommande() != CommandeDrone.Vecteur) {
			this.vitesseToUp = 0;
			sForce = (worldPhysic.forceDroneStabilise () / 4.0F) + PIDForceOy ;
			sForce = worldPhysic.checkVitessePhysic(sForce);
		} else {
			sForce = (worldPhysic.forceDroneStabilise () / 4.0F) + getVitesseToUp () ;
			setHauteur(drone.GetComponent<Rigidbody>().position.y);
		}

		//Pour axes Ox et Oz
		float[] dir = new float[4];
		if (getCommande () != CommandeDrone.Vecteur) {
			//On ne stabilise que sur OZ et OX
			for (int x = 0; x < dir.Length; x++) {
				dir [x] = (PIDForceOx * dirStabiliseOx [x]) + (PIDForceOz * dirStabiliseOz [x]);
			}
		} else {
			//On stabilise sur OZ, OX, et OY
			float pidReturnOY = PIDOy.updatePID (180, (float)drone.transform.eulerAngles.y, Time.deltaTime);
			float[] dirRotationOY = {1.0F,1.0F,1.0F,1.0F};
			if(180 > drone.transform.eulerAngles.y && 0 < drone.transform.eulerAngles.y ){
				dirRotationOY = lookForDirection.manageMovementVitesseMoteur (CommandeDrone.RotationDroite);
			}else{
				dirRotationOY = lookForDirection.manageMovementVitesseMoteur (CommandeDrone.RotationGauche);
			}
			for (int x = 0; x < dir.Length; x++) {
				dir [x] = ((PIDForceOx * dirStabiliseOx [x]) + (PIDForceOz * dirStabiliseOz [x]) + (pidReturnOY * dirRotationOY[x]))/2;
			}
		}

		//Pour les vitesses des moteurs (utilisées pour les rotations)
		float[] dirRotation = {1.0F,1.0F,1.0F,1.0F};
		if (getCommande () == CommandeDrone.RotationDroite || getCommande () == CommandeDrone.RotationGauche || getCommande() == CommandeDrone.Vecteur) {
			dirRotation = lookForDirection.manageMovementVitesseMoteur (getCommande ());
		}

		float[] forceTotale = new float[4];

		//Ajout des forces sur le PID hauteur et sur les angles
		for (int x = 0; x < moteurDrone.Length; x++) {
			forceTotale [x] = dir[x] + sForce;
		}

		// Ajout des forces totales avec des coefficiens de rotations
		moteurDrone [0].setVitesse (dirRotation[0] * forceTotale [0]);
		moteurDrone [1].setVitesse (dirRotation[1] * forceTotale [1]);
		moteurDrone [2].setVitesse (dirRotation[2] * forceTotale [2]);
		moteurDrone [3].setVitesse (dirRotation[3] * forceTotale [3]);

		//On envoie la vitesse au drone
		for (int j = 0; j < moteurDrone.Length; j++) {
			this.vitesseAllMoteur += moteurDrone[j].getVitesse();
		}
		
		this.vitesseAllMoteur /= 4;
	}



/*
	private void moveLateral(float v, CommandeDrone direction){
		
		float PIDForce = v * 0.2F;
		float sForce = worldPhysic.forceDroneStabilise()/4;
		
		BehaviorMovement lookForDir = new BehaviorMovement ();
		int[] dir = lookForDir.manageMovement (direction);
		
		moteurDrone [0].setVitesse (sForce +(dir[0] * PIDForce));
		moteurDrone [1].setVitesse (sForce + (dir[1] * PIDForce));
		moteurDrone [2].setVitesse (sForce + (dir[2] * PIDForce));
		moteurDrone [3].setVitesse (sForce + (dir[3] * PIDForce));
		
		for (int j = 0; j < moteurDrone.Length; j++) {
			this.vitesseAllMoteur += moteurDrone[j].getVitesse();
		}
		
		this.vitesseAllMoteur /= 4;
		
	}

	//On calcule la force à appliquer pour déaccélérer le drone.
	// La physique se charge de la déaccéleration maximun
	// Cette méthode n'est pus utile car on utilise les drags pour simuler la résistance à l'air
	private void StabiliseDrone(){
		float forceOneMotor = worldPhysic.accelerationToForce (drone.rigidbody.velocity.magnitude);

		forceOneMotor = worldPhysic.checkVitessePhysic (forceOneMotor);

		//On veux une force descendante si on a une accélération vers le haut
		if (drone.rigidbody.velocity.y > 0) {
			forceOneMotor *= -1;
		}

		// Ajout à la force stabilisant, la force de déaccélération de telle manière que lorsque l'accélération est null il ne reste plus que la force pour aller contre la gravitée
		forceOneMotor = (worldPhysic.forceDroneStabilise() / 4) + forceOneMotor;

		changeVitesse (forceOneMotor);
	}

	//Change directement la vitesse du drône (utilisé seulement pour les tests)
	private void changeVitesse(float newVitesse){
		this.vitesseAllMoteur = newVitesse;
		for (int j = 0; j < moteurDrone.Length; j++) {
			moteurDrone[j].setVitesse(getVitesse());
		}
	}*/
	
}