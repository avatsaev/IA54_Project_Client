using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroneBehaviour : MonoBehaviour
{

    public float moveOffsetX = 1, moveOffsetZ = 1;
    Vector3 nextPosition;
    private int moveCoordination;
    private int directionChangeCount = 0, directionChangeFrame = 0;
    private int frameNum;
    //script de l'objet grid
    private Grid gridScript;
    //coins de la grille
    private Vector3 gridTL, gridRB;

    //gameObject camera
    GameObject myCamera;

    //script Intruder de la camera
    private Intruders IntrudersScript;

    //liste des intrus
    private List<GameObject> myIntruders;
    //intru trouve
    private GameObject foundIntruder;

    // Use this for initialization
    void Start()
    {

        myIntruders = new List<GameObject>();

        //initialisation du nombre de frame avant changement de direction
        directionChangeFrame = Random.Range(50, 100);

        //recuperation du component script de la grid
        gridScript = GameObject.Find("detectionGrid").GetComponent<Grid>();
        //recuperation des coins de la grille
        gridTL = gridScript.getLeftTop();
        print("gridTL : x = " + gridScript.getLeftTop().x + "; y = " + gridScript.getLeftTop().y + "; z = " + gridScript.getLeftTop().z);

        gridRB = gridScript.getRightBottom();
        print("gridRB : x = " + gridRB.x + "; y = " + gridRB.y + "; z = " + gridRB.z);

        myCamera = GameObject.FindGameObjectWithTag("MainCamera");

        IntrudersScript = myCamera.GetComponent<Intruders>();
    }

    // Update is called once per frame
    void Update()
    {
        //on compte le nombre de frame depuis le dernier changement de direction
        if (directionChangeCount == 0) //on incremente au debut pour prendre en compte un nouveau frame
        {
            moveOffsetX = Random.Range(-10f, 10f);//choix de la valeur de la composante en x
            moveOffsetZ = Random.Range(-10f, 10f);//choix de la valeur de la composante en z
            directionChangeCount += 1;
        }
        else if (directionChangeCount < directionChangeFrame) //tant qu'on inferieur a la valeur de changement de frame on incremente
        {
            directionChangeCount += 1;

        }
        else if (directionChangeCount == directionChangeFrame) //quand on atteint la valeur de changement de frame on met met les comptes a 0 afin d'effectuer un changement de direction 
        {
            directionChangeCount = 0;

        }

        //test si le mouvement est valide et l'effectue, sinon ne bouge pas
        if (testPosition(transform.position + new Vector3(moveOffsetX, 0, moveOffsetZ) * Time.deltaTime) == true)
        {

        }
        else
        {
            moveTo(new Vector3(moveOffsetX, 0, moveOffsetZ));
        }

        //recherche un intru dans son voisinage
        findIntruder();

    }

    public void findIntruder()
    {
        float ecart = 2f;
        myIntruders = IntrudersScript.getIntruderList();
        foundIntruder = myIntruders.Find(intruder => (
        (transform.position.x - ecart <= intruder.transform.position.x) && 
        (intruder.transform.position.x <= transform.position.x + ecart) &&
        (transform.position.z - ecart <= intruder.transform.position.z) &&
        (intruder.transform.position.z <= transform.position.z + ecart)
        ));

        if(foundIntruder != null)
        gridScript.interceptIntruder(foundIntruder);

    }

    public void moveTo(Vector3 posVect)
    {
        nextPosition = posVect * Time.deltaTime;
        transform.position = transform.position + nextPosition;
    }

    public bool testPosition(Vector3 newPos)
    {
        if (newPos.x < gridTL.x || newPos.x > gridRB.x || newPos.z < gridRB.z || newPos.z > gridTL.z)
            return true;
        else
            return false;

    }
}
