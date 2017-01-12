using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    private Vector3 gridLeftTop;
    private Vector3 gridRightBottom;
    Material goalFoundMat;
    Material intruderFoundMat;

    //gameObject camera
    GameObject myCamera;
    //script de l'objet droneSet
    private DroneSet myDrones;
    //script de l'objet intruders
    private Intruders myIntruders;


    // Use this for initialization
    void Start () {

        //detection de la camera
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");

        //recuperation du component script DroneSet de la camera
        myDrones = myCamera.GetComponent<DroneSet>();

        //recuperation du component script DroneSet de la camera
        myIntruders = myCamera.GetComponent<Intruders>();


        setLeftTop(new Vector3(-30f, 0f, 30f));
        print("gridLeftTop : x = " + gridLeftTop.x + "; y = " + gridLeftTop.y + "; z = " + gridLeftTop.z);
        setRightBottom(new Vector3(30f, 0f, -30f));
        print("gridLeftTop : x = " + gridLeftTop.x + "; y = " + gridLeftTop.y + "; z = " + gridLeftTop.z);

        goalFoundMat = Resources.Load("goalFoundMaterial", typeof(Material)) as Material;
        intruderFoundMat = Resources.Load("intruderFoundMaterial", typeof(Material)) as Material;

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("d"))
            myDrones.addDrone();

        if (Input.GetKeyDown("i"))
            myIntruders.addIntruder();


    }

    public void interceptIntruder(GameObject intruderFound)
    {
        intruderFound.GetComponent<Renderer>().material = intruderFoundMat;
        intruderFound.GetComponent<IntruderBehaviour>().enabled = false;
    }

    public void reachGoalPoint(GameObject goalPointFound)
    {
        goalPointFound.GetComponent<Renderer>().material = goalFoundMat;
        //goalPointFound.GetComponent<Renderer>().material.color = new Color(246, 79, 9);
        //goalPointFound.GetComponent<IntruderBehaviour>().enabled = false;
    }


    public void setLeftTop(Vector3 coordinates)
    {
        gridLeftTop = coordinates;
    }

    public Vector3 getLeftTop()
    {
        return gridLeftTop;
    }

    public void setRightBottom(Vector3 coordinates)
    {
        gridRightBottom = coordinates;
    }

    public Vector3 getRightBottom()
    {
        return gridRightBottom;
    }

}
