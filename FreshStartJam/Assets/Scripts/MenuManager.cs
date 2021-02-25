using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    float dclick_threshold = 0.3f, timerdclick = 0;

    public GameObject errorPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AppClicked()
    {
        if ((Time.time - timerdclick) > dclick_threshold)
        {
            Debug.Log("single click");
            //call the SingleClick() function, not shown
        }
        else
        {
            Debug.Log("double click");
            //call the DoubleClick() function, not shown
        }
        timerdclick = Time.time;
    }
}
