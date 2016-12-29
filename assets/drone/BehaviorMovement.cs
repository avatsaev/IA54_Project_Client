using UnityEngine;
using System.Collections;

public class BehaviorMovement {


	public  BehaviorMovement(){
	
	}

	public int [] manageMovement(CommandeDrone direction){

		switch (direction) {

		case CommandeDrone.Droite:
			return getDroite();
			//break;
		
		case CommandeDrone.Gauche:
			return getGauche();
			//break;
		
		case CommandeDrone.Bas:
			return getBas();
			//break;
		
		case CommandeDrone.Haut:
			return getHaut();
			//break;

		case CommandeDrone.Monter:
			return getHaut();
			//break;

		case CommandeDrone.Descendre:
			return getHaut();
			//break;

		case CommandeDrone.Stabilise:
			return getDroite();

		/*case CommandeDrone.RotationDroite :
			return getRotationDroite();

		case CommandeDrone.RotationGauche :
				return getRotationGauche();
		*/
		}

		return null;
	}


	public float [] manageMovementAngle(CommandeDrone direction){
		
		switch (direction) {
			
		case CommandeDrone.Droite:
			return getAngleDroite();
			//break;
			
		case CommandeDrone.Gauche:
			return getAngleGauche();
			//break;
			
		case CommandeDrone.Bas:
			return getAngleBas();
			//break;
			
		case CommandeDrone.Haut:
			return getAngleHaut();
			//break;
			
		case CommandeDrone.Monter:
		case CommandeDrone.RotationGauche:
		case CommandeDrone.RotationDroite:
		case CommandeDrone.Descendre:
		case CommandeDrone.Stabilise:
			return getAngleStabilise();

		}
		
		return null;
	}


	public float[] manageMovementVitesseMoteur(CommandeDrone commande){

		switch (commande) {

		case CommandeDrone.RotationDroite:
			return getRotationDroite();

		case CommandeDrone.RotationGauche:
			return getRotationGauche();

		case CommandeDrone.Vecteur:
			return getVitesseVecteur();

		}

		return null;
	}

// MOUVEMENT
	private int[] getDroite(){
		int[] tableau = new int[4];
		tableau [0] = -1;
		tableau [1] = 1;
		tableau [2] = 1;
		tableau [3] = -1;
		return tableau;
	}

	private int[] getGauche(){
		int[] tableau = new int[4];
		tableau [0] = 1;
		tableau [1] = -1;
		tableau [2] = -1;
		tableau [3] = 1;
		return tableau;
	}

	private int[] getHaut(){
		int[] tableau = new int[4];
		tableau [0] = -1;
		tableau [1] = 1;
		tableau [2] = -1;
		tableau [3] = 1;
		return tableau;
	}

	private int[] getBas(){
		int[] tableau = new int[4];
		tableau [0] = 1;
		tableau [1] = -1;
		tableau [2] =  1;
		tableau [3] = -1;
		return tableau;
	}
// MOUVEMENT
	
	public int[] getStabiliseOx(){
		int[] tableau = new int[4];
		//Bas pour Ox
		tableau [0] =  0;
		tableau [1] =  0;
		tableau [2] =  1;
		tableau [3] = -1;
		return tableau;
	}

	public int[] getStabiliseOz(){
		int[] tableau = new int[4];
		//Droite pour Oz
		tableau [0] = -1;
		tableau [1] =  1;
		tableau [2] =  0;
		tableau [3] =  0;
		return tableau;
	}

	private float[] getRotationDroite(){
		float[] tableau = new float[4];
		tableau [0] =  1.3F;
		tableau [1] =  1.3F;
		tableau [2] =  0.7F;
		tableau [3] =  0.7F;
		return tableau;
	}
	
	private float[] getRotationGauche(){
		float[] tableau = new float[4];
		tableau [0] = 0.7F;
		tableau [1] = 0.7F;
		tableau [2] = 1.3F;
		tableau [3] = 1.3F;
		return tableau;
	}

	private float[] getVitesseVecteur(){
		float[] tableau = new float[4];
		tableau [0] = 1.0F;
		tableau [1] = 1.0F;
		tableau [2] = 1.0F;
		tableau [3] = 1.0F;
		return tableau;
	}

//ANGLE
	private float[] getAngleDroite(){
		float[] tableau = new float[4];
		tableau [0] = 20.0F;
		tableau [1] = 20.0F;
		return tableau;
	}
	
	private float[] getAngleGauche(){
		float[] tableau = new float[4];
		tableau [0] = -20.0F;;
		tableau [1] = -20.0F;;
		return tableau;
	}
	
	private float[] getAngleHaut(){
		float[] tableau = new float[4];
		tableau [0] = 20.0F;;
		tableau [1] = -20.0F;;
		return tableau;
	}
	
	private float[] getAngleBas(){
		float[] tableau = new float[4];
		tableau [0] = -20.0F;
		tableau [1] = 20.0F;
		return tableau;
	}
	private float[] getAngleStabilise(){
		float[] tableau = new float[4];
		tableau [0] = 0.0F;
		tableau [1] = 0.0F;
		return tableau;
	}
//ANGLE

}
