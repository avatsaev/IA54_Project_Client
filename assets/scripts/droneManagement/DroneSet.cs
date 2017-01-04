using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroneSet : MonoBehaviour
{
    //liste des intrus
    List<GameObject> droneList;
    //liste des vecteur position des drones
    List<Vector3> dronePosList;
    //objet intru
    public GameObject drone;
    //nombre d'intrus
    private int droneNumber;
    //vecteur de position d'un intru
    private Vector3 dronePos;
    //coordonnees x, y et z de l'intrus
    private float x, y, z;
    //script de l'objet grid
    private Grid gridScript;
    //coins de la grille
    private Vector3 gridTL, gridRB;
    //script d'un intru
    private IntruderBehaviour droneScript;


    void Start()
    {
        //initialisation du nombre de point d'interet
        setDroneNumber(10);

        //recuperation du component script de la grid
        gridScript = GameObject.Find("detectionGrid").GetComponent<Grid>();
        //recuperation des coins de la grille
        gridTL = gridScript.getLeftTop();
        print("gridTL : x = " + gridScript.getLeftTop().x + "; y = " + gridScript.getLeftTop().y + "; z = " + gridScript.getLeftTop().z);

        gridRB = gridScript.getRightBottom();
        print("gridRB : x = " + gridRB.x + "; y = " + gridRB.y + "; z = " + gridRB.z);

        //instantiaition de la liste
        droneList = new List<GameObject>();
        //instantiaition de la liste
        dronePosList = new List<Vector3>();

        for (int i = 1; i <= droneNumber; i += 1)
        {

            //defintion de la positioon choisie pour l'intru
            dronePos = setDronePosition(Random.Range(0, 5));
            print("dronePos " + i + " : x = " + dronePos.x + "; y = " + dronePos.y + "; z = " + dronePos.z);

            //Quaternion orientation = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.up);
            Instantiate(drone, dronePos, Quaternion.identity);
        }


    }


    void Update()
    {


    }

    //definit le nombre de drone
    public void setDroneNumber(int number)
    {
        droneNumber = number;
    }

    //pour creer un nouveau drone
    public void addDrone()
    {
        //defintion de la positioon choisie pour l'intru
        dronePos = setDronePosition(Random.Range(0, 5));

        //Quaternion orientation = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.up);
        Instantiate(drone, dronePos, Quaternion.identity);

    }

    //met ajout la liste des drones
    public void updateDroneList()
    {
        droneList.Clear();
        droneList.AddRange(GameObject.FindGameObjectsWithTag("droneTag"));
    }

    //mise a jour des vecteurs position
    public void updateDronePosList()
    {

        dronePosList.Clear();
        foreach (GameObject myDrone in droneList)
        {
            dronePosList.Add(myDrone.transform.position);
        }

    }

    //choisit une position aleatoire sur une bordure de la grille
    public Vector3 setDronePosition(int appearingCase)
    {
        do
        {
            switch (appearingCase)
            {
                case 1:
                    //apparition de l'intrus a gauche
                    x = -30f;
                    z = Random.Range(gridRB.z, gridTL.z);
                    y = 30f;
                    break;
                case 2:
                    //appartion de l'intru a droite
                    x = 30f;
                    z = Random.Range(gridRB.z, gridTL.z);
                    y = 30f;
                    break;
                case 3:
                    //apparition de l'intru en bas
                    x = Random.Range(gridTL.x, gridRB.x);
                    z = -30f;
                    y = 30f;
                    break;
                case 4:
                    //apparition de l'intru en haut 
                    x = Random.Range(gridTL.x, gridRB.x);
                    z = 30f;
                    y = 30f;
                    break;
                default:
                    //print("Incorrect case");
                    break;
            }

        } while (droneList.Find(drone => drone.transform.position.x == x) && droneList.Find(drone => drone.transform.position.z == z));

        print("drone : " + "x = " + x + "; y = " + y + "; z = " + z);


        return new Vector3(x, y, z);
    }

}