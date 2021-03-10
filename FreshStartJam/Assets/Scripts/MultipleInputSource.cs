using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleInputSource : MonoBehaviour
{

    float timeElapsed = 0f;
    public float lerpDuration = 3;

    public List<InputSource> inputs = new List<InputSource>();
    public List<GameObject> walls = new List<GameObject>();
    public List<Transform> endPoints = new List<Transform>();
    List<bool> openIt = new List<bool>();


    public int currentInput = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject wall in walls)
        {
            //endPoints.Add(wall.GetComponentInChildren<Transform>());
            openIt.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInput > 0)
        {
            if (openIt[currentInput - 1] == true)
            {
                //timeElapsed = 0;
                OpenWall(currentInput - 1);
                if (timeElapsed >= 3)
                {
                    openIt[currentInput - 1] = false;
                    timeElapsed = 0;

                }
            }
        }

    }

    public void enable(int i)
    {
        Debug.Log(currentInput);
        Debug.Log(inputs.Count - 1);
        if (currentInput <= inputs.Count - 1)
        {
            currentInput += 1;
            inputs[i].enabled = false;
            openIt[i] = true;
            Collider2D collider = inputs[i + 1].gameObject.GetComponent<Collider2D>();

            inputs[i + 1].enabled = true;
            collider.enabled = true;



        }


    }
    public void OpenWall(int i)
    {
        walls[i].transform.position = Vector2.Lerp(walls[i].transform.position, endPoints[i].position, lerpDuration * Time.deltaTime);
        timeElapsed += Time.deltaTime;
    }
}