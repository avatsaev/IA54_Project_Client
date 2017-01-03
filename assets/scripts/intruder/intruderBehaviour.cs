﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntruderBehaviour : MonoBehaviour
{

    public float moveOffsetX, moveOffsetZ;
    Vector3 nextPosition;
    private int moveCoordination;
    private int directionChangeFrame = 0;
    private int frameNum;
    //script de l'objet grid
    private Grid gridScript;
    //coins de la grille
    private Vector3 gridTL, gridRB;
    //script d'un intru


    // Use this for initialization
    void Start()
    {
        //recuperation du component script de la grid
        gridScript = GameObject.Find("detectionGrid").GetComponent<Grid>();
        //recuperation des coins de la grille
        gridTL = gridScript.getLeftTop();
        print("gridTL : x = " + gridScript.getLeftTop().x + "; y = " + gridScript.getLeftTop().y + "; z = " + gridScript.getLeftTop().z);

        gridRB = gridScript.getRightBottom();
        print("gridRB : x = " + gridRB.x + "; y = " + gridRB.y + "; z = " + gridRB.z);

    }

    // Update is called once per frame
    void Update()
    {
        //directionChangeFrame = Random.Range(0, 100);
        if (directionChangeFrame == 0)
        {
            moveOffsetX = Random.Range(-10f, 10f);
            moveOffsetZ = Random.Range(-10f, 10f);
            directionChangeFrame += 1;
        }
        else if (directionChangeFrame < 100)
        {
            directionChangeFrame += 1;

        }
        else if (directionChangeFrame == 100)
        {
            directionChangeFrame = 0;

        }

        //if (directionChangeFrame < 5)
        //    changeDirection();

        nextPosition = new Vector3(moveOffsetX, 0, moveOffsetZ) * Time.deltaTime;

        if (testPosition(transform.position + nextPosition) == true)
        {
            print("vrai");

        }
        else
        {
            transform.position = transform.position += nextPosition;
            print("faux");
        }
    }

    /*  
    public void changeDirection()
    {
        moveCoordination = Random.Range(1, 5);

        switch (moveCoordination)
        {

            case 1:
                moveOffsetX = -moveOffsetX;
                moveOffsetZ = -moveOffsetZ;
                //moveOffsetX = moveOffsetX;
                //moveOffsetZ = moveOffsetZ;
                break;
            case 2:
                moveOffsetX = -moveOffsetX;
                //moveOffsetZ = moveOffsetZ;
                break;
            case 3:
                //moveOffsetX = moveOffsetX;
                moveOffsetZ = -moveOffsetZ;
                break;
            case 4:
                moveOffsetX = -moveOffsetX;
                moveOffsetZ = -moveOffsetZ;
                break;
            default:
                break;

        }
    }
    */

    public void setPosition(Vector3 posVect)
    {
        nextPosition = posVect;
    }

    public bool testPosition(Vector3 newPos)
    {
        if (newPos.x < gridTL.x || newPos.x > gridRB.x || newPos.z < gridRB.z || newPos.z > gridTL.z)
            return true;
        else
            return false;

    }
}
