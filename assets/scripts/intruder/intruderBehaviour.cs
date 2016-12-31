using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntruderBehaviour : MonoBehaviour
{

    public float moveOffsetX, moveOffsetZ;
    Vector3 nextPosition;
    private int moveCoordination;
    private int directionChangeFrame;
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
        frameNum++;
        directionChangeFrame = Random.Range(frameNum - 100, frameNum + 100);

        moveOffsetX = Random.Range(2f, 3f);
        moveOffsetZ = Random.Range(2f, 3f);

        if (directionChangeFrame < frameNum)
            changeDirection();


        nextPosition = new Vector3(moveOffsetX, 0, moveOffsetZ) * Time.deltaTime;
        
        transform.position += nextPosition;
    }

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

    public void setPosition(Vector3 posVect)
    {
        nextPosition = posVect;
    }
}