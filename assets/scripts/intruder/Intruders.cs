﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Intruders : MonoBehaviour
{
    //liste des intrus
    List<GameObject> intruderList;
    //liste des vecteur position des intrus
    List<Vector3> intruderPosList;
    //objet intru
    public GameObject intruder;
    //nombre d'intrus
    public int intruderNumber;
    //vecteur de position d'un intru
    private Vector3 intruderPos;
    //coordonnees x, y et z de l'intrus
    private float x, y, z;
    //script de l'objet grid
    private Grid gridScript;
    //coins de la grille
    private Vector3 gridTL, gridRB;
    //script d'un intru
    private IntruderBehaviour intruderScript;


    void Start()
    {

        //initialisation du nombre de point d'interet
        //setIntruderNumber(15);

        //recuperation du component script de la grid
        gridScript = GameObject.Find("detectionGrid").GetComponent<Grid>();
        //recuperation des coins de la grille
        gridTL = gridScript.getLeftTop();
        print("gridTL : x = " + gridScript.getLeftTop().x + "; y = " + gridScript.getLeftTop().y + "; z = " + gridScript.getLeftTop().z);

        gridRB = gridScript.getRightBottom();
        print("gridRB : x = " + gridRB.x + "; y = " + gridRB.y + "; z = " + gridRB.z);

        //instantiaition de la liste
        intruderList = new List<GameObject>();
        //instantiaition de la liste vecteur posotion
        intruderPosList = new List<Vector3>();

        for (int i = 1; i <= intruderNumber; i += 1)
        {
            addIntruder();

            /* 
            //defintion de la positioon choisie pour l'intru
            intruderPos = setIntruderPosition(Random.Range(0, 5));
            print("intruPos " + i + " : x = " + intruderPos.x + "; y = " + intruderPos.y + "; z = " + intruderPos.z);

            //Quaternion orientation = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.up);
            Instantiate(intruder, intruderPos, Quaternion.identity);
            */
        }


    }

    void Update()
    {
        updateIntruderList();
        updateIntruderPosList();
    }

    //pour creer un nouveau drone
    public void addIntruder()
    {
        //defintion de la positioon choisie pour l'intru
        intruderPos = setIntruderPosition(Random.Range(0, 5));

        //Quaternion orientation = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.up);
        Instantiate(intruder, intruderPos, Quaternion.identity);

    }



    //mise a de la liste des intrus
    public void updateIntruderList()
    {
        intruderList.Clear();
        intruderList.AddRange(GameObject.FindGameObjectsWithTag("intruderTag"));
    }

    //mise a jour des vecteurs position
    public void updateIntruderPosList()
    {

        intruderPosList.Clear();
        foreach(GameObject myIntruder in intruderList)
        {
            intruderPosList.Add(myIntruder.transform.position);
        }

    }

    public void setIntruderNumber(int number)
    {
        intruderNumber = number;
    }


    public Vector3 setIntruderPosition(int appearingCase)
    {
        do
        {
            switch (appearingCase)
            {
                case 1:
                    //apparition de l'intrus a gauche
                    x = -30f;
                    z = Random.Range(gridRB.z, gridTL.z);
                    y = Random.Range(1f, 20f);
                    break;
                case 2:
                    //appartion de l'intru a droite
                    x = 30f;
                    z = Random.Range(gridRB.z, gridTL.z);
                    y = Random.Range(1f, 20f);
                    break;
                case 3:
                    //apparition de l'intru en bas
                    x = Random.Range(gridTL.x, gridRB.x);
                    z = -30f;
                    y = Random.Range(1f, 20f);
                    break;
                case 4:
                    //apparition de l'intru en haut 
                    x = Random.Range(gridTL.x, gridRB.x);
                    z = 30f;
                    y = Random.Range(1f, 20f);
                    break;
                default:
                    //print("Incorrect case");
                    break;
            }

        } while (intruderList.Find(intruder => intruder.transform.position.x == x) && intruderList.Find(intruder => intruder.transform.position.z == z));

        print("intru : " + "x = " + x + "; y = " + y + "; z = " + z);


        return new Vector3(x, y, z);
    }

    public List<GameObject> getIntruderList()
    {
        return intruderList;
    }
   
}