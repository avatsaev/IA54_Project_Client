using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    private Vector3 gridLeftTop;
    private Vector3 gridRightBottom;

	// Use this for initialization
	void Start () {

        setLeftTop(new Vector3(-30f, 0f, 30f));
        setRightBottom(new Vector3(30f, 0f, -30f));
    }
	
	// Update is called once per frame
	void Update () {
	
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
