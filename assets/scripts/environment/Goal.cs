using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal : MonoBehaviour
{
    //liste des points d'interets
    List<GameObject> goals;
    //objet point d'interet
    public GameObject goalPoint;
    //nombre de point d'interet
    private int goalNumber;
    //vecteur de position d'un point d'interet
    private Vector3 goalPointPos;
    //coordonnees x, y et z du point d'interet
    private float x, y, z;
    //script de l'objet grid
    private Grid gridScript;
    //coins de la grille
    Vector3 gridTL, gridRB;

    /*
    void Start()
    {

        //initialisation du nombre de point d'interet
        setGoalNumber(8);

        //recuperation du component script de la grid
        gridScript = GameObject.Find("detectionGrid").GetComponent<Grid>();
        //recuperation des coins de la grille
        gridTL = gridScript.getLeftTop();
        gridRB = gridScript.getRightBottom();

        //initialisation de la hauteur du point d'interet de sorte que tout le cylindre soit visible sur la plateforme
        y = 1.0f;
        
        //instanttion de la liste de point d'interet
        goals = new List<GameObject>();
        for (int i = 1; i <= goalNumber; i += 1)
        {
            //choix d'une valeur aleatoire pour x et y dans le champ de la plateforme 
            x = Random.Range(gridTL.x, gridRB.x);
            z = Random.Range(gridRB.z, gridTL.z);

            //verification de l'existence de ces coordonées dans la liste des coordonnées d'intrus
            
            while (goals.Find(goal => goal.transform.position.x == x) && goals.Find(goal => goal.transform.position.z == z))
            {
                x = Random.Range(gridTL.x, gridRB.x);
                z = Random.Range(gridRB.z, gridTL.z);
            }
            

            goalPointPos = new Vector3(x, y, z);
            //affichage des coordonnées choisies
            print("x = " + goalPointPos.x + "; y = " + goalPointPos.y + "; z = " + goalPointPos.z);

            //Quaternion orientation = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.up);
            Instantiate(goalPoint, goalPointPos, Quaternion.identity);
            //goalPoint.transform.position = goalPointPos;
            //print("goalPoint.x = " + goalPoint.transform.position.x + "; goalPoint.y = " + goalPoint.transform.position.y + "; goalPoint.z = " + goalPoint.transform.position.z);

            //ajout du nouveau point d'interet à la liste des points d'interet
            goals.Add(goalPoint);

            //pour ajout d'un ensemble d'objet avec un certain tag
            //goals.AddRange(GameObject.FindGameObjectsWithTag("goalTag"));
        }

        print("goal list : ");
        
        foreach(GameObject goal in goals)
        {
            print("(" + goal.transform.position.x + ", " + goal.transform.position.y + ", " + goal.transform.position.z + ")");
        }
        

    }
    */

    void Update()
    {
       
    }

    public void setGoalNumber(int number)
    {
        goalNumber = number;
    }
    

}