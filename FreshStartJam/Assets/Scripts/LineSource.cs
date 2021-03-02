using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSource : MonoBehaviour
{

    public GameObject StartingPoints;
    public Transform source;

    [HideInInspector]
    public List<GameObject> Obstacle = new List<GameObject>();

    [HideInInspector]
    public List<Vector3> points = new List<Vector3>();

    [HideInInspector]
    public LineRenderer cable;

    public bool firstObsDestroyed = false, done = false;

    // Start is called before the first frame update
    void Start()
    {
        cable = GetComponent<LineRenderer>();
        foreach(Transform child in StartingPoints.GetComponentsInChildren<Transform>())
        {
            if(child.tag == "Obstacle")
            {
                Obstacle.Add(child.gameObject);
                
            }
            
        }
        

        makeLine();
    }

    // Update is called once per frame
    void Update()
    {
        if(Obstacle.Count <= 0)
        {
            done = true;
        }
    }

    public void makeLine()
    {

        resetLine();

        points.Add(source.position);

        Debug.Log("obstacle length : " + Obstacle.ToArray().Length);
        

        for (int i = 0; i < Obstacle.Count; i++)
        {
            
            points.Add(Obstacle[i].transform.position);

        }

        Debug.Log("points length : " + points.ToArray().Length);
        cable.positionCount = points.Count;
        cable.SetPositions(points.ToArray());
    }

    void resetLine()
    {
        points.Clear();
        cable.positionCount = 1;
    }
}
